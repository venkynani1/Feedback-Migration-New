import { createContext, useContext, useEffect, useMemo, useRef, useState, type ReactNode } from "react";

interface ConfirmOptions {
  title?: string;
  confirmLabel?: string;
  danger?: boolean;
}

interface Dialogs {
  /** Ask a yes/no question; resolves true when confirmed. */
  confirm: (message: string, options?: ConfirmOptions) => Promise<boolean>;
  /** Show a message that has to be acknowledged before carrying on. */
  notify: (message: string, title?: string) => Promise<void>;
}

interface Request extends ConfirmOptions {
  message: string;
  acknowledgeOnly: boolean;
  resolve: (confirmed: boolean) => void;
}

const DialogContext = createContext<Dialogs | null>(null);

export function DialogProvider({ children }: { children: ReactNode }) {
  const [request, setRequest] = useState<Request | null>(null);
  const element = useRef<HTMLDialogElement>(null);

  useEffect(() => {
    if (request && element.current && !element.current.open) element.current.showModal();
  }, [request]);

  function close(confirmed: boolean) {
    element.current?.close();
    request?.resolve(confirmed);
    setRequest(null);
  }

  const dialogs = useMemo<Dialogs>(
    () => ({
      confirm: (message, options) =>
        new Promise((resolve) => setRequest({ ...options, message, acknowledgeOnly: false, resolve })),
      notify: (message, title) =>
        new Promise((resolve) =>
          setRequest({ title, message, acknowledgeOnly: true, resolve: () => resolve() }),
        ),
    }),
    [],
  );

  return (
    <DialogContext.Provider value={dialogs}>
      {children}
      {request && (
        <dialog
          ref={element}
          className="dialog"
          aria-labelledby="dialog-title"
          onCancel={(event) => {
            event.preventDefault();
            close(false);
          }}
        >
          <h2 id="dialog-title">{request.title ?? (request.acknowledgeOnly ? "Training Feedback" : "Please confirm")}</h2>
          <p>{request.message}</p>
          <div className="dialog-actions">
            {!request.acknowledgeOnly && (
              <button className="button button-quiet" type="button" onClick={() => close(false)}>
                Cancel
              </button>
            )}
            <button
              className={`button${request.danger ? " button-danger" : ""}`}
              type="button"
              autoFocus
              onClick={() => close(true)}
            >
              {request.confirmLabel ?? "OK"}
            </button>
          </div>
        </dialog>
      )}
    </DialogContext.Provider>
  );
}

export function useDialogs(): Dialogs {
  const dialogs = useContext(DialogContext);
  if (!dialogs) throw new Error("useDialogs must be used inside DialogProvider");
  return dialogs;
}
