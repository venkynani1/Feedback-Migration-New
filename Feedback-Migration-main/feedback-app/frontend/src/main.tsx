import "@fontsource-variable/inter";
import { MotionConfig } from "framer-motion";
import { Component, StrictMode, type ReactNode } from "react";
import { createRoot } from "react-dom/client";
import { BrowserRouter } from "react-router-dom";
import { App } from "./App";
import { AuthProvider } from "./auth";
import { DialogProvider } from "./components/Dialog";
import { initTheme } from "./theme";
import "./styles.css";

initTheme();

/** Keeps a rendering fault from leaving the user with a blank page. */
class ErrorBoundary extends Component<{ children: ReactNode }, { failed: boolean }> {
  state = { failed: false };

  static getDerivedStateFromError() {
    return { failed: true };
  }

  componentDidCatch(error: unknown) {
    console.error(error);
  }

  render() {
    if (!this.state.failed) return this.props.children;
    return (
      <div className="fatal">
        <h1>Something went wrong</h1>
        <p>An unexpected error occurred. Please reload the page, or contact the system administrator if it keeps happening.</p>
        <button className="button" type="button" onClick={() => window.location.reload()}>
          Reload
        </button>
      </div>
    );
  }
}

createRoot(document.getElementById("root")!).render(
  <StrictMode>
    <ErrorBoundary>
      <MotionConfig reducedMotion="user">
        <BrowserRouter>
          <AuthProvider>
            <DialogProvider>
              <App />
            </DialogProvider>
          </AuthProvider>
        </BrowserRouter>
      </MotionConfig>
    </ErrorBoundary>
  </StrictMode>,
);
