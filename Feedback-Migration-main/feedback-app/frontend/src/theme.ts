import { useEffect, useState } from "react";

type ThemeChoice = "system" | "light" | "dark";

const KEY = "tf-theme";
const media = window.matchMedia("(prefers-color-scheme: dark)");

function stored(): ThemeChoice {
  try {
    const value = localStorage.getItem(KEY);
    return value === "light" || value === "dark" ? value : "system";
  } catch {
    return "system";
  }
}

function apply(choice: ThemeChoice) {
  const dark = choice === "dark" || (choice === "system" && media.matches);
  document.documentElement.dataset.theme = dark ? "dark" : "light";
}

/** Set the theme before the first render and follow the system setting until the user picks one. */
export function initTheme() {
  apply(stored());
  media.addEventListener("change", () => apply(stored()));
}

export function useTheme() {
  const [choice, setChoice] = useState<ThemeChoice>(stored);
  const [systemDark, setSystemDark] = useState(media.matches);

  useEffect(() => {
    const follow = () => setSystemDark(media.matches);
    media.addEventListener("change", follow);
    return () => media.removeEventListener("change", follow);
  }, []);

  useEffect(() => {
    apply(choice);
    try {
      if (choice === "system") localStorage.removeItem(KEY);
      else localStorage.setItem(KEY, choice);
    } catch {
      // Private windows can refuse storage; the choice then lasts for this visit only.
    }
  }, [choice, systemDark]);

  const dark = choice === "dark" || (choice === "system" && systemDark);
  return { dark, toggle: () => setChoice(dark ? "light" : "dark") };
}
