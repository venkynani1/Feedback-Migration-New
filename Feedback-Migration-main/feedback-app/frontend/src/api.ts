export class ApiError extends Error {
  constructor(
    public status: number,
    message: string,
  ) {
    super(message);
  }
}

export type Params = Record<string, string | number | boolean | undefined | null>;

/** A result set as the API returns it: column names plus rows of values. */
export interface Grid {
  columns: string[];
  rows: unknown[][];
}

// Every answer from the server renews the session cookie, so the idle clock
// restarts with each request. Tabs share the time so one busy tab keeps the
// others from warning.
const ACTIVITY_KEY = "feedback.activity";
let lastActivity = Date.now();

function touch() {
  lastActivity = Date.now();
  try {
    localStorage.setItem(ACTIVITY_KEY, String(lastActivity));
  } catch {
    // storage unavailable: this tab still keeps its own time
  }
}

/** When the server last renewed the session, in any tab. */
export function lastServerActivity(): number {
  let shared = 0;
  try {
    shared = Number(localStorage.getItem(ACTIVITY_KEY)) || 0;
  } catch {
    // storage unavailable
  }
  return Math.max(lastActivity, shared);
}

function url(path: string, params?: Params): string {
  const query = new URLSearchParams();
  for (const [key, value] of Object.entries(params ?? {})) {
    if (value !== undefined && value !== null) query.set(key, String(value));
  }
  const text = query.toString();
  return text ? `${path}?${text}` : path;
}

async function failure(response: Response): Promise<ApiError> {
  let message = `Request failed (${response.status})`;
  try {
    const data = await response.json();
    if (typeof data.detail === "string") message = data.detail;
    else if (Array.isArray(data.detail)) message = "Please check the values entered.";
  } catch {
    // not JSON; keep the generic message
  }
  if (response.status === 401 && !response.url.endsWith("/api/auth/login")) {
    window.dispatchEvent(new Event("feedback:signed-out"));
  }
  return new ApiError(response.status, message);
}

async function send<T>(method: string, path: string, params?: Params, body?: unknown): Promise<T> {
  const headers: Record<string, string> = { "X-Requested-With": "XMLHttpRequest" };
  let payload: BodyInit | undefined;
  if (body instanceof FormData) {
    payload = body;
  } else if (body !== undefined) {
    headers["Content-Type"] = "application/json";
    payload = JSON.stringify(body);
  }
  const response = await fetch(url(path, params), {
    method,
    headers,
    body: payload,
    credentials: "same-origin",
  });
  if (!response.ok) throw await failure(response);
  touch();
  return (await response.json()) as T;
}

export const api = {
  get: <T>(path: string, params?: Params) => send<T>("GET", path, params),
  post: <T>(path: string, body?: unknown) => send<T>("POST", path, undefined, body ?? {}),
  put: <T>(path: string, body?: unknown) => send<T>("PUT", path, undefined, body ?? {}),

  /** Fetch a file and hand it to the browser as a download. */
  async download(path: string, params: Params, fallbackName: string): Promise<void> {
    const response = await fetch(url(path, params), { credentials: "same-origin" });
    if (!response.ok) throw await failure(response);
    touch();
    const named = /filename="?([^";]+)"?/.exec(response.headers.get("Content-Disposition") ?? "");
    const link = document.createElement("a");
    link.href = URL.createObjectURL(await response.blob());
    link.download = named?.[1] ?? fallbackName;
    document.body.appendChild(link);
    link.click();
    link.remove();
    setTimeout(() => URL.revokeObjectURL(link.href), 1000);
  },
};

export function errorText(error: unknown): string {
  return error instanceof Error ? error.message : "Something went wrong.";
}

/** Read a field from a record without caring about the column's letter case. */
export function field(row: Record<string, unknown>, name: string): string {
  const key = Object.keys(row).find((k) => k.toLowerCase() === name.toLowerCase());
  const value = key === undefined ? undefined : row[key];
  return value === null || value === undefined ? "" : String(value);
}
