import { useEffect, useRef, useState } from "react";
import { api, lastServerActivity } from "../api";
import { useAuth } from "../auth";

/** Set before signing out for inactivity, so the sign-in page can say why. */
export const EXPIRED_KEY = "feedback.expired";

const WARN_MS = 2 * 60_000;

function clock(ms: number): string {
  const seconds = Math.max(0, Math.ceil(ms / 1000));
  return `${Math.floor(seconds / 60)}:${String(seconds % 60).padStart(2, "0")}`;
}

/**
 * The server ends a session after `minutes` without a request. Warn shortly
 * before that so nobody loses a half-written form, and sign out cleanly when
 * the time runs out.
 */
export function SessionTimeout({ minutes }: { minutes: number }) {
  const { signOut } = useAuth();
  const [left, setLeft] = useState<number | null>(null);
  const dialog = useRef<HTMLDialogElement>(null);

  useEffect(() => {
    const limit = minutes * 60_000;
    const warnAt = Math.min(WARN_MS, limit / 2);
    const timer = window.setInterval(() => {
      const remaining = limit - (Date.now() - lastServerActivity());
      if (remaining <= 0) {
        window.clearInterval(timer);
        try {
          sessionStorage.setItem(EXPIRED_KEY, String(minutes));
        } catch {
          // the sign-in page then shows no reason
        }
        void signOut();
      } else {
        setLeft(remaining <= warnAt ? remaining : null);
      }
    }, 1000);
    return () => window.clearInterval(timer);
  }, [minutes, signOut]);

  const warning = left !== null;
  useEffect(() => {
    const element = dialog.current;
    if (!element) return;
    if (warning && !element.open) element.showModal();
    if (!warning && element.open) element.close();
  }, [warning]);

  return (
    <dialog
      ref={dialog}
      className="dialog"
      aria-labelledby="timeout-title"
      aria-describedby="timeout-text"
      onCancel={(event) => {
        event.preventDefault();
        void api.get("/api/auth/me");
      }}
    >
      <h2 id="timeout-title">Are you still there?</h2>
      <p id="timeout-text">
        For your security you will be signed out in <strong className="countdown">{clock(left ?? 0)}</strong> because
        there has been no activity. Feedback you are part-way through is kept in this browser tab and comes back when
        you sign in again.
      </p>
      <div className="dialog-actions">
        <button className="button button-quiet" type="button" onClick={() => void signOut()}>
          Sign out
        </button>
        <button className="button" type="button" autoFocus onClick={() => void api.get("/api/auth/me")}>
          Stay signed in
        </button>
      </div>
    </dialog>
  );
}
