import { useEffect, useState } from "react";
import { NavLink, Outlet, useLocation, useNavigate } from "react-router-dom";
import { api } from "../api";
import { useAuth, type User } from "../auth";
import { ADMIN_NAV, SUPER_ADMIN_NAV } from "../nav";
import { Icon } from "./Icon";
import { useLoad } from "./ui";

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
        TF
      </span>
      <span className="brand-text">
        <strong>Training Feedback</strong>
        <small>Learning &amp; Development</small>
      </span>
    </span>
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
        <NavLink to="/" end className="nav-link">
          <Icon name="home" />
          Home
        </NavLink>
        {groups.map((group) => (
          <div className="nav-group" key={group.label}>
            <h2>{group.label}</h2>
            {group.items.map((item) => (
              <NavLink key={item.to} to={item.to} end className="nav-link">
                <Icon name={item.icon} />
                {item.label}
              </NavLink>
            ))}
          </div>
        ))}
      </nav>
    </aside>
  );
}

export function Layout() {
  const { user, isAdmin, signOut, years, year, setYear } = useAuth();
  const navigate = useNavigate();
  const { pathname } = useLocation();
  const [navOpen, setNavOpen] = useState(false);
  const demo = useLoad(() => api.get<{ demo: boolean }>("/api/auth/config"), []).data?.demo;

  // On small screens the menu is a drawer; picking a page puts it away.
  useEffect(() => setNavOpen(false), [pathname]);

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
            {isAdmin && years.length > 0 && (
              <label className="year">
                <span>Year</span>
                <select value={year ?? ""} onChange={(e) => setYear(Number(e.target.value))}>
                  {years.map((y) => (
                    <option key={y} value={y}>
                      {y}
                    </option>
                  ))}
                </select>
              </label>
            )}
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
          <Outlet />
        </main>
        <footer className="footer">&copy; Hexaware Technologies Ltd. All rights reserved.</footer>
      </div>
    </div>
  );
}
