import { expect, test as base, type Page } from "@playwright/test";

/*
 * Runs against demo mode (see playwright.config.ts). Demo data lives in the
 * server's memory, so tests that record feedback change it for later tests;
 * they run one at a time, in file order.
 */

// Every test fails if the page throws an error or the server answers 5xx.
const test = base.extend<{ watched: Page }>({
  watched: async ({ page }, use) => {
    const problems: string[] = [];
    page.on("pageerror", (error) => problems.push(`page error: ${error.message}`));
    page.on("response", (response) => {
      if (response.status() >= 500) problems.push(`${response.status()} from ${response.url()}`);
    });
    await use(page);
    expect(problems, problems.join("\n")).toEqual([]);
  },
});

async function signIn(page: Page, user: string) {
  await page.goto("/login");
  await page.locator("input[autocomplete=username]").fill(user);
  await page.locator("input[autocomplete=current-password]").fill("demo");
  await page.getByRole("button", { name: "Sign in" }).click();
}

test.describe("sign in", () => {
  test("lists the demo logins and refuses a wrong password", async ({ watched: page }) => {
    await page.goto("/login");
    await expect(page.getByRole("heading", { name: "Welcome back" })).toBeVisible();
    await expect(page.getByRole("button", { name: /^admin/ })).toBeVisible();

    await page.locator("input[autocomplete=username]").fill("admin");
    await page.locator("input[autocomplete=current-password]").fill("wrong");
    await page.getByRole("button", { name: "Sign in" }).click();
    await expect(page.getByRole("alert")).toContainText("not successful");
  });

  test("the password can be shown and hidden", async ({ watched: page }) => {
    await page.goto("/login");
    const password = page.locator("input[autocomplete=current-password]");
    await page.getByRole("button", { name: "Show password" }).click();
    await expect(password).toHaveAttribute("type", "text");
    await page.getByRole("button", { name: "Hide password" }).click();
    await expect(password).toHaveAttribute("type", "password");
  });
});

test.describe("administrator", () => {
  test("lands on the workspace and can open every menu page", async ({ watched: page }) => {
    await signIn(page, "admin");
    await expect(page.getByRole("heading", { level: 1 })).toContainText("A little feedback");

    const links = page.getByRole("navigation", { name: "Main" }).getByRole("link");
    const count = await links.count();
    expect(count).toBeGreaterThan(10);
    for (let i = 0; i < count; i++) {
      const link = links.nth(i);
      const label = (await link.innerText()).trim();
      await link.click();
      const heading = page.getByRole("heading", { level: 1 });
      await expect(heading, `heading on ${label}`).toBeVisible();
      if (label !== "Overview") {
        await expect(heading).toHaveText(label);
        // Keyboard and screen reader users start on the new page's heading.
        await expect(heading).toBeFocused();
      }
    }
  });

  test("finds tools from the workspace search", async ({ watched: page }) => {
    await signIn(page, "admin");
    await page.getByRole("searchbox", { name: "Find a workspace tool" }).fill("defaulters");
    await expect(page.locator(".tool-link")).toHaveCount(1);
    await page.keyboard.press("Enter");
    await expect(page).toHaveURL(/\/reports\/defaulters$/);
  });

  test("sorts a table by clicking its heading", async ({ watched: page }) => {
    await signIn(page, "admin");
    await page.goto("/admin/employees");
    await page.getByRole("button", { name: "Search" }).click();
    const header = page.getByRole("columnheader", { name: /Employee Name/i });
    await header.getByRole("button").click();
    await expect(header).toHaveAttribute("aria-sort", "ascending");
    const names = await page.locator("tbody tr td:nth-child(2)").allInnerTexts();
    expect(names).toEqual([...names].sort((a, b) => a.localeCompare(b)));
    await header.getByRole("button").click();
    await expect(header).toHaveAttribute("aria-sort", "descending");
  });

  test("switches to the dark theme and remembers it", async ({ watched: page }) => {
    await signIn(page, "admin");
    await page.getByRole("button", { name: "Switch to dark theme" }).click();
    await expect(page.locator("html")).toHaveAttribute("data-theme", "dark");
    await page.reload();
    await expect(page.locator("html")).toHaveAttribute("data-theme", "dark");
  });
});

test.describe("employee", () => {
  test("cannot open administrator pages", async ({ watched: page }) => {
    await signIn(page, "employee");
    await expect(page).toHaveURL(/\/feedback$/);
    await expect(page.getByRole("navigation", { name: "Main" })).toHaveCount(0);
    await page.goto("/masters/vendor");
    await expect(page).toHaveURL(/\/feedback$/);
  });

  test("records feedback, and a half-filled form survives a reload", async ({ watched: page }) => {
    await signIn(page, "employee");
    await page.getByRole("link", { name: "Record My Feedback" }).first().click();
    await expect(page.getByRole("heading", { level: 1 })).toHaveText("Record My Feedback");

    const trainer = page.getByRole("combobox");
    await trainer.selectOption({ index: 1 });
    for (const question of await page.locator("fieldset.question").all()) {
      await question.getByText("Excellent").click();
    }
    await page.getByRole("textbox").fill("Clear examples and good pace.");

    await page.reload();
    await expect(page.getByText("Your unsubmitted answers have been restored.")).toBeVisible();
    await expect(page.getByRole("textbox")).toHaveValue("Clear examples and good pace.");

    await page.getByRole("button", { name: /^Submit/ }).click();
    await page.getByRole("dialog").getByRole("button", { name: "Submit" }).click();
    await expect(page.getByText(/Feedback recorded for|awaiting feedback/).first()).toBeVisible();
  });
});

test.describe("idle sessions", () => {
  test("warn before signing out, and bring the user back afterwards", async ({ watched: page }) => {
    await page.clock.install();
    await signIn(page, "admin");
    await page.goto("/masters/vendor");
    await expect(page.getByRole("heading", { level: 1 })).toHaveText("Vendor Master");

    await page.clock.fastForward("28:30");
    const warning = page.getByRole("dialog", { name: "Are you still there?" });
    await expect(warning).toBeVisible();
    await warning.getByRole("button", { name: "Stay signed in" }).click();
    await expect(warning).toBeHidden();

    await page.clock.fastForward("31:00");
    await expect(page).toHaveURL(/\/login$/);
    await expect(page.getByText(/signed out after 30 minutes without activity/)).toBeVisible();

    await page.locator("input[autocomplete=username]").fill("admin");
    await page.locator("input[autocomplete=current-password]").fill("demo");
    await page.getByRole("button", { name: "Sign in" }).click();
    await expect(page).toHaveURL(/\/masters\/vendor$/);
  });
});

test("health checks answer", async ({ request }) => {
  expect((await request.get("/api/health")).ok()).toBe(true);
  const ready = await request.get("/api/health/ready");
  expect(await ready.json()).toEqual({ status: "ok", database: "ok" });
});
