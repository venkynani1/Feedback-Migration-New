import { useEffect, useState, type FormEvent } from "react";
import { Navigate, useNavigate } from "react-router-dom";
import { useAuth } from "../auth";
import { api } from "../api";
import { Icon } from "../components/Icon";
import { Brand } from "../components/Layout";
import { fail, Field, Notice, useLoad, type Message } from "../components/ui";

interface DemoConfig {
  demo: boolean;
  logins: Array<{ username: string; password: string; description: string }>;
}

export function Login() {
  const { user, signIn } = useAuth();
  const navigate = useNavigate();
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const [capsLock, setCapsLock] = useState(false);
  const [busy, setBusy] = useState(false);
  const [message, setMessage] = useState<Message | null>(null);
  const demo = useLoad(() => api.get<DemoConfig>("/api/auth/config"), []).data;

  useEffect(() => {
    document.title = "Sign in - Training Feedback";
  }, []);

  if (user) return <Navigate to={user.role === "user" ? "/feedback" : "/"} replace />;

  async function submit(event: FormEvent) {
    event.preventDefault();
    setBusy(true);
    setMessage(null);
    try {
      const signedIn = await signIn(username, password);
      navigate(signedIn.role === "user" ? "/feedback" : "/", { replace: true });
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
        <div className="login-pitch">
          <h1>Training feedback, ratings and reports in one place.</h1>
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
        </div>
        <p className="login-legal">&copy; Hexaware Technologies Ltd. All rights reserved.</p>
      </aside>
      <main className="login-main">
        <form className="login-card" onSubmit={submit}>
          <div className="login-heading">
            <h2>Sign in</h2>
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
            <input
              type="password"
              value={password}
              onChange={(e) => setPassword(e.target.value)}
              onKeyUp={(e) => setCapsLock(e.getModifierState("CapsLock"))}
              autoComplete="current-password"
              required
            />
          </Field>
          <button className="button button-large" type="submit" disabled={busy}>
            {busy ? "Signing in..." : "Sign in"}
          </button>
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
        </form>
      </main>
    </div>
  );
}
