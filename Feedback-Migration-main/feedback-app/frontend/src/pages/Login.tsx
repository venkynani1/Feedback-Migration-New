import { motion } from "framer-motion";
import { useEffect, useState, type FormEvent } from "react";
import { Navigate, useLocation, useNavigate, type Location } from "react-router-dom";
import { useAuth, type User } from "../auth";
import { api } from "../api";
import { Icon } from "../components/Icon";
import { Brand } from "../components/Layout";
import { EXPIRED_KEY } from "../components/SessionTimeout";
import { fail, Field, Notice, useLoad, type Message } from "../components/ui";

interface DemoConfig {
  demo: boolean;
  logins: Array<{ username: string; password: string; description: string }>;
}

function expiredNotice(): Message | null {
  try {
    const minutes = sessionStorage.getItem(EXPIRED_KEY);
    if (!minutes) return null;
    return {
      kind: "info",
      text: `You were signed out after ${minutes} minutes without activity. Sign in again to carry on where you left off.`,
    };
  } catch {
    return null;
  }
}

/** Where to go after signing in: back to the page that asked for it, or the role's start page. */
function destination(user: User, from: Location | undefined): string {
  if (from && from.pathname !== "/login") return from.pathname + from.search;
  return user.role === "user" ? "/feedback" : "/";
}

export function Login() {
  const { user, signIn } = useAuth();
  const navigate = useNavigate();
  const from = (useLocation().state as { from?: Location } | null)?.from;
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const [capsLock, setCapsLock] = useState(false);
  const [showPassword, setShowPassword] = useState(false);
  const [busy, setBusy] = useState(false);
  const [message, setMessage] = useState<Message | null>(expiredNotice);
  const demo = useLoad(() => api.get<DemoConfig>("/api/auth/config"), []).data;

  useEffect(() => {
    document.title = "Sign in - Training Feedback";
    try {
      sessionStorage.removeItem(EXPIRED_KEY);
    } catch {
      // nothing to clear
    }
  }, []);

  if (user) return <Navigate to={destination(user, from)} replace />;

  async function submit(event: FormEvent) {
    event.preventDefault();
    setBusy(true);
    setMessage(null);
    try {
      const signedIn = await signIn(username, password);
      navigate(destination(signedIn, from), { replace: true });
    } catch (error) {
      setMessage(fail(error));
    } finally {
      setBusy(false);
    }
  }

  return (
    <div className="login">
      <aside className="login-aside">
        <Brand />
        <motion.div
          className="login-pitch"
          initial={{ opacity: 0, y: 16 }}
          animate={{ opacity: 1, y: 0 }}
          transition={{ duration: 0.5, delay: 0.08, ease: [0.2, 0.8, 0.2, 1] }}
        >
          <span className="eyebrow">GROWTH STARTS WITH LISTENING</span>
          <h1>Better feedback.<br />Stronger teams.<br /><span>Lasting impact.</span></h1>
          <p className="login-intro">Your learning journey, connected. One workspace to share feedback and shape what comes next.</p>
          <ul>
            <li>
              <Icon name="edit" />
              Record feedback for the trainings you attended
            </li>
            <li>
              <Icon name="users" />
              Rate trainees and review supervisor ratings
            </li>
            <li>
              <Icon name="chart" />
              Run session, trainer and effectiveness reports
            </li>
          </ul>
          <div className="login-signature"><span /> LEARN. REFLECT. GROW.</div>
        </motion.div>
        <p className="login-legal">&copy; Hexaware Technologies Ltd. All rights reserved.</p>
      </aside>
      <main className="login-main">
        <motion.form
          className="login-card"
          onSubmit={submit}
          initial={{ opacity: 0, y: 16 }}
          animate={{ opacity: 1, y: 0 }}
          transition={{ duration: 0.4, ease: [0.2, 0.8, 0.2, 1] }}
        >
          <div className="login-heading">
            <span className="eyebrow">HEXAWARE · TRAINING FEEDBACK</span>
            <h2>Welcome back</h2>
            <p className="muted">
              {demo?.demo ? "Pick a demo login below to explore." : "Use your StationH user ID and password."}
            </p>
          </div>
          <Notice message={message} />
          <Field label="User ID">
            <input
              value={username}
              onChange={(e) => setUsername(e.target.value)}
              autoComplete="username"
              autoFocus
              required
            />
          </Field>
          <Field label="Password" hint={capsLock ? "Caps lock is on" : undefined}>
            <span className="password-field">
              <input
                type={showPassword ? "text" : "password"}
                value={password}
                onChange={(e) => setPassword(e.target.value)}
                onKeyUp={(e) => setCapsLock(e.getModifierState("CapsLock"))}
                autoComplete="current-password"
                required
              />
              <button
                type="button"
                className="password-toggle"
                aria-label={showPassword ? "Hide password" : "Show password"}
                aria-pressed={showPassword}
                onClick={() => setShowPassword(!showPassword)}
              >
                <Icon name={showPassword ? "eyeOff" : "eye"} size={18} />
              </button>
            </span>
          </Field>
          <button className="button button-large" type="submit" disabled={busy}>
            {busy ? "Signing in..." : "Sign in"}
            <Icon name="chevron" size={18} />
          </button>
          <p className="login-auth-note"><Icon name="check" size={15} /> {demo?.demo ? "Demo workspace · Sample data only" : "Sign in with your StationH credentials"}</p>
          {demo?.demo && (
            <div className="demo-logins">
              <p>
                <strong>Demo mode.</strong> Sample data only; changes are lost when the server restarts. Every
                login uses the password <code>{demo.logins[0]?.password}</code>.
              </p>
              <ul>
                {demo.logins.map((login) => (
                  <li key={login.username}>
                    <button
                      type="button"
                      onClick={() => {
                        setUsername(login.username);
                        setPassword(login.password);
                      }}
                    >
                      <strong>{login.username}</strong>
                      <small>{login.description}</small>
                    </button>
                  </li>
                ))}
              </ul>
            </div>
          )}
        </motion.form>
      </main>
    </div>
  );
}
