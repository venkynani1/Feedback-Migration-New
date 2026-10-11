import { useEffect, useState } from "react";
import { api, type Grid } from "../api";
import { FRESHER_COURSE, NO_SELECTION, SessionPicker, type Selection } from "../components/SessionPicker";
import { Card, DataGrid, fail, Field, Notice, ok, Page, Select, useLoad, type Message } from "../components/ui";

function useDownload() {
  const [message, setMessage] = useState<Message | null>(null);
  const [busy, setBusy] = useState(false);
  async function download(path: string, params: Record<string, string>, name: string) {
    setBusy(true);
    setMessage(null);
    try {
      await api.download(path, params, name);
    } catch (error) {
      setMessage(fail(error));
    } finally {
      setBusy(false);
    }
  }
  return { download, busy, message };
}

// --- View Comments ----------------------------------------------------------

interface Comment {
  employeeId: string;
  comments: string;
  display: boolean;
}

export function CommentsReport() {
  const [selection, setSelection] = useState<Selection>(NO_SELECTION);
  const sessionId = selection.sessionId;
  const balance = useLoad(
    sessionId ? () => api.get<Grid>("/api/reports/feedback-balance", { session_id: sessionId }) : null,
    [sessionId],
  );
  const [comments, setComments] = useState<Comment[] | null>(null);
  const [message, setMessage] = useState<Message | null>(null);

  useEffect(() => {
    setComments(null);
    setMessage(null);
  }, [sessionId]);

  async function open() {
    setMessage(null);
    try {
      const list = await api.get<Comment[]>("/api/reports/comments", { session_id: sessionId });
      if (list.length === 0) setMessage({ kind: "info", text: `No Comments are given for session :${sessionId}` });
      else setComments(list);
    } catch (error) {
      setMessage(fail(error));
    }
  }

  async function approve() {
    try {
      const result = await api.put<{ message: string }>("/api/reports/comments", { sessionId, comments });
      setMessage(ok(result.message));
    } catch (error) {
      setMessage(fail(error));
    }
  }

  return (
    <Page title="View Comments">
      <Card title="View and Validate Comments">
        <div className="form form-inline">
          <SessionPicker value={selection} onChange={setSelection} />
        </div>
        {balance.error && <Notice message={{ kind: "error", text: balance.error }} />}
        {sessionId && (
          <DataGrid
            grid={balance.data}
            show={[
              ["sessionid", "Session"],
              ["EMPLOYEES", "Nominated Employees"],
              ["Trainers", "Session Trainers"],
              ["Tot_Feedbacks", "Total Feedbacks"],
              ["Comp_Feedback", "Completed Feedbacks"],
            ]}
            empty="No feedback has been given for this session yet."
          />
        )}
        {sessionId && balance.data && balance.data.rows.length > 0 && (
          <div className="actions">
            <button className="button" type="button" onClick={open}>
              View comments
            </button>
          </div>
        )}
        <Notice message={message} />
      </Card>
      {comments && (
        <Card title="Approve Comments">
          <p className="muted">Ticked comments are printed on the feedback analysis PDF.</p>
          <div className="table-wrap">
            <table>
              <thead>
                <tr>
                  <th>Emp Id</th>
                  <th>Comments</th>
                  <th>Display</th>
                </tr>
              </thead>
              <tbody>
                {comments.map((comment, i) => (
                  <tr key={i}>
                    <td>{comment.employeeId}</td>
                    <td>{comment.comments}</td>
                    <td>
                      <input
                        type="checkbox"
                        aria-label={`Display comment from ${comment.employeeId}`}
                        checked={comment.display}
                        onChange={(e) =>
                          setComments(comments.map((c, j) => (j === i ? { ...c, display: e.target.checked } : c)))
                        }
                      />
                    </td>
                  </tr>
                ))}
              </tbody>
            </table>
          </div>
          <div className="actions">
            <button className="button" type="button" onClick={approve}>
              Approve
            </button>
          </div>
        </Card>
      )}
    </Page>
  );
}

// --- Feedback Defaulters ----------------------------------------------------

export function DefaultersReport() {
  const [selection, setSelection] = useState<Selection>(NO_SELECTION);
  const sessionId = selection.sessionId;
  const report = useLoad(
    sessionId ? () => api.get<Grid>("/api/reports/defaulters", { session_id: sessionId }) : null,
    [sessionId],
  );
  return (
    <Page title="Feedback Defaulters">
      <Card title="View FeedBack Defaulters">
        <div className="form form-inline">
          <SessionPicker value={selection} onChange={setSelection} />
        </div>
        {report.error && <Notice message={{ kind: "error", text: report.error }} />}
      </Card>
      {sessionId && report.data && (
        <Card title="Employee Details">
          <DataGrid grid={report.data} serial empty="All the feedbacks are completed by employee." />
        </Card>
      )}
    </Page>
  );
}

// --- Overall Reports --------------------------------------------------------

const OVERALL = "OverAll";

interface OverallData {
  rating: Grid | null;
  ratingError: string | null;
}

function useSessionTrainers(sessionId: string) {
  return (
    useLoad(
      sessionId ? () => api.get<string[]>("/api/lookups/session-trainers", { session_id: sessionId }) : null,
      [sessionId],
    ).data ?? []
  );
}

export function OverallReport() {
  const [selection, setSelection] = useState<Selection>(NO_SELECTION);
  const [trainer, setTrainer] = useState("");
  const sessionId = selection.sessionId;
  const trainers = useSessionTrainers(sessionId);
  const pdf = useDownload();

  useEffect(() => setTrainer(""), [sessionId]);

  const report = useLoad(
    sessionId && trainer
      ? () => api.get<OverallData>("/api/reports/overall", { session_id: sessionId, trainer_id: trainer })
      : null,
    [sessionId, trainer],
  );

  return (
    <Page title="Overall Reports">
      <Card title="View Feedback Analysis Report - Overall & Individual">
        <div className="form form-inline">
          <SessionPicker value={selection} onChange={setSelection} />
          <Field label="Trainer">
            <Select value={trainer} onChange={setTrainer} options={[OVERALL, ...trainers]} disabled={!sessionId} />
          </Field>
        </div>
        {report.error && <Notice message={{ kind: "error", text: report.error }} />}
      </Card>
      {report.data && trainer && (
        <>
          <Card title="Rating Table">
            {report.data.ratingError && <Notice message={{ kind: "error", text: report.data.ratingError }} />}
            <DataGrid
              grid={report.data.rating}
              show={[
                ["SNo", "S.No"],
                ["SECTION", "Section"],
                ["SCORE", "Score"],
              ]}
            />
            {report.data.rating && (
              <div className="actions">
                <button
                  className="button"
                  type="button"
                  disabled={pdf.busy}
                  onClick={() =>
                    pdf.download(
                      "/api/reports/overall/pdf",
                      { session_id: sessionId, trainer_id: trainer, course_name: selection.courseName },
                      "TrainingFeedbackAnalysis.pdf",
                    )
                  }
                >
                  Export PDF
                </button>
              </div>
            )}
            <Notice message={pdf.message} />
          </Card>
        </>
      )}
    </Page>
  );
}

// --- Individual Reports -----------------------------------------------------

interface IndividualData extends Grid {
  questions: Array<{ number: string; question: string; description?: string }>;
}

export function IndividualReport() {
  const [selection, setSelection] = useState<Selection>(NO_SELECTION);
  const [trainer, setTrainer] = useState("");
  const [employee, setEmployee] = useState("");
  const sessionId = selection.sessionId;
  const trainers = useSessionTrainers(sessionId);
  const excel = useDownload();

  const employees =
    useLoad(
      sessionId && trainer
        ? () => api.get<string[]>("/api/lookups/session-employees", { session_id: sessionId })
        : null,
      [sessionId, trainer],
    ).data ?? [];

  useEffect(() => setTrainer(""), [sessionId]);
  useEffect(() => setEmployee(""), [sessionId, trainer]);

  const params = { session_id: sessionId, trainer_id: trainer, employee_id: employee };
  const report = useLoad(
    sessionId && trainer && employee ? () => api.get<IndividualData>("/api/reports/individual", params) : null,
    [sessionId, trainer, employee],
  );
  const hasDescriptions = report.data?.questions.some((q) => q.description !== undefined) ?? false;

  return (
    <Page title="Individual Reports">
      <Card title="View Individual Analysis Report">
        <div className="form form-inline">
          <SessionPicker value={selection} onChange={setSelection} />
          <Field label="Trainer">
            <Select
              value={trainer}
              onChange={setTrainer}
              options={trainers}
              placeholder="Select the Trainer"
              disabled={!sessionId}
            />
          </Field>
          <Field label="Employee">
            <Select
              value={employee}
              onChange={setEmployee}
              options={[{ value: "All", label: "All Employees" }, ...[...new Set(employees)]]}
              disabled={!trainer}
            />
          </Field>
        </div>
        {report.error && <Notice message={{ kind: "error", text: report.error }} />}
      </Card>
      {report.data && employee && (
        <>
          <Card title="Feedback Details">
            <DataGrid grid={report.data} />
            <div className="actions">
              <button
                className="button"
                type="button"
                disabled={excel.busy}
                onClick={() =>
                  excel.download(
                    "/api/reports/individual/excel",
                    { ...params, course_name: selection.courseName },
                    "FeedbackDetails.xlsx",
                  )
                }
              >
                Export to Excel
              </button>
            </div>
            <Notice message={excel.message} />
          </Card>
          {report.data.questions.length > 0 && (
            <Card title="Questions">
              <div className="table-wrap">
                <table>
                  <thead>
                    <tr>
                      <th>Question No</th>
                      <th>Question</th>
                      {hasDescriptions && <th>Description</th>}
                    </tr>
                  </thead>
                  <tbody>
                    {report.data.questions.map((q) => (
                      <tr key={q.number}>
                        <td>{q.number}</td>
                        <td>{q.question}</td>
                        {hasDescriptions && <td>{q.description}</td>}
                      </tr>
                    ))}
                  </tbody>
                </table>
              </div>
            </Card>
          )}
        </>
      )}
    </Page>
  );
}

// --- Training Effectiveness Report ------------------------------------------

export function EffectivenessReport() {
  const [selection, setSelection] = useState<Selection>(NO_SELECTION);
  const sessionId = selection.sessionId;
  const report = useLoad(
    sessionId ? () => api.get<Grid>("/api/reports/effectiveness", { session_id: sessionId }) : null,
    [sessionId],
  );
  const excel = useDownload();

  return (
    <Page title="Training Effectiveness Report">
      <Card title="View Training Effectiveness Report">
        <div className="form form-inline">
          <SessionPicker value={selection} onChange={setSelection} fixedCourse={FRESHER_COURSE} />
        </div>
        {report.error && <Notice message={{ kind: "error", text: report.error }} />}
      </Card>
      {sessionId && report.data && (
        <Card title="Feedback Details">
          <DataGrid
            grid={report.data}
            show={[
              ["course", "Course"],
              ["Session_id", "Session ID"],
              ["S_SUPERVISOR_ID", "Supervisor ID"],
              ["Supervisor Name", "Supervisor Name"],
              ["Subordinate_ID", "Subordinate ID"],
              ["Subordinate Name", "Subordinate Name"],
              ["Work Area", "Work Area"],
              ["Technical Skills", "Technical Skills"],
              ["Soft Skills", "Soft Skills"],
              ["Performance", "Performance"],
              ["Overall Feedback Rating", "Overall Feedback Rating"],
              ["Overall Comments", "Overall Comments"],
              ["Status", "Status"],
              ["Feedback Date", "Feedback Date"],
              ["PM_ID", "PM ID"],
              ["PM Name", "PM Name"],
              ["PM Remarks", "PM Remarks"],
            ]}
            empty="Record Not found"
          />
          {report.data.rows.length > 0 && (
            <div className="actions">
              <button
                className="button"
                type="button"
                disabled={excel.busy}
                onClick={() =>
                  excel.download("/api/reports/effectiveness/excel", { session_id: sessionId }, "FeedbackDetails.xlsx")
                }
              >
                Export to Excel
              </button>
            </div>
          )}
          <Notice message={excel.message} />
        </Card>
      )}
    </Page>
  );
}
