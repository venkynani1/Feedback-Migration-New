import { Link } from "react-router-dom";
import { useAuth } from "../auth";
import { Icon } from "../components/Icon";
import { Page } from "../components/ui";
import { ADMIN_NAV, SUPER_ADMIN_NAV } from "../nav";

export function Home() {
  const { user, year } = useAuth();
  const groups = user?.role === "super" ? [...ADMIN_NAV, SUPER_ADMIN_NAV] : ADMIN_NAV;
  return (
    <Page
      title="Admin Home"
      description={`Welcome, ${user?.name || user?.empId}.${year !== null ? ` Session lists show sessions starting in ${year}.` : ""}`}
    >
      <div className="tiles">
        {groups.map((group) => (
          <section className="card" key={group.label}>
            <div className="card-header">
              <h2>{group.label}</h2>
            </div>
            <ul className="tile-links">
              {group.items.map((item) => (
                <li key={item.to}>
                  <Link to={item.to}>
                    <span className="tile-icon">
                      <Icon name={item.icon} size={20} />
                    </span>
                    <span className="tile-text">
                      <strong>{item.label}</strong>
                      <small>{item.description}</small>
                    </span>
                    <Icon name="chevron" size={16} />
                  </Link>
                </li>
              ))}
            </ul>
          </section>
        ))}
      </div>
    </Page>
  );
}
