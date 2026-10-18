# Training Feedback

The Training Feedback tool, migrated from ASP.NET Web Forms (.NET Framework 4.0)
to a FastAPI back end and a React front end. It was ported from
`Production/Application` in this repository.

The SQL Server database is unchanged. The application calls the same stored
procedures as before, and sign-in still goes through the StationH SOAP
authentication service.

## Layout

| Path | Contents |
|---|---|
| `backend/app` | FastAPI application: one router per area of the old site |
| `backend/tests` | Tests that run against a stand-in for the database |
| `backend/assets` | Sample participant list and the logo used on the PDF |
| `frontend/src` | React (TypeScript, Vite) single-page application |

## Requirements

- Python 3.12 or later
- Node.js 22.12 or later (only to build the front end)
- Microsoft ODBC Driver 18 for SQL Server on the machine running the back end
- Network access to the `TrainingFeedback` database, the StationH
  authentication service and the upload share

## Configuration

Copy `backend/.env.example` to `backend/.env` and fill it in. Every setting is
described in that file. `FEEDBACK_SECRET_KEY` is required; the application will
not start without it.

`FEEDBACK_UPLOAD_DIR` matters for the Excel Data Upload page: the import stored
procedures read the uploaded workbook themselves, so the folder must be a path
that SQL Server can open, and the account running the back end needs write
access to it. The default is the share the old application wrote to. Files are
saved there as `TrainingFeedback_<employee>_<time>_<name>.xls` and deleted once
imported.

`FEEDBACK_ENVIRONMENT` defaults to `production`. In production the application
refuses to start if demo mode or the password-less dev sign-in is on, the
cookie is not marked secure, the secret key is shorter than 32 characters or
the database connection is empty, and the interactive API pages (`/api/docs`)
are switched off. Set it to `development` on your own machine.

### Demo mode

`FEEDBACK_DEMO_MODE=true` runs the application on built-in sample data, with no
database, StationH service or mail. The sign-in page lists four demo logins
(`admin`, `employee`, `supervisor`, `manager`; password `demo`) and every page
shows a demo banner. Changes are lost when the server restarts. It is for
trying the screens out away from the corporate network and must stay off in
production; it is off unless set.

## Run locally

```powershell
cd backend
python -m venv .venv
.venv\Scripts\pip install -r requirements-dev.txt
.venv\Scripts\uvicorn app.main:app --reload --port 8000
```

```powershell
cd frontend
npm install
npm run dev
```

Open http://localhost:5173. The dev server forwards `/api` to port 8000. For
local work over plain http set `FEEDBACK_ENVIRONMENT=development` and
`FEEDBACK_COOKIE_SECURE=false`.

## Deploy

```powershell
cd frontend
npm ci
npm run build
```

The back end serves `frontend/dist` itself, so one process hosts the whole
application:

```powershell
cd backend
.venv\Scripts\uvicorn app.main:app --host 0.0.0.0 --port 8000
```

Run it behind IIS or another reverse proxy that terminates HTTPS, and keep
`FEEDBACK_COOKIE_SECURE=true`. The database connection used Windows-integrated
security in the old application; to keep that, run the service under an account
that has the same database rights as the old IIS application pool.

### Before going live

- `FEEDBACK_ENVIRONMENT=production` with a fresh `FEEDBACK_SECRET_KEY`; the
  application then checks the other safety settings itself at start-up.
- Set `FEEDBACK_LOG_FILE` so errors are kept once it runs as a service, and
  point the proxy's health check at `/api/health`. A monitor that should also
  notice a lost database connection can use `/api/health/ready`.
- Run a single worker process, or accept that the failed sign-in lock
  (5 attempts, then 15 minutes) is counted per process.
- Check on the production database that these procedures exist. The
  application calls them, as the old one did, but this repository has no
  script for them: `usp_GetFiveDigitEmpID`, `usp_Insertfeedbacks_V1`,
  `usp_getFeedbackForOverallrpt_V1`, `usp_rATINGForOverallrpt_V1`,
  `usp_getFeedbackdetQuesansEmployeeid_V1`, `GetSkillportAssessmentReport`.
  The script for `usp_InsertSupFeedbacks` in `Production/Database` is also
  older than the version the old code called (it has no `@TechSkills` /
  `@SoftSkills`), so the database is the reference, not the script.
- Walk through each role once against the real database: sign in, record
  feedback, submit and review supervisor ratings, send a nomination mail, run
  each report and export, and load one Excel file.
- The old source has database passwords in it (a commented-out connection
  string in `Web.config` and one in `HomeMenu.aspx.cs`). Change those
  passwords if they are still in use anywhere.

## Tests

Back end (a stand-in replaces the database):

```powershell
cd backend
.venv\Scripts\python -m pytest
```

Browser tests (Playwright, in the installed Chrome). They build the front end,
start the back end in demo mode on port 8001 and go through each role: sign
in, every admin page, recording feedback, sorting, the theme switch and the
idle-session warning. They need the back end's `.venv` from the step above.

```powershell
cd frontend
npm run test:e2e
```

Both run on every push through `.github/workflows/ci.yml` at the repository
root. Neither touches the real database, StationH or mail, so they do not
replace the walk-through against the real environment below.

## Health checks

| URL | Answers |
|---|---|
| `/api/health` | 200 while the application is running (for the proxy) |
| `/api/health/ready` | 200 when the database also answers `SELECT 1`, otherwise 503. The reason is written to the log, not returned |

## Idle sessions

A session ends after `FEEDBACK_SESSION_MINUTES` (default 30) without a
request. Two minutes before that the page warns and offers to stay signed in;
when the time runs out it signs out, says why on the sign-in page, and after
signing in again returns to the page the user was on. Feedback being written
on the Record My Feedback form is kept in that browser tab (session storage,
per employee and session) and restored, so a timeout or a reload loses nothing;
it is removed once submitted or closed.

## Old page to new page

| Old page | New route |
|---|---|
| Account/Login.aspx | `/login` |
| Home.aspx, AdminHome.aspx | `/` |
| TrainingList.aspx | `/feedback` |
| Feedback.aspx | `/feedback/record` |
| FeedbackSup.aspx | `/feedback/supervisor` |
| FeedbackReview.aspx | `/feedback/review` |
| MasterMail.aspx | `/masters/mail` |
| MasterVendor.aspx | `/masters/vendor` |
| MasterTrainer.aspx | `/masters/trainer` |
| Course.aspx | `/masters/course` |
| Session.aspx | `/sessions/new` |
| NominationList.aspx | `/sessions/participants` |
| BalFeedback.aspx, DispComments.aspx | `/reports/comments` |
| FeedbackDef.aspx | `/reports/defaulters` |
| IdividualRep.aspx | `/reports/overall` |
| IndividAnalysis.aspx, ExcelExport.aspx | `/reports/individual` |
| TrainingEffectivenessReport.aspx | `/reports/effectiveness` |
| SkillPort/SkillPortUpload.aspx | `/upload` |
| SkillPort/LearningProgramReport.aspx | `/upload/learning-report` |
| CreateAdmin.aspx | `/admin/employees` |

Not migrated, because they were unused or non-functional in the old site:
`test.aspx`, `test2.aspx`, `Test3.aspx`, `WebForm4.aspx`, `Copy of Default.aspx`,
`HomeMenu.aspx`, `UserHome.aspx`, `Thanks.aspx`, `ViewReports.aspx`,
`ImageHandler.ashx` and `Account/ChangePassword.aspx` (it relied on an ASP.NET
membership database that was never configured).

## Differences from the old application

- **Admin pages are enforced on the server.** The old site only hid the menu
  from non-admins; the pages themselves opened for any signed-in user.
- **Excel exports are real `.xlsx` files** instead of HTML tables saved with an
  `.xls` extension.
- **The feedback analysis PDF is drawn directly** (ReportLab) rather than
  converted from HTML, so its layout differs slightly; the content is the same.
- **Per-user state is no longer shared.** The old Feedback, View Comments and
  Participant List pages kept values in static fields, so two people using
  them at once could overwrite each other.
- **Uploaded workbooks get a unique file name** in the upload folder, so two
  admins uploading a file with the same name no longer collide.
- **A mail failure no longer loses the page.** Ratings and reviews are saved
  first; if the notification mail fails the user is told, and the error is
  logged.
- **A course is refused when its ID or its name is already taken.** The stored
  procedure never inserted such a course, but the old page said "Course
  successfully created" unless both were taken.
- **The Overall report shows the Rating Table only.** The old page also built
  pattern and areas-of-concern tables but kept them hidden.
- **Tables can be filtered and paged**, messages and confirmations appear in
  the page instead of browser pop-ups, and the menu is a side bar.
- **Responses carry security headers** (content security policy, no framing,
  no caching of API data).
- **A user id is locked for 15 minutes after 5 failed sign-ins.**
- **Create/Remove Admin was never implemented** in the old site (the Add button
  did nothing); only the employee search is carried over. Admins are still
  added in the database.

One calculation was kept exactly as it was even though it looks like a bug: on
the Overall report with trainer "OverAll", the trainer-delivery score is the
average of the overall result and every trainer except the last. See
`overall_trainer_average` in `backend/app/routers/reports.py`.
