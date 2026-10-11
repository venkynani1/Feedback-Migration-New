import { createContext, useCallback, useContext, useEffect, useMemo, useState, type ReactNode } from "react";
import { api } from "./api";

export interface User {
  empId: string;
  name: string;
  role: "user" | "admin" | "super";
}

interface AuthState {
  user: User | null;
  loading: boolean;
  isAdmin: boolean;
  signIn: (username: string, password: string) => Promise<User>;
  signOut: () => Promise<void>;
  /** Reporting year chosen in the header; admin pages filter sessions by it. */
  years: number[];
  year: number | null;
  setYear: (year: number) => void;
}

const AuthContext = createContext<AuthState | null>(null);
const YEAR_KEY = "feedback.year";

export function AuthProvider({ children }: { children: ReactNode }) {
  const [user, setUser] = useState<User | null>(null);
  const [loading, setLoading] = useState(true);
  const [years, setYears] = useState<number[]>([]);
  const [year, setYearState] = useState<number | null>(null);

  useEffect(() => {
    api
      .get<User>("/api/auth/me")
      .then(setUser)
      .catch(() => setUser(null))
      .finally(() => setLoading(false));
    const signedOut = () => setUser(null);
    window.addEventListener("feedback:signed-out", signedOut);
    return () => window.removeEventListener("feedback:signed-out", signedOut);
  }, []);

  const isAdmin = user !== null && user.role !== "user";

  useEffect(() => {
    if (!isAdmin) {
      setYears([]);
      setYearState(null);
      return;
    }
    api
      .get<number[]>("/api/lookups/years")
      .then((list) => {
        setYears(list);
        const remembered = Number(sessionStorage.getItem(YEAR_KEY));
        setYearState(list.includes(remembered) ? remembered : (list[0] ?? null));
      })
      .catch(() => setYears([]));
  }, [isAdmin]);

  const setYear = useCallback((value: number) => {
    sessionStorage.setItem(YEAR_KEY, String(value));
    setYearState(value);
  }, []);

  const signIn = useCallback(async (username: string, password: string) => {
    const signedIn = await api.post<User>("/api/auth/login", { username, password });
    setUser(signedIn);
    return signedIn;
  }, []);

  const signOut = useCallback(async () => {
    await api.post("/api/auth/logout").catch(() => undefined);
    sessionStorage.removeItem(YEAR_KEY);
    setUser(null);
  }, []);

  const value = useMemo(
    () => ({ user, loading, isAdmin, signIn, signOut, years, year, setYear }),
    [user, loading, isAdmin, signIn, signOut, years, year, setYear],
  );
  return <AuthContext.Provider value={value}>{children}</AuthContext.Provider>;
}

export function useAuth(): AuthState {
  const state = useContext(AuthContext);
  if (!state) throw new Error("useAuth must be used inside AuthProvider");
  return state;
}
