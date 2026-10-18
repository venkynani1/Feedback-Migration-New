import { useCallback, useEffect, useMemo, useRef, useState, type ReactNode } from "react";
import { useLocation } from "react-router-dom";
import { errorText, type Grid } from "../api";
import { findNav } from "../nav";
import { Icon, type IconName } from "./Icon";

export interface Message {
  kind: "error" | "success" | "info";
  text: string;
}

export const ok = (text: string): Message => ({ kind: "success", text });
export const fail = (error: unknown): Message => ({ kind: "error", text: errorText(error) });

const NOTICE_ICONS: Record<Message["kind"], IconName> = { error: "error", success: "check", info: "info" };

export function Notice({ message }: { message: Message | null }) {
  if (!message) return null;
  return (
    <div className={`notice notice-${message.kind}`} role={message.kind === "error" ? "alert" : "status"}>
      <Icon name={NOTICE_ICONS[message.kind]} />
      <span>{message.text}</span>
    </div>
  );
}

export function Loading({ label = "Loading..." }: { label?: string }) {
  return (
    <p className="loading" role="status">
      <span className="spinner" aria-hidden="true" />
      {label}
    </p>
  );
}

interface PageProps {
  title: string;
  /** One line under the title saying what the page is for. */
  description?: string;
  children: ReactNode;
}

let headingFocusPending = false;

/** After navigation, move focus to the next page's heading once it is on screen. */
export function requestHeadingFocus(container: HTMLElement | null) {
  const heading = container?.querySelector<HTMLElement>("h1");
  if (heading) focusHeading(heading);
  else headingFocusPending = true; // the page's code is still loading
}

function focusHeading(heading: HTMLElement) {
  headingFocusPending = false;
  heading.tabIndex = -1;
  heading.focus({ preventScroll: true });
}

/** For a page heading: takes the focus when it appears after a navigation. */
export function useHeadingFocus() {
  const heading = useRef<HTMLHeadingElement>(null);
  useEffect(() => {
    if (headingFocusPending && heading.current) focusHeading(heading.current);
  }, []);
  return heading;
}

export function Page({ title, description, children }: PageProps) {
  const { pathname } = useLocation();
  const nav = findNav(pathname);
  const heading = useHeadingFocus();
  useEffect(() => {
    document.title = `${title} - Training Feedback`;
  }, [title]);

  const trail = nav ? [nav.group.label, nav.item.label] : [];
  if (trail[trail.length - 1] !== title) trail.push(title);
  // The menu's description fits the menu page itself, not the pages under it.
  const summary = description ?? (nav?.item.to === pathname ? nav.item.description : undefined);

  return (
    <section className="page">
      <header className="page-header">
        {trail.length > 1 && (
          <ol className="breadcrumb" aria-label="Breadcrumb">
            {trail.map((step, i) => (
              <li key={i} aria-current={i === trail.length - 1 ? "page" : undefined}>
                {step}
              </li>
            ))}
          </ol>
        )}
        <h1 ref={heading} tabIndex={-1}>
          {title}
        </h1>
        {summary && <p className="page-description">{summary}</p>}
      </header>
      {children}
    </section>
  );
}

export function Card({ title, children }: { title?: string; children: ReactNode }) {
  return (
    <div className="card">
      {title && (
        <div className="card-header">
          <h2>{title}</h2>
        </div>
      )}
      <div className="card-body">{children}</div>
    </div>
  );
}

interface FieldProps {
  label: string;
  children: ReactNode;
  hint?: string;
  required?: boolean;
}

export function Field({ label, children, hint, required }: FieldProps) {
  return (
    <label className="field">
      <span className="field-label">
        {label}
        {required && (
          <span className="field-required" aria-hidden="true">
            *
          </span>
        )}
      </span>
      {children}
      {hint && <span className="field-hint">{hint}</span>}
    </label>
  );
}

interface SelectProps {
  value: string;
  onChange: (value: string) => void;
  options: Array<string | { value: string; label: string }>;
  placeholder?: string;
  disabled?: boolean;
}

export function Select({ value, onChange, options, placeholder = "--Select--", disabled }: SelectProps) {
  return (
    <select value={value} onChange={(e) => onChange(e.target.value)} disabled={disabled}>
      <option value="">{placeholder}</option>
      {options.map((option) => {
        const item = typeof option === "string" ? { value: option, label: option } : option;
        return (
          <option key={item.value} value={item.value}>
            {item.label}
          </option>
        );
      })}
    </select>
  );
}

type Tone = "success" | "danger" | "info" | "neutral";

const STATUS_TONES: Record<string, Tone> = {
  delivered: "success",
  approved: "success",
  failed: "danger",
  rejected: "danger",
  "submitted for review": "info",
  saved: "neutral",
};

export function Badge({ tone, children }: { tone: Tone; children: ReactNode }) {
  return <span className={`badge badge-${tone}`}>{children}</span>;
}

function cell(value: unknown): string {
  return value === null || value === undefined ? "" : String(value);
}

const PAGE_SIZES = [25, 50, 100];
const FILTER_FROM_ROWS = 10;
const collator = new Intl.Collator(undefined, { numeric: true, sensitivity: "base" });

type Sort = { index: number; dir: "asc" | "desc" } | null;

/** Unsorted, then ascending, then descending, then back to unsorted. */
function nextSort(sort: Sort, index: number): Sort {
  if (sort?.index !== index) return { index, dir: "asc" };
  return sort.dir === "asc" ? { index, dir: "desc" } : null;
}

interface DataGridProps {
  grid: Grid | null;
  /** Columns to show, in order, with their headings. Omit to show every column. */
  show?: Array<[column: string, heading: string]>;
  serial?: boolean;
  empty?: string;
}

export function DataGrid({ grid, show, serial, empty = "No records to display." }: DataGridProps) {
  const [filter, setFilter] = useState("");
  const [pageSize, setPageSize] = useState(PAGE_SIZES[0]);
  const [page, setPage] = useState(0);
  const [sort, setSort] = useState<Sort>(null);

  const picked = useMemo(() => {
    if (!grid) return [];
    const lower = grid.columns.map((c) => c.toLowerCase());
    return show
      ? show
          .map(([column, heading]) => ({ index: lower.indexOf(column.toLowerCase()), heading }))
          .filter((c) => c.index >= 0)
      : grid.columns.map((heading, index) => ({ index, heading }));
  }, [grid, show]);

  const rows = useMemo(() => {
    if (!grid) return [];
    const wanted = filter.trim().toLowerCase();
    const matching = wanted
      ? grid.rows.filter((row) => picked.some((c) => cell(row[c.index]).toLowerCase().includes(wanted)))
      : grid.rows;
    if (!sort) return matching;
    const sign = sort.dir === "asc" ? 1 : -1;
    return [...matching].sort((a, b) => sign * collator.compare(cell(a[sort.index]), cell(b[sort.index])));
  }, [grid, picked, filter, sort]);

  useEffect(() => setPage(0), [grid, filter, pageSize, sort]);

  if (!grid) return null;
  if (grid.rows.length === 0) {
    return (
      <div className="empty">
        <Icon name="inbox" size={28} />
        <p>{empty}</p>
      </div>
    );
  }

  const pages = Math.max(1, Math.ceil(rows.length / pageSize));
  const first = Math.min(page, pages - 1) * pageSize;
  const visible = rows.slice(first, first + pageSize);

  return (
    <div className="grid">
      {grid.rows.length > FILTER_FROM_ROWS && (
        <div className="grid-toolbar">
          <label className="grid-filter">
            <Icon name="search" size={16} />
            <input
              type="search"
              placeholder="Filter rows"
              aria-label="Filter rows"
              value={filter}
              onChange={(e) => setFilter(e.target.value)}
            />
          </label>
        </div>
      )}
      <div className="table-wrap">
        <table>
          <thead>
            <tr>
              {serial && <th className="col-serial">S.No</th>}
              {picked.map((c) => {
                const dir = sort?.index === c.index ? sort.dir : null;
                return (
                  <th key={c.index} aria-sort={dir ? (dir === "asc" ? "ascending" : "descending") : undefined}>
                    <button type="button" className="th-sort" onClick={() => setSort(nextSort(sort, c.index))}>
                      {c.heading}
                      <Icon name={dir === "asc" ? "sortUp" : dir === "desc" ? "sortDown" : "sort"} size={14} />
                    </button>
                  </th>
                );
              })}
            </tr>
          </thead>
          <tbody>
            {visible.map((row, r) => (
              <tr key={first + r}>
                {serial && <td className="col-serial">{first + r + 1}</td>}
                {picked.map((c) => {
                  const text = cell(row[c.index]);
                  const tone = c.heading.toLowerCase() === "status" ? STATUS_TONES[text.toLowerCase()] : undefined;
                  return <td key={c.index}>{tone ? <Badge tone={tone}>{text}</Badge> : text}</td>;
                })}
              </tr>
            ))}
            {visible.length === 0 && (
              <tr>
                <td className="grid-none" colSpan={picked.length + (serial ? 1 : 0)}>
                  No rows match the filter.
                </td>
              </tr>
            )}
          </tbody>
        </table>
      </div>
      <div className="grid-footer">
        <span>
          {rows.length === grid.rows.length
            ? `${rows.length} ${rows.length === 1 ? "record" : "records"}`
            : `${rows.length} of ${grid.rows.length} records`}
        </span>
        {grid.rows.length > PAGE_SIZES[0] && (
          <div className="pager">
            <label>
              Rows per page
              <select value={pageSize} onChange={(e) => setPageSize(Number(e.target.value))}>
                {PAGE_SIZES.map((size) => (
                  <option key={size}>{size}</option>
                ))}
              </select>
            </label>
            <button className="button button-quiet button-small" type="button" disabled={first === 0} onClick={() => setPage(first / pageSize - 1)}>
              Previous
            </button>
            <span>
              Page {first / pageSize + 1} of {pages}
            </span>
            <button
              className="button button-quiet button-small"
              type="button"
              disabled={first + pageSize >= rows.length}
              onClick={() => setPage(first / pageSize + 1)}
            >
              Next
            </button>
          </div>
        )}
      </div>
    </div>
  );
}

/** Load data when the dependencies change; null inputs skip the request. */
export function useLoad<T>(loader: (() => Promise<T>) | null, deps: unknown[]) {
  const [data, setData] = useState<T | null>(null);
  const [error, setError] = useState<string | null>(null);
  const [loading, setLoading] = useState(false);
  const [version, setVersion] = useState(0);

  useEffect(() => {
    if (!loader) {
      setData(null);
      setError(null);
      return;
    }
    let current = true;
    setLoading(true);
    setError(null);
    loader()
      .then((result) => current && setData(result))
      .catch((e) => {
        if (!current) return;
        setData(null);
        setError(errorText(e));
      })
      .finally(() => current && setLoading(false));
    return () => {
      current = false;
    };
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [...deps, version]);

  const reload = useCallback(() => setVersion((v) => v + 1), []);
  return { data, error, loading, reload };
}
