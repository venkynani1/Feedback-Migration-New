import { defineConfig } from "@playwright/test";

/*
 * Browser tests. They run the real application in demo mode (built-in sample
 * data, no database, StationH or mail), against the built front end, so
 * `npm run test:e2e` builds first. Port 8001 keeps them clear of a dev server.
 */
const PORT = 8001;
const python = process.platform === "win32" ? ".venv\\Scripts\\python.exe" : ".venv/bin/python";

export default defineConfig({
  testDir: "e2e",
  fullyParallel: false,
  workers: 1,
  retries: process.env.CI ? 1 : 0,
  reporter: process.env.CI ? [["list"], ["html", { open: "never" }]] : "list",
  use: {
    baseURL: `http://127.0.0.1:${PORT}`,
    // The installed Chrome, so no browser download is needed. Override with PW_CHANNEL.
    channel: process.env.PW_CHANNEL ?? "chrome",
    trace: "retain-on-failure",
  },
  webServer: {
    command: `${python} -m uvicorn app.main:app --host 127.0.0.1 --port ${PORT}`,
    cwd: "../backend",
    url: `http://127.0.0.1:${PORT}/api/health`,
    reuseExistingServer: false,
    env: {
      FEEDBACK_ENVIRONMENT: "development",
      FEEDBACK_DEMO_MODE: "true",
      FEEDBACK_COOKIE_SECURE: "false",
      FEEDBACK_SECRET_KEY: "playwright-only-secret-key-not-used-anywhere-else",
      FEEDBACK_LOG_FILE: "",
    },
  },
});
