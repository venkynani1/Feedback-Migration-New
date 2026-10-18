import { AnimatePresence, motion } from "framer-motion";
import { Suspense, useEffect, useRef, useState, type ReactNode } from "react";
import { NavLink, useLocation, useNavigate, useOutlet } from "react-router-dom";
import { api } from "../api";
import { useAuth, type User } from "../auth";
import { ADMIN_NAV, SUPER_ADMIN_NAV, findNav } from "../nav";
import { useTheme } from "../theme";
import { Icon, type IconName } from "./Icon";
import { SessionTimeout } from "./SessionTimeout";
import { Loading, requestHeadingFocus, useLoad } from "./ui";

const ROLE_LABELS: Record<User["role"], string> = {
  user: "Employee",
  admin: "Administrator",
  super: "Super Administrator",
};

function initials(name: string): string {
  const parts = name.trim().split(/\s+/).filter(Boolean);
  return ((parts[0]?.[0] ?? "") + (parts.length > 1 ? parts[parts.length - 1][0] : "")).toUpperCase() || "?";
}

export function Brand() {
  return (
    <span className="brand">
      <span className="brand-mark" aria-hidden="true">
        <Icon name="book" size={20} />
      </span>
      <span className="brand-text">
        <strong>Training Feedback</strong>
        <small>Learning &amp; Development</small>
      </span>
    </span>
  );
}

function NavItem({ to, icon, label }: { to: string; icon: IconName; label: string }) {
  return (
    <NavLink to={to} end className="nav-link">
      {({ isActive }) => (
        <>
          {isActive && (
            <motion.span
              className="nav-link-active"
              layoutId="nav-active"
              transition={{ type: "spring", stiffness: 520, damping: 40 }}
            />
          )}
          <Icon name={icon} />
          <span>{label}</span>
        </>
      )}
    </NavLink>
  );
}

function SideNav({ superAdmin }: { superAdmin: boolean }) {
  const groups = superAdmin ? [...ADMIN_NAV, SUPER_ADMIN_NAV] : ADMIN_NAV;
  return (
    <aside className="sidenav" id="sidenav">
      <NavLink to="/" className="sidenav-brand">
        <Brand />
      </NavLink>
      <nav aria-label="Main">
        <NavItem to="/" icon="home" label="Overview" />
        {groups.map((group) => (
          <div className="nav-group" key={group.label}>
            <h2>{group.label}</h2>
            {group.items.map((item) => (
              <NavItem key={item.to} to={item.to} icon={item.icon} label={item.label} />
            ))}
          </div>
        ))}
      </nav>
      <div className="sidenav-note">
        <span className="sidenav-note-icon">
          <Icon name="comment" size={18} />
        </span>
        <div>
          <strong>A little feedback goes a long way.</strong>
          <span>Make your next learning experience better.</span>
          <NavLink to="/feedback">
            Share your feedback <Icon name="chevron" size={13} />
          </NavLink>
        </div>
      </div>
    </aside>
  );
}

let firstRoute = true;

/** Animates each page in, and on navigation scrolls up and moves focus to the new page's heading. */
function RouteFrame({ children }: { children: ReactNode }) {
  const frame = useRef<HTMLDivElement>(null);
  useEffect(() => {
    if (firstRoute) {
      firstRoute = false;
      return;
    }
    window.scrollTo(0, 0);
    requestHeadingFocus(frame.current);
  }, []);
  return (
    <motion.div
      ref={frame}
      initial={{ opacity: 0, y: 10 }}
      animate={{ opacity: 1, y: 0, transition: { duration: 0.24, ease: [0.2, 0.8, 0.2, 1] } }}
      exit={{ opacity: 0, y: -4, transition: { duration: 0.1, ease: "easeIn" } }}
    >
      <Suspense fallback={<Loading />}>{children}</Suspense>
    </motion.div>
  );
}

export function Layout() {
  const { user, isAdmin, signOut, years, year, setYear } = useAuth();
  const navigate = useNavigate();
  const { pathname } = useLocation();
  const outlet = useOutlet();
  const theme = useTheme();
  const [navOpen, setNavOpen] = useState(false);
  const config = useLoad(() => api.get<{ demo: boolean; sessionMinutes: number }>("/api/auth/config"), []).data;
  const demo = config?.demo;

  // On small screens the menu is a drawer; picking a page puts it away.
  useEffect(() => setNavOpen(false), [pathname]);

  useEffect(() => {
    if (!navOpen) return;
    const close = (event: KeyboardEvent) => {
      if (event.key === "Escape") setNavOpen(false);
    };
    document.addEventListener("keydown", close);
    return () => document.removeEventListener("keydown", close);
  }, [navOpen]);

  if (!user) return null;
  const name = user.name || user.empId;

  return (
    <div className={`app${isAdmin ? " app-with-nav" : ""}${navOpen ? " nav-open" : ""}`}>
      <a className="skip-link" href="#main">
        Skip to content
      </a>
      {isAdmin && <SideNav superAdmin={user.role === "super"} />}
      {isAdmin && <div className="nav-backdrop" onClick={() => setNavOpen(false)} />}
      <div className="app-main">
        <header className="appbar">
          {isAdmin ? (
            <button
              type="button"
              className="icon-button nav-toggle"
              aria-label={navOpen ? "Close menu" : "Open menu"}
              aria-expanded={navOpen}
              aria-controls="sidenav"
              onClick={() => setNavOpen(!navOpen)}
            >
              <Icon name={navOpen ? "close" : "menu"} size={20} />
            </button>
          ) : (
            <NavLink to="/feedback" className="appbar-brand">
              <Brand />
            </NavLink>
          )}
          <div className="appbar-right">
            {isAdmin && (
              <div className="appbar-context">
                <span>Workspace</span>
                <Icon name="chevron" size={14} />
                <strong>{findNav(pathname)?.group.label ?? "Overview"}</strong>
              </div>
            )}
            {isAdmin && years.length > 0 && (
              <label className="year">
                <Icon name="calendar" size={16} />
                <span>Training year</span>
                <select aria-label="Training year" value={year ?? ""} onChange={(e) => setYear(Number(e.target.value))}>
                  {years.map((y) => (
                    <option key={y} value={y}>
                      {y}
                    </option>
                  ))}
                </select>
              </label>
            )}
            <button
              type="button"
              className="icon-button theme-toggle"
              aria-label={theme.dark ? "Switch to light theme" : "Switch to dark theme"}
              title={theme.dark ? "Light theme" : "Dark theme"}
              onClick={theme.toggle}
            >
              <AnimatePresence mode="wait" initial={false}>
                <motion.span
                  key={theme.dark ? "sun" : "moon"}
                  initial={{ opacity: 0, rotate: -60, scale: 0.6 }}
                  animate={{ opacity: 1, rotate: 0, scale: 1 }}
                  exit={{ opacity: 0, rotate: 60, scale: 0.6 }}
                  transition={{ duration: 0.16 }}
                >
                  <Icon name={theme.dark ? "sun" : "moon"} size={18} />
                </motion.span>
              </AnimatePresence>
            </button>
            <div className="user-chip">
              <span className="avatar" aria-hidden="true">
                {initials(name)}
              </span>
              <span className="user-chip-text">
                <strong>{name}</strong>
                <small>{ROLE_LABELS[user.role]}</small>
              </span>
            </div>
            <button
              type="button"
              className="button button-quiet button-small"
              aria-label="Sign out"
              onClick={() => signOut().then(() => navigate("/login"))}
            >
              <Icon name="logout" size={16} />
              <span className="hide-narrow">Sign out</span>
            </button>
          </div>
        </header>
        {demo && (
          <div className="demo-banner" role="status">
            <Icon name="info" size={16} />
            Demo mode: sample data, no mail is sent, changes are not kept.
          </div>
        )}
        <main className="content" id="main">
          <AnimatePresence mode="wait" initial={false}>
            <RouteFrame key={pathname}>{outlet}</RouteFrame>
          </AnimatePresence>
        </main>
        <footer className="footer">&copy; Hexaware Technologies Ltd. All rights reserved.</footer>
        {config && <SessionTimeout minutes={config.sessionMinutes} />}
      </div>
    </div>
  );
}
