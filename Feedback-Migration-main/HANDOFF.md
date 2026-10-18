# Handoff: Training Feedback migration

Context for whoever (person or AI assistant) continues this work. Last updated 2026-10-18.

## What this project is

The Hexaware **Training Feedback** tool, migrated from **ASP.NET Web Forms (.NET Framework 4.0)** to
**FastAPI (Python) + React (TypeScript, Vite)**.

- The legacy app is in `Production/Application` and the database scripts are in `Production/Database`. Both are kept for reference. Do not edit them.
- The new app is in `feedback-app/`. Read `feedback-app/README.md` first; it is the authoritative doc for setup, deployment, the old-page-to-new-route map and the deliberate behaviour differences.
- The **SQL Server database is unchanged**. The new back end calls the same stored procedures with the same parameters.
- **Sign-in still goes through the StationH SOAP service** (`IsValidUser`). The user required this: "keep soap login service since we need stationh authentications". Do not replace it.

## Git state

- Branch `fastapi-react-migration` is pushed to origin. Its commits:
  - `e8cdcc4`: the migration.
  - `b2bd8c9`: fixes plus security hardening.
- The working tree was clean at handoff. `master` is the main branch.
- No pull request has been opened yet.
- `git` is not on the PATH of the PowerShell used by the last assistant; the user ran some pushes themselves.

## Architecture

### Back end (`feedback-app/backend/app`)

| File | Purpose |
|---|---|
| `main.py` | App factory and SessionMiddleware (signed cookie). Also sets the security headers (CSP, X-Frame-Options, nosniff, Referrer-Policy, `Cache-Control: no-store` on `/api`), the rotating log file (5 MB × 10), `/api/health` and the refuse-to-start check. Serves `frontend/dist` as the SPA. |
| `config.py` | pydantic-settings with prefix `FEEDBACK_`. `environment` defaults to `production`. `production_problems()` lists unsafe settings: demo on, auth_mode not `soap`, cookie not secure, secret shorter than 32 characters, or an empty DB string. In production those problems stop startup with a RuntimeError, and the docs and OpenAPI pages are off. |
| `db.py` | pyodbc wrapper with the same semantics as the old ADO.NET helpers: a None parameter is skipped so the proc uses its default; `call_proc_rowcount` sums rowcounts across `nextset()` (ExecuteNonQuery); scalar means first cell (ExecuteScalar). |
| `deps.py` | `current_user`, `require_admin`. A CSRF guard requires the `X-Requested-With` header on API calls. |
| `soap_auth.py` | httpx SOAP client for StationH `IsValidUser`. |
| `mailer.py` | Notification mail. Data is saved first; a mail failure is reported to the user and logged. |
| `exports.py` | `.xlsx` (openpyxl) and PDF (ReportLab) exports. |
| `demo.py` | In-memory `DemoDatabase` that imitates the procs for demo mode. |
| `routers/auth.py` | Login and logout. Roles are `user`, `admin` and `super` (from `IsSuperAdmin`). Failed logins are locked out in memory: 5 failures give a 15-minute 429, keyed by the lower-case username and cleared on success. Because the state is in memory, run **one worker**. |
| `routers/feedback.py`, `sessions.py`, `nominations.py`, `masters.py`, `reports.py`, `skillport.py`, `lookups.py` | One router per area of the old site, under `/api/...`. |

Notes on individual routers:

- **`skillport.py`:** uploads go to `FEEDBACK_UPLOAD_DIR` (default `\\CCH1WPNTDB\StationhReportSource`, which SQL Server reads), named `TrainingFeedback_<emp>_<timestamp>_<stem>.xls`. The file is deleted after import.
- **`masters.py`:** a course is refused if its ID **or** its name already exists. The responses are 409 "Course ID already exist", "Course Name already exist" and "Course Already exist".
- **`reports.py`:** `overall_trainer_average` deliberately keeps a legacy quirk; see the README.

### Front end (`feedback-app/frontend/src`)

- The stack is React 18, react-router-dom 7.18 in declarative mode with BrowserRouter, and Vite 8 (Node 22.12 or later).
- `api.ts` is a fetch wrapper that sends `X-Requested-With`. `auth.tsx` provides the auth context (user, year, signIn).
- `nav.ts` holds `ADMIN_NAV` and `SUPER_ADMIN_NAV`. They are used by `Layout.tsx` (side bar) and `Home.tsx` (tiles).
- `components/ui.tsx` has the shared parts: DataGrid with filtering and paging, Page, Card, Field, Notice, `useLoad` and `fail`. `Dialog.tsx` provides DialogProvider and `useDialogs`, used for in-page confirms instead of browser pop-ups. `Icon.tsx` has inline SVG icons.
- `main.tsx` wraps the app in an ErrorBoundary and DialogProvider.
- The pages are `Login`, `Home`, `Feedback`, `Masters`, `Sessions`, `Reports`, `Upload`, plus `SessionPicker.tsx`.
- `styles.css` holds the enterprise restyle, which is responsive and was checked at 1440px and 390px.

### Tests (`feedback-app/backend/tests`)

- **67 tests, all passing.** Run them with `.venv\Scripts\python -m pytest` from `backend`. Browser tests: `npm run test:e2e` from `frontend` (see the section on improvements below).
- `conftest.py` forces `FEEDBACK_ENVIRONMENT=development` and `FEEDBACK_DEMO_MODE=false`. This matters because the local `.env` has demo on.
- The DB is faked with a `FakeCursor`.
- The test files are `test_api.py`, `test_core.py`, `test_demo.py`, `test_static.py` and `test_security.py` (lockout, `production_problems`, docs hidden, health, headers).

## Running locally

```powershell
# back end (from feedback-app\backend)
.\.venv\Scripts\python.exe -m uvicorn app.main:app --host 127.0.0.1 --port 8000
# front end dev server (optional; from feedback-app\frontend). It proxies /api to 8000.
npm run dev   # http://localhost:5173
```

- The local `backend/.env` is git-ignored. It sets `FEEDBACK_ENVIRONMENT=development`, `FEEDBACK_DEMO_MODE=true` and `FEEDBACK_COOKIE_SECURE=false`, plus a secret key, the DB string, the SOAP URL and the upload dir. `backend/.env.example` documents every setting.
- In **demo mode** the logins are `admin`, `employee`, `supervisor` and `manager`, all with the password `demo`. It uses in-memory data, sends no mail and makes no DB or SOAP calls. Changes reset on restart.
- If `frontend/dist` is built, the back end serves the whole app on port 8000.
- `.vscode/settings.json` points Pylance at `feedback-app/backend/.venv` and adds `feedback-app/backend` to the analysis paths.

## Verified so far

- Every legacy page was audited against its new route. Behaviour matches, except for the deliberate fixes listed under "Differences" in the README.
- All four demo roles were walked through in headless Chrome. There were no console errors (apart from the expected 401 before login), no 5xx responses and no horizontal overflow on mobile.
- A security review was done: the hardening above, plus `npm audit` was cleaned by upgrading dependencies.

## Not verified: needs the corporate network

The real database, StationH and the mail server can only be reached on the corporate network, so they have **never been tested** with the new app. What to do there:

1. Install **ODBC Driver 18 for SQL Server**, then set a real `FEEDBACK_DB_CONNECTION_STRING`. The local `.env` currently uses the old `{SQL Server}` driver.
2. Confirm these procs exist on the production DB. They are called but there is no script for them in the repo:
   - `usp_GetFiveDigitEmpID`
   - `usp_Insertfeedbacks_V1`
   - `usp_getFeedbackForOverallrpt_V1`
   - `usp_rATINGForOverallrpt_V1`
   - `usp_getFeedbackdetQuesansEmployeeid_V1`
   - `GetSkillportAssessmentReport`

   Also, the `usp_InsertSupFeedbacks` script in the repo is older than the version the code calls (`@TechSkills`/`@SoftSkills`). Treat the database as the reference, not the script.
3. Walk through each role end to end:
   - sign in
   - record feedback
   - submit and review supervisor ratings
   - send a nomination mail
   - run every report and export
   - load one Excel upload
4. Deploy behind HTTPS (IIS or another reverse proxy) with `FEEDBACK_ENVIRONMENT=production`, a fresh secret, `COOKIE_SECURE=true`, `DEMO_MODE=false`, `AUTH_MODE=soap` and `FEEDBACK_LOG_FILE` set. Point the health check at `/api/health`; `/api/health/ready` is the quickest first check that the database connection works. **Mail is sent by SQL Server (`usp_sendmail`), so testing nominations or ratings against a live database emails real people**: use a test database or nominate only testers. Run a single worker. The service account needs the same DB rights as the old IIS app pool (Windows-integrated auth) and write access to the upload share.
5. **Rotate the database `sa` passwords.** They are hard-coded in the legacy source: a commented-out connection string in `Web.config` and one in `HomeMenu.aspx.cs`.

## Possible next steps

- Open a PR from `fastapi-react-migration` to `master`.
- Optional: build a real Create/Remove Admin feature. The legacy Add button never worked, so only the employee search was ported.
- If you need more than one worker, move the login lockout state out of process memory, for example into the database or Redis.

## Conventions for further changes

- Keep stored-proc names and parameters unchanged. The DB is shared with nothing new.
- Match the existing code style: small routers, a `UserError(message, status)` for user-facing errors, and the shared UI components from `components/ui.tsx`.
- Add or adjust tests in `backend/tests` and keep the suite green.
- Never commit `backend/.env`.

## Improvements before hand-over to the test team

None of these change the database, stored procedures, StationH or mail.

- **Browser tests** (`frontend/e2e`, Playwright in the installed Chrome): `npm run test:e2e` builds, starts the back end in demo mode on port 8001 and covers sign-in, every admin page, workspace search, table sorting, the theme switch, employee access limits, recording feedback with draft restore, and the idle-session warning (with a fake clock). 10 tests.
- **CI**: `.github/workflows/ci.yml` at the repository root (one level above this folder) runs the back-end tests and the browser tests on Windows for every push and pull request.
- **Idle sessions**: a warning two minutes before the 30-minute timeout, a clean sign-out with the reason shown, and a return to the same page after signing in again (`components/SessionTimeout.tsx`; the timeout comes from `/api/auth/config`).
- **Feedback drafts**: the Record My Feedback form keeps unsubmitted answers in session storage per employee and session, restores them after a reload or timeout, and clears them on submit or close.
- **Readiness check**: `/api/health/ready` runs `SELECT 1` and answers 503 when the database is unreachable (`db.ping()`); `/api/health` is unchanged.
- **Faster first load**: each page's code loads when first opened (`App.tsx`).
- **Typos** in user-facing text fixed. Stored procedure and column names such as `usp_getFeedBackCount` and `Pending FeedBack` are database names and stay as they are.
- **Caching**: `index.html` is served `no-cache` and the hashed `/assets` files are cached as immutable, so a new release shows without a forced reload.
- **Theme**: light blue and white (Hexaware-style; replace the blue tokens at the top of `styles.css` with the official brand values if needed) with a navy dark mode.
- Back-end tests: **67**, all passing.

## UI refresh — 2026-10-18

- Refreshed the existing React application with a token-based visual system (now light blue and white, see above), revised navigation and sign-in screen, clearer form/table styling, and responsive desktop/mobile layouts.
- Admin Home is now `Workspace overview`, with quick links and a searchable directory based on the existing role-aware navigation. Search is local and does not call a new API.
- The employee feedback page summarizes the existing response's session/review counts and has clearer empty states. These are counts of listed sessions/reviews, not counts of individual participants.
- Changes are confined to `frontend/src/styles.css`, `components/Layout.tsx`, `pages/Home.tsx`, `pages/Login.tsx`, and `pages/Feedback.tsx`. StationH SOAP authentication, API contracts, stored procedures, and legacy source are unchanged.
- Validation: production TypeScript/Vite build passed; all 65 backend tests passed. Run backend tests after the frontend build finishes, because Vite replaces `dist/assets`, which backend static-file tests access.
- Browser smoke checks covered 16 routes at 1440px and 390px, all four demo logins, tool search/no-results/reset, mobile menu navigation, and opening role-specific feedback forms. No JavaScript page errors, HTTP 5xx responses, or page-level horizontal overflow were found. Wide tables retain their own horizontal scrolling.
- Corporate database, SOAP, mail, and upload-share integration still require the corporate-network checks above. Demo smoke checks do not replace those checks.
