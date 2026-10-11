import { useEffect, useState } from "react";
import { Link, useNavigate, useSearchParams } from "react-router-dom";
import { api, field } from "../api";
import { useDialogs } from "../components/Dialog";
import { Card, fail, Field, Loading, Notice, ok, Page, Select, useLoad, type Message } from "../components/ui";

type Row = Record<string, unknown>;

const PENDING_NOTE =
  "Note:Only those trainings which are not yet closed and feedback pending by you will be shown below.";

function link(path: string, values: Record<string, string>): string {
  return `${path}?${new URLSearchParams(values)}`;
}

function TrainingTable({ rows, action, to }: { rows: Row[]; action: string; to: string }) {
  if (rows.length === 0) return <p className="muted">{PENDING_NOTE}</p>;
  return (
    <div className="table-wrap">
      <table>
        <thead>
          <tr>
            <th>Trainings</th>
            <th>Start Date</th>
            <th>End Date</th>
            <th>Total Feedbacks</th>
            <th>Pending Feedbacks</th>
            <th />
          </tr>
        </thead>
        <tbody>
          {rows.map((row, i) => (
            <tr key={i}>
              <td>{field(row, "Course")}</td>
              <td>{field(row, "startdate")}</td>
              <td>{field(row, "enddate")}</td>
              <td>{field(row, "Tot_Feedbacks")}</td>
              <td>{field(row, "Bal_Feedbacks")}</td>
              <td>
                <Link
                  className="button button-small"
                  to={link(to, {
                    sessionId: field(row, "sessionid"),
                    courseId: field(row, "courseid"),
                    courseName: field(row, "Course"),
                  })}
                >
                  {action}
                </Link>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}

export function MyFeedback() {
  const { data, error, loading } = useLoad(
    () => api.get<{ trainings: Row[]; supervisor: Row[]; reviews: Row[] }>("/api/feedback/my"),
    [],
  );
  return (
    <Page title="Record My Feedback">
      {error && <Notice message={{ kind: "error", text: error }} />}
      {loading && !data && <Loading />}
      {data && (
        <>
          <Card title="Record My Feedback">
            <TrainingTable rows={data.trainings} action="Record My Feedback" to="/feedback/record" />
          </Card>
          <Card title="Training Effectiveness - Supervisor Feedback">
            <TrainingTable rows={data.supervisor} action="Record Rating" to="/feedback/supervisor" />
          </Card>
          <Card title="Training Effectiveness - Review Feedback">
            {data.reviews.length === 0 ? (
              <p className="muted">
                Note:Only those Reviews which are pending for your approval will be shown below.
              </p>
            ) : (
              <div className="table-wrap">
                <table>
                  <thead>
                    <tr>
                      <th>Supervisors</th>
                      <th>Trainings</th>
                      <th>Start Date</th>
                      <th>End Date</th>
                      <th />
                    </tr>
                  </thead>
                  <tbody>
                    {data.reviews.map((row, i) => (
                      <tr key={i}>
                        <td>{field(row, "Supervisors")}</td>
                        <td>{field(row, "Course")}</td>
                        <td>{field(row, "startdate")}</td>
                        <td>{field(row, "enddate")}</td>
                        <td>
                          <Link
                            className="button button-small"
                            to={link("/feedback/review", {
                              sessionId: field(row, "Session_id"),
                              courseId: field(row, "Course_id"),
                              supervisor: field(row, "Supervisors"),
                            })}
                          >
                            Review Ratings
                          </Link>
                        </td>
                      </tr>
                    ))}
                  </tbody>
                </table>
              </div>
            )}
          </Card>
        </>
      )}
    </Page>
  );
}

interface Header {
  employeeId: string;
  name: string;
  dates: string;
  location: string;
  course?: string;
}

function TrainingHeader({ header, course, who }: { header: Header; course: string; who: string }) {
  return (
    <dl className="summary">
      <div>
        <dt>{who} Name</dt>
        <dd>{header.name}</dd>
      </div>
      <div>
        <dt>{who} ID</dt>
        <dd>{header.employeeId}</dd>
      </div>
      <div>
        <dt>Course Name</dt>
        <dd>{course}</dd>
      </div>
      <div>
        <dt>Training Date(s)</dt>
        <dd>{header.dates}</dd>
      </div>
      <div>
        <dt>Training Location</dt>
        <dd>{header.location}</dd>
      </div>
    </dl>
  );
}

// --- trainee feedback -------------------------------------------------------

const SCALE = [
  { value: 5, label: "Excellent" },
  { value: 4, label: "Very Good" },
  { value: 3, label: "Good" },
  { value: 2, label: "Fair" },
  { value: 1, label: "Poor" },
];
const MAX_COMMENT = 1000;

interface QuestionProps {
  title: string;
  help: string;
  value: number | null;
  onChange: (value: number) => void;
  disabled: boolean;
  missing: boolean;
}

function Question({ title, help, value, onChange, disabled, missing }: QuestionProps) {
  return (
    <fieldset className={`question${missing ? " question-missing" : ""}`} disabled={disabled}>
      <legend>{title}</legend>
      <p className="muted">{help}</p>
      <div className="scale">
        {SCALE.map((option) => (
          <label key={option.value}>
            <input
              type="radio"
              name={title}
              checked={value === option.value}
              onChange={() => onChange(option.value)}
            />
            <span className="scale-value">{option.value}</span>
            {option.label}
          </label>
        ))}
      </div>
    </fieldset>
  );
}

interface FeedbackContext extends Header {
  trainers: string[];
  firstFeedback: boolean;
}

export function FeedbackForm() {
  const [query] = useSearchParams();
  const navigate = useNavigate();
  const sessionId = query.get("sessionId") ?? "";
  const courseId = query.get("courseId") ?? "";
  const courseName = query.get("courseName") ?? "";
  const dialogs = useDialogs();

  const context = useLoad(
    () =>
      api.get<FeedbackContext>("/api/feedback/context", { session_id: sessionId, course_id: courseId }),
    [sessionId, courseId],
  );
  const [trainers, setTrainers] = useState<string[]>([]);
  const [first, setFirst] = useState(true);
  const [trainer, setTrainer] = useState("");
  const [administration, setAdministration] = useState<number | null>(null);
  const [design, setDesign] = useState<number | null>(null);
  const [delivery, setDelivery] = useState<number | null>(null);
  const [comments, setComments] = useState("");
  const [checked, setChecked] = useState(false);
  const [busy, setBusy] = useState(false);
  const [message, setMessage] = useState<Message | null>(null);

  useEffect(() => {
    if (context.data) {
      setTrainers(context.data.trainers);
      setFirst(context.data.firstFeedback);
    }
  }, [context.data]);

  const last = trainers.length <= 1;

  async function submit() {
    setChecked(true);
    setMessage(null);
    if (!trainer) return setMessage({ kind: "error", text: "Please Select Trainers" });
    if (!comments.trim()) return setMessage({ kind: "error", text: "Please enter your comments" });
    if (delivery === null || (first && (administration === null || design === null))) {
      return setMessage({ kind: "error", text: "Please answer every question." });
    }
    if (!(await dialogs.confirm("Are you sure to submit?", { confirmLabel: "Submit" }))) return;

    setBusy(true);
    try {
      const result = await api.post<{ remainingTrainers: string[] }>("/api/feedback", {
        sessionId,
        courseId,
        trainerId: trainer,
        administration,
        design,
        delivery,
        comments,
      });
      if (result.remainingTrainers.length === 0) {
        navigate("/feedback");
        return;
      }
      setMessage(ok(`Feedback recorded for ${trainer}. Please rate the next trainer.`));
      setTrainers(result.remainingTrainers);
      setFirst(false);
      setTrainer("");
      setDelivery(null);
      setComments("");
      setChecked(false);
    } catch (error) {
      setMessage(fail(error));
    } finally {
      setBusy(false);
    }
  }

  return (
    <Page title="Record My Feedback">
      {context.error && <Notice message={{ kind: "error", text: context.error }} />}
      {context.data && (
        <>
          <Card>
            <TrainingHeader header={context.data} course={courseName} who="Trainee" />
            <Field label="Trainer Name" hint={trainer ? undefined : "Please select trainer to enable the options."}>
              <Select value={trainer} onChange={setTrainer} options={trainers} placeholder="Select the Trainer" />
            </Field>
          </Card>
          <Card>
            {first && (
              <>
                <Question
                  title="Training Administration"
                  help="Please rate the training execution on the aspects of schedule intimation, course plan, timings and the training venue."
                  value={administration}
                  onChange={setAdministration}
                  disabled={!trainer}
                  missing={checked && administration === null}
                />
                <Question
                  title="Training Design And Methodology"
                  help="Please rate the training program on the aspects of course content, learning objectives, course duration, quality of the course materials and effectiveness of activities/exercises."
                  value={design}
                  onChange={setDesign}
                  disabled={!trainer}
                  missing={checked && design === null}
                />
              </>
            )}
            <Question
              title="Training Delivery"
              help="Please rate the trainer on the aspects of subject knowledge, training handling, interaction with participants and expertise in hands-on"
              value={delivery}
              onChange={setDelivery}
              disabled={!trainer}
              missing={checked && delivery === null}
            />
            <Field label="Overall Comments" hint={`${MAX_COMMENT - comments.length} Character remaining.`}>
              <textarea
                rows={4}
                maxLength={MAX_COMMENT}
                value={comments}
                onChange={(e) => setComments(e.target.value)}
              />
            </Field>
            <Notice message={message} />
            <div className="actions">
              <button className="button" type="button" disabled={busy || !trainer} onClick={submit}>
                {last ? "Submit" : "Submit & Next Trainer"}
              </button>
              <button
                className="button button-quiet"
                type="button"
                onClick={async () =>
                  (await dialogs.confirm("Are you sure to close?", { confirmLabel: "Close" })) && navigate("/feedback")
                }
              >
                Close
              </button>
            </div>
          </Card>
        </>
      )}
    </Page>
  );
}

// --- supervisor ratings -----------------------------------------------------

const RATINGS = ["5", "4", "3", "2", "1", "NA"];

function RatingGuide() {
  return (
    <Card title="Rating guide">
      <p>
        Please enter the rating on a scale of 1 (Lowest) to 5 (Highest). Please write "NA" if items are not
        applicable.
        <br />
        <strong>(1- Poor 2- Fair 3- Good 4- Very good 5- Excellent NA- Not applicable)</strong>
      </p>
      <div className="guide">
        <div>
          <h3>Technical Skills</h3>
          <ul>
            <li>Able to understand requirements clearly and seek clarifications whenever it is required</li>
            <li>Able to independently develop, build and test code written by him</li>
            <li>Able to work with agile team and deliver project deliverable on time</li>
            <li>Able to learn and implement new technology skills</li>
          </ul>
        </div>
        <div>
          <h3>Soft Skills</h3>
          <ul>
            <li>Able to speak up and express oneself with confidence</li>
            <li>Able to write emails using appropriate business etiquette</li>
            <li>Able to demonstrate effective active listening skills</li>
            <li>Demonstrate appropriate workplace etiquette</li>
            <li>Able to ask appropriate/relevant questions to check understanding and clarify information</li>
            <li>Able to plan and prioritize tasks effectively</li>
            <li>Able to work in a team effectively</li>
          </ul>
        </div>
        <div>
          <h3>Performance</h3>
          <ul>
            <li>How productive the trainee is? Rate the quality of output</li>
          </ul>
          <h3>Parameters for Overall Comments</h3>
          <ul>
            <li>How fast the trainee is productive?</li>
            <li>Any improvement areas for the trainee?</li>
            <li>If retraining is required, please specify on what skills</li>
          </ul>
        </div>
      </div>
    </Card>
  );
}

interface SupervisorRow {
  subordinateId: string;
  name: string;
  workArea: string;
  techSkills: string;
  softSkills: string;
  performance: string;
  overallRating: string;
  overallFeedback: string;
}

const RATING_FIELDS = ["techSkills", "softSkills", "performance", "overallRating"] as const;

export function SupervisorFeedback() {
  const [query] = useSearchParams();
  const navigate = useNavigate();
  const sessionId = query.get("sessionId") ?? "";
  const courseId = query.get("courseId") ?? "";
  const courseName = query.get("courseName") ?? "";
  const dialogs = useDialogs();

  const form = useLoad(
    () =>
      api.get<{ header: Header | null; rows: SupervisorRow[] }>("/api/feedback/supervisor", {
        session_id: sessionId,
        course_id: courseId,
      }),
    [sessionId, courseId],
  );
  const [rows, setRows] = useState<SupervisorRow[]>([]);
  const [busy, setBusy] = useState(false);
  const [message, setMessage] = useState<Message | null>(null);

  useEffect(() => {
    if (form.data) setRows(form.data.rows);
  }, [form.data]);

  function change(index: number, key: keyof SupervisorRow, value: string) {
    setRows((current) => current.map((row, i) => (i === index ? { ...row, [key]: value } : row)));
  }

  const complete =
    rows.length > 0 &&
    rows.every(
      (row) => row.workArea.trim() && row.overallFeedback.trim() && RATING_FIELDS.every((key) => row[key]),
    );

  async function save(submit: boolean) {
    setBusy(true);
    setMessage(null);
    try {
      const result = await api.post<{ message: string; warning?: string | null }>("/api/feedback/supervisor", {
        sessionId,
        courseId,
        submit,
        rows,
      });
      if (submit) {
        await dialogs.notify(result.warning ? `${result.message}\n${result.warning}` : result.message);
        navigate("/feedback");
      } else {
        setMessage(ok(result.message));
      }
    } catch (error) {
      setMessage(fail(error));
    } finally {
      setBusy(false);
    }
  }

  return (
    <Page title="Supervisor Feedback">
      {form.error && <Notice message={{ kind: "error", text: form.error }} />}
      {form.data && (
        <>
          {form.data.header && (
            <Card>
              <TrainingHeader header={form.data.header} course={courseName} who="Supervisor" />
            </Card>
          )}
          <RatingGuide />
          <Card>
            <div className="table-wrap">
              <table>
                <thead>
                  <tr>
                    <th>S. No.</th>
                    <th>Trainee's Employee Id</th>
                    <th>Trainee's Name</th>
                    <th>Mention Work Area (1st 90 days). Specify the area covered by the Trainee.</th>
                    <th>Technical Skills</th>
                    <th>Soft Skills</th>
                    <th>Performance</th>
                    <th>Overall Feedback Rating</th>
                    <th>Overall Comments for the trainee based on the specified parameters</th>
                  </tr>
                </thead>
                <tbody>
                  {rows.map((row, i) => (
                    <tr key={row.subordinateId}>
                      <td>{i + 1}</td>
                      <td>{row.subordinateId}</td>
                      <td>{row.name}</td>
                      <td>
                        <input
                          aria-label={`Work area for ${row.name}`}
                          value={row.workArea}
                          onChange={(e) => change(i, "workArea", e.target.value)}
                        />
                      </td>
                      {RATING_FIELDS.map((key) => (
                        <td key={key}>
                          <Select
                            value={row[key]}
                            onChange={(value) => change(i, key, value)}
                            options={RATINGS}
                            placeholder="Select"
                          />
                        </td>
                      ))}
                      <td>
                        <textarea
                          aria-label={`Overall comments for ${row.name}`}
                          rows={3}
                          value={row.overallFeedback}
                          onChange={(e) => change(i, "overallFeedback", e.target.value)}
                        />
                      </td>
                    </tr>
                  ))}
                </tbody>
              </table>
            </div>
            <Notice message={message} />
            <div className="actions">
              <button className="button" type="button" disabled={busy || !complete} onClick={() => save(true)}>
                Submit
              </button>
              <button className="button button-quiet" type="button" disabled={busy} onClick={() => save(false)}>
                Save
              </button>
            </div>
            {!complete && <p className="muted">Submit becomes available once every trainee is fully rated.</p>}
          </Card>
        </>
      )}
    </Page>
  );
}

// --- PM review --------------------------------------------------------------

export function ReviewFeedback() {
  const [query] = useSearchParams();
  const navigate = useNavigate();
  const sessionId = query.get("sessionId") ?? "";
  const courseId = query.get("courseId") ?? "";
  const supervisor = query.get("supervisor") ?? "";
  const dialogs = useDialogs();

  const form = useLoad(
    () =>
      api.get<{ header: Header; columns: string[]; rows: string[][] }>("/api/feedback/review", {
        session_id: sessionId,
        supervisor,
      }),
    [sessionId, supervisor],
  );
  const [remarks, setRemarks] = useState("");
  const [busy, setBusy] = useState(false);
  const [message, setMessage] = useState<Message | null>(null);

  async function decide(approve: boolean) {
    if (!remarks.trim()) return setMessage({ kind: "error", text: "Please provide Remarks" });
    setBusy(true);
    setMessage(null);
    try {
      const result = await api.post<{ message: string; warning?: string | null }>("/api/feedback/review", {
        sessionId,
        courseId,
        supervisor,
        remarks,
        approve,
      });
      await dialogs.notify(result.warning ? `${result.message}\n${result.warning}` : result.message);
      navigate("/feedback");
    } catch (error) {
      setMessage(fail(error));
    } finally {
      setBusy(false);
    }
  }

  return (
    <Page title="Review Feedback">
      {form.error && <Notice message={{ kind: "error", text: form.error }} />}
      {form.data && (
        <>
          <Card>
            <TrainingHeader header={form.data.header} course={form.data.header.course ?? ""} who="Supervisor" />
          </Card>
          <RatingGuide />
          <Card>
            <div className="table-wrap">
              <table>
                <thead>
                  <tr>
                    {form.data.columns.map((column) => (
                      <th key={column}>{column}</th>
                    ))}
                  </tr>
                </thead>
                <tbody>
                  {form.data.rows.map((row, r) => (
                    <tr key={r}>
                      {row.map((value, c) => (
                        <td key={c}>{value}</td>
                      ))}
                    </tr>
                  ))}
                </tbody>
              </table>
            </div>
            <Field label="Remarks">
              <textarea rows={3} value={remarks} onChange={(e) => setRemarks(e.target.value)} />
            </Field>
            <Notice message={message} />
            <div className="actions">
              <button className="button" type="button" disabled={busy} onClick={() => decide(true)}>
                Approve
              </button>
              <button className="button button-danger" type="button" disabled={busy} onClick={() => decide(false)}>
                Reject
              </button>
            </div>
          </Card>
        </>
      )}
    </Page>
  );
}
