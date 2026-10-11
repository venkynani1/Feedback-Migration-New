import { useEffect, useState, type ChangeEvent } from "react";
import { api, type Grid } from "../api";
import { useDialogs } from "../components/Dialog";
import { FRESHER_COURSE, NO_SELECTION, SessionPicker, useCourses, type Selection } from "../components/SessionPicker";
import { Card, DataGrid, fail, Field, Notice, ok, Page, Select, useLoad, type Message } from "../components/ui";

const LOCATIONS = ["Bangalore", "Chennai", "Mumbai", "Pune", "Mexico"];
const TRAINER_TYPES = [
  { value: "I", label: "Internal" },
  { value: "E", label: "External" },
];

function today(): string {
  const now = new Date();
  const local = new Date(now.getTime() - now.getTimezoneOffset() * 60000);
  return local.toISOString().slice(0, 10);
}

export function NewSession() {
  const courses = useCourses();
  const dialogs = useDialogs();
  const vendors = useLoad(() => api.get<string[]>("/api/lookups/vendors"), []).data ?? [];
  const [courseId, setCourseId] = useState("");
  const [startDate, setStartDate] = useState("");
  const [endDate, setEndDate] = useState("");
  const [location, setLocation] = useState("");
  const [trainerType, setTrainerType] = useState("");
  const [vendorName, setVendorName] = useState("");
  const [chosen, setChosen] = useState<string[]>([]);
  const [markedAvailable, setMarkedAvailable] = useState<string[]>([]);
  const [markedChosen, setMarkedChosen] = useState<string[]>([]);
  const [busy, setBusy] = useState(false);
  const [message, setMessage] = useState<Message | null>(null);

  const external = trainerType === "E";
  const canListTrainers = Boolean(courseId && trainerType && (!external || vendorName));
  const trainers = useLoad(
    canListTrainers
      ? () =>
          api.get<string[]>("/api/lookups/course-trainers", {
            course_id: courseId,
            trainer_type: trainerType,
            vendor_name: external ? vendorName : undefined,
          })
      : null,
    [courseId, trainerType, vendorName],
  );
  const available = (trainers.data ?? []).filter((t) => !chosen.includes(t));

  // Trainers are registered per course and type, so a change clears the picks.
  useEffect(() => {
    setChosen([]);
    setMarkedAvailable([]);
    setMarkedChosen([]);
  }, [courseId, trainerType, vendorName]);

  function reset() {
    setCourseId("");
    setStartDate("");
    setEndDate("");
    setLocation("");
    setTrainerType("");
    setVendorName("");
  }

  async function create() {
    setMessage(null);
    const error =
      (!courseId && "Please Select Course Name.") ||
      (!location && "Please Select Location Name.") ||
      (!trainerType && "Please Select Trainer Type.") ||
      (!startDate && "Please Enter From Date.") ||
      (!endDate && "Please Enter To Date.") ||
      (startDate > today() && "From Date should be current date or less than current date.") ||
      (startDate > endDate && "From Date should be less than or equal to To date.") ||
      (chosen.length === 0 && "Minimum one trainers should select for session.");
    if (error) return setMessage({ kind: "error", text: error });

    const key = { courseId, location, startDate, endDate };
    setBusy(true);
    try {
      const check = await api.post<{ sessionId: string; existingCount: number }>("/api/sessions/check", key);
      let createAnother = false;
      if (check.existingCount > 0) {
        createAnother = await dialogs.confirm(
          "Session for the provided Course and Time is already available. Would you like to another?",
          { confirmLabel: "Create another" },
        );
        if (!createAnother) return;
      }
      const result = await api.post<{ message: string }>("/api/sessions", {
        ...key,
        courseName: courses.find((c) => c.id === courseId)?.name ?? "",
        trainers: chosen,
        createAnother,
      });
      setMessage(ok(result.message));
      reset();
    } catch (e) {
      setMessage(fail(e));
    } finally {
      setBusy(false);
    }
  }

  const picked = (event: ChangeEvent<HTMLSelectElement>) =>
    Array.from(event.target.selectedOptions, (o) => o.value);

  return (
    <Page title="Create New Session">
      <Card title="Add New Session">
        <div className="form">
          <Field label="Course Name">
            <Select value={courseId} onChange={setCourseId} options={courses.map((c) => ({ value: c.id, label: c.name }))} />
          </Field>
          <Field label="From Date">
            <input type="date" value={startDate} max={today()} onChange={(e) => setStartDate(e.target.value)} />
          </Field>
          <Field label="To Date">
            <input type="date" value={endDate} min={startDate || undefined} onChange={(e) => setEndDate(e.target.value)} />
          </Field>
          <Field label="Location">
            <Select value={location} onChange={setLocation} options={LOCATIONS} />
          </Field>
          <Field label="Trainer Type">
            <Select
              value={trainerType}
              onChange={(value) => {
                setTrainerType(value);
                setVendorName("");
              }}
              options={TRAINER_TYPES}
            />
          </Field>
          {external && (
            <Field label="Vendor">
              <Select value={vendorName} onChange={setVendorName} options={vendors} />
            </Field>
          )}
          <div className="dual-list">
            <Field label="Available trainers">
              <select multiple size={6} value={markedAvailable} onChange={(e) => setMarkedAvailable(picked(e))}>
                {available.map((t) => (
                  <option key={t}>{t}</option>
                ))}
              </select>
            </Field>
            <div className="dual-list-buttons">
              <button
                className="button button-small"
                type="button"
                aria-label="Add selected trainers"
                onClick={() => {
                  setChosen([...chosen, ...markedAvailable]);
                  setMarkedAvailable([]);
                }}
              >
                &gt;&gt;
              </button>
              <button
                className="button button-small"
                type="button"
                aria-label="Remove selected trainers"
                onClick={() => {
                  setChosen(chosen.filter((t) => !markedChosen.includes(t)));
                  setMarkedChosen([]);
                }}
              >
                &lt;&lt;
              </button>
            </div>
            <Field label="Trainers for this session">
              <select multiple size={6} value={markedChosen} onChange={(e) => setMarkedChosen(picked(e))}>
                {chosen.map((t) => (
                  <option key={t}>{t}</option>
                ))}
              </select>
            </Field>
          </div>
          <p className="muted">The session ID is generated automatically from the location, course and dates.</p>
          <Notice message={message} />
          <div className="actions">
            <button className="button" type="button" disabled={busy} onClick={create}>
              Create
            </button>
            <button className="button button-quiet" type="button" onClick={reset}>
              Reset
            </button>
          </div>
        </div>
      </Card>
    </Page>
  );
}

interface SubmitResult {
  message: string;
  alreadyNominated: string[];
  mailErrors: string[];
  mailStatus: Grid;
}

export function ParticipantList() {
  // 0 = participants uploaded from Excel, 1 = supervisors of the nominated freshers
  const [listType, setListType] = useState(0);
  const [selection, setSelection] = useState<Selection>(NO_SELECTION);
  const [file, setFile] = useState<File | null>(null);
  const [list, setList] = useState<Grid | null>(null);
  const [result, setResult] = useState<SubmitResult | null>(null);
  const [busy, setBusy] = useState(false);
  const [message, setMessage] = useState<Message | null>(null);

  const dates = useLoad(
    selection.sessionId
      ? () =>
          api.get<{ startDate: string; endDate: string }>("/api/lookups/session-details", {
            session_id: selection.sessionId,
          })
      : null,
    [selection.sessionId],
  );

  function changeType(value: number) {
    setListType(value);
    setSelection(NO_SELECTION);
    setList(null);
    setResult(null);
    setFile(null);
    setMessage(null);
  }

  async function run(action: () => Promise<void>) {
    setBusy(true);
    setMessage(null);
    try {
      await action();
    } catch (error) {
      setMessage(fail(error));
    } finally {
      setBusy(false);
    }
  }

  const importList = () =>
    run(async () => {
      setResult(null);
      if (listType === 1) {
        if (!selection.sessionId) throw new Error("Please select the session.");
        setList(await api.get<Grid>("/api/nominations/supervisors", { session_id: selection.sessionId }));
        return;
      }
      if (!file) throw new Error("Please select the file to upload");
      const form = new FormData();
      form.append("file", file);
      setList(await api.post<Grid>("/api/nominations/parse", form));
    });

  const send = () =>
    run(async () => {
      if (!selection.courseId || !selection.sessionId) throw new Error("Please select the course and session.");
      const submitted = await api.post<SubmitResult>("/api/nominations", {
        listType,
        courseId: selection.courseId,
        sessionId: selection.sessionId,
        employeeIds: (list?.rows ?? []).map((row) => String(row[0] ?? "")),
      });
      setResult(submitted);
      setList(null);
      setMessage(ok(submitted.message));
    });

  return (
    <Page title="Upload Participant List">
      <Card title="Upload Participant List & Send Mail">
        <div className="form">
          <div className="choices">
            {["Nomination List", "Supervisor's List"].map((label, value) => (
              <label key={label}>
                <input type="radio" name="list-type" checked={listType === value} onChange={() => changeType(value)} />
                {label}
              </label>
            ))}
          </div>
          <SessionPicker
            value={selection}
            onChange={setSelection}
            fixedCourse={listType === 1 ? FRESHER_COURSE : undefined}
          />
          {dates.data && selection.sessionId && (
            <p className="muted">
              Start date: {dates.data.startDate} &nbsp; End date: {dates.data.endDate}
            </p>
          )}
          {listType === 0 && (
            <Field label="Participants list (.xlsx)">
              <input type="file" accept=".xlsx" onChange={(e) => setFile(e.target.files?.[0] ?? null)} />
            </Field>
          )}
          <div className="actions">
            <button className="button" type="button" disabled={busy} onClick={importList}>
              Import
            </button>
            {listType === 0 && (
              <button
                className="button button-quiet"
                type="button"
                onClick={() => run(() => api.download("/api/nominations/sample", {}, "SampleNominationList.xlsx"))}
              >
                Download sample Participants list
              </button>
            )}
          </div>
          <Notice message={message} />
        </div>
      </Card>

      {list && (
        <Card title={listType === 0 ? "Participants list" : "Supervisors list"}>
          <DataGrid grid={list} empty="No one to add for this session." />
          {list.rows.length > 0 && (
            <div className="actions">
              <button className="button" type="button" disabled={busy} onClick={send}>
                Add &amp; Send Mail
              </button>
            </div>
          )}
        </Card>
      )}

      {result && (
        <Card title="Mail Status">
          {result.alreadyNominated.length > 0 && (
            <Notice
              message={{ kind: "info", text: `Employee Already Nominated: ${result.alreadyNominated.join(", ")}` }}
            />
          )}
          {result.mailErrors.length > 0 && (
            <Notice
              message={{ kind: "error", text: `Mail could not be sent to: ${result.mailErrors.join(", ")}` }}
            />
          )}
          <DataGrid grid={result.mailStatus} empty="No mail has been sent for this session." />
        </Card>
      )}
    </Page>
  );
}
