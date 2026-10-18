import { motion, type Variants } from "framer-motion";
import { useEffect, useRef, useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import { useAuth } from "../auth";
import { Icon, type IconName } from "../components/Icon";
import { useHeadingFocus } from "../components/ui";
import { ADMIN_NAV, SUPER_ADMIN_NAV } from "../nav";

const MotionLink = motion.create(Link);

const FEATURES: Array<{ to: string; icon: IconName; label: string; title: string; description: string }> = [
  { to: "/sessions/new", icon: "calendar", label: "Plan", title: "Make room for growth", description: "Create a session and bring your people together." },
  { to: "/feedback", icon: "comment", label: "Reflect", title: "Every perspective matters", description: "Share feedback, record ratings, and review your team." },
  { to: "/reports/overall", icon: "trend", label: "Understand", title: "See the bigger picture", description: "Explore session scores and trainer performance." },
];

const stagger: Variants = { show: { transition: { staggerChildren: 0.05, delayChildren: 0.05 } } };
const rise: Variants = {
  hidden: { opacity: 0, y: 12 },
  show: { opacity: 1, y: 0, transition: { duration: 0.32, ease: [0.2, 0.8, 0.2, 1] } },
};

export function Home() {
  const { user, year } = useAuth();
  const [query, setQuery] = useState("");
  const [category, setCategory] = useState("All tools");
  const search = useRef<HTMLInputElement>(null);
  const heading = useHeadingFocus();
  const navigate = useNavigate();
  const groups = user?.role === "super" ? [...ADMIN_NAV, SUPER_ADMIN_NAV] : ADMIN_NAV;
  const items = groups.flatMap((group) => group.items.map((item) => ({ ...item, group: group.label })));
  const wanted = query.trim().toLowerCase();
  const filtered = items.filter(
    (item) =>
      (category === "All tools" || item.group === category) &&
      `${item.group} ${item.label} ${item.description}`.toLowerCase().includes(wanted),
  );

  useEffect(() => {
    document.title = "Workspace - Training Feedback";
    const focusSearch = (event: KeyboardEvent) => {
      const target = event.target as HTMLElement | null;
      if (
        event.key === "/" &&
        !event.ctrlKey &&
        !event.metaKey &&
        !event.altKey &&
        !target?.closest("input, textarea, select, [contenteditable='true']")
      ) {
        event.preventDefault();
        search.current?.focus();
      }
    };
    document.addEventListener("keydown", focusSearch);
    return () => document.removeEventListener("keydown", focusSearch);
  }, []);

  return (
    <motion.section className="workspace-home" variants={stagger} initial="hidden" animate="show">
      <motion.div className="workspace-topline" variants={rise}>
        <span>Your learning workspace</span>
        <span>
          <Icon name="calendar" size={14} /> {year ?? "All years"} training year
        </span>
      </motion.div>

      <motion.header className="workspace-hero" variants={rise}>
        <div className="hero-symbol" aria-hidden="true">
          <Icon name="book" size={26} />
        </div>
        <p className="hero-greeting">Welcome back, {user?.name || user?.empId}</p>
        <h1 ref={heading} tabIndex={-1}>
          A little feedback. <span>A big difference.</span>
        </h1>
        <p className="hero-description">A thoughtful space to plan, listen, and help your people grow.</p>
        <form
          className="workspace-search"
          role="search"
          onSubmit={(event) => {
            event.preventDefault();
            if (filtered.length) navigate(filtered[0].to);
          }}
        >
          <Icon name="search" size={20} />
          <input
            ref={search}
            type="search"
            aria-label="Find a workspace tool"
            placeholder="What would you like to work on?"
            value={query}
            onChange={(event) => {
              setQuery(event.target.value);
              setCategory("All tools");
            }}
          />
          <kbd aria-hidden="true">/</kbd>
          <button className="search-go" aria-label="Open first matching tool" disabled={!filtered.length}>
            <Icon name="chevron" size={18} />
          </button>
        </form>
        <div className="hero-shortcuts">
          <span>Jump to</span>
          <Link to="/sessions/participants">
            Participants <Icon name="chevron" size={12} />
          </Link>
          <Link to="/reports/comments">
            Comments <Icon name="chevron" size={12} />
          </Link>
          <Link to="/upload">
            Excel upload <Icon name="chevron" size={12} />
          </Link>
        </div>
      </motion.header>

      {!query && (
        <motion.div className="feature-grid" variants={stagger}>
          {FEATURES.map((item, index) => (
            <MotionLink
              key={item.to}
              to={item.to}
              className={`feature-card feature-card-${index}`}
              variants={rise}
              whileHover={{ y: -3 }}
              whileTap={{ scale: 0.98 }}
            >
              <div className="feature-top">
                <span className="feature-icon">
                  <Icon name={item.icon} size={22} />
                </span>
                <span className="feature-label">{item.label}</span>
                <Icon name="chevron" size={17} />
              </div>
              <h2>{item.title}</h2>
              <p>{item.description}</p>
            </MotionLink>
          ))}
        </motion.div>
      )}

      <motion.section className="tool-directory" aria-labelledby="tools-heading" variants={rise}>
        <div className="section-heading">
          <div>
            <h2 id="tools-heading">{query ? "Search results" : "Your tools, one place"}</h2>
            <p>{query ? `Tools matching “${query}”` : "Everything you need for a better learning experience."}</p>
          </div>
          <span className="section-meta" aria-live="polite">
            {filtered.length} {filtered.length === 1 ? "tool" : "tools"}
          </span>
        </div>
        <div className="category-filters" role="group" aria-label="Filter tools by category">
          {["All tools", ...groups.map((group) => group.label)].map((label) => (
            <button type="button" key={label} aria-pressed={category === label} onClick={() => setCategory(label)}>
              {category === label && (
                <motion.span
                  className="category-active"
                  layoutId="category-active"
                  transition={{ type: "spring", stiffness: 520, damping: 40 }}
                />
              )}
              <span>{label}</span>
            </button>
          ))}
        </div>
        {/* Filtered-out tools leave at once (an exit animation interrupted by fast
            typing could leave them on screen); the rest glide into place. */}
        <div className="tool-grid">
          {filtered.map((item) => (
            <MotionLink
              layout="position"
              key={item.to}
              className="tool-link"
              to={item.to}
              initial={{ opacity: 0, scale: 0.96 }}
              animate={{ opacity: 1, scale: 1 }}
              transition={{ duration: 0.2, ease: [0.2, 0.8, 0.2, 1] }}
            >
              <span className="tool-icon">
                <Icon name={item.icon} size={20} />
              </span>
              <span className="tool-copy">
                <strong>{item.label}</strong>
                <small>{item.description}</small>
              </span>
              <Icon name="chevron" size={15} />
            </MotionLink>
          ))}
        </div>
        {!filtered.length && (
          <div className="empty" role="status">
            <Icon name="search" size={28} />
            <strong>No matching tools</strong>
            <p>Try “session”, “feedback”, or “report”.</p>
            <button
              type="button"
              className="button button-quiet"
              onClick={() => {
                setQuery("");
                setCategory("All tools");
              }}
            >
              Clear search
            </button>
          </div>
        )}
      </motion.section>

      <div className="workspace-endnote">
        <Icon name="book" size={15} />
        <span>Small reflections. Meaningful progress.</span>
      </div>
    </motion.section>
  );
}
