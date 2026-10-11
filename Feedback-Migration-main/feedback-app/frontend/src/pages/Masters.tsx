import { useEffect, useState, type FormEvent } from "react";
import { useNavigate } from "react-router-dom";
import { api, type Grid } from "../api";
import { useDialogs } from "../components/Dialog";
import { useCourses } from "../components/SessionPicker";
import { Card, DataGrid, fail, Field, Notice, ok, Page, Select, useLoad, type Message } from "../components/ui";

const TRAINER_TYPES = [
  { value: "I", label: "Internal" },
  { value: "E", label: "External" },
];

export function CourseMaster() {
  const navigate = useNavigate();
  const dialogs = useDialogs();
  const types = useLoad(() => api.get<string[]>("/api/lookups/course-types"), []).data ?? [];
  const blank = { courseId: "", courseName: "", description: "", internalExternal: "", courseType: "" };
  const [form, setForm] = useState(blank);
  const [busy, setBusy] = useState(false);
  const [message, setMessage] = useState<Message | null>(null);
  const set = (key: keyof typeof blank) => (value: string) => setForm((f) => ({ ...f, [key]: value }));

  async function submit(event: FormEvent) {
    event.preventDefault();
    setBusy(true);
    setMessage(null);
    try {
      const result = await api.post<{ message: string }>("/api/courses", form);
      await dialogs.notify(result.message);
      // The old page went straight on to session creation.
      navigate("/sessions/new");
    } catch (error) {
      setMessage(fail(error));
    } finally {
      setBusy(false);
    }
  }

  return (
    <Page title="Course Master">
      <Card title="Add New Course">
        <form className="form" onSubmit={submit}>
          <Field label="Course ID">
            <input value={form.courseId} onChange={(e) => set("courseId")(e.target.value)} maxLength={50} required />
          </Field>
          <Field label="Course Name">
            <input value={form.courseName} onChange={(e) => set("courseName")(e.target.value)} maxLength={500} required />
          </Field>
          <Field label="Description">
            <input value={form.description} onChange={(e) => set("description")(e.target.value)} maxLength={100} required />
          </Field>
          <Field label="Internal / External">
            <Select value={form.internalExternal} onChange={set("internalExternal")} options={TRAINER_TYPES} placeholder="Select" />
          </Field>
          <Field label="Course Type">
            <Select value={form.courseType} onChange={set("courseType")} options={types} placeholder="Select a Type" />
          </Field>
          <Notice message={message} />
          <div className="actions">
            <button className="button" type="submit" disabled={busy}>
              Submit
            </button>
            <button className="button button-quiet" type="button" onClick={() => setForm(blank)}>
              Reset
            </button>
          </div>
        </form>
      </Card>
    </Page>
  );
}

export function VendorMaster() {
  const [vendorName, setVendorName] = useState("");
  const [prefixId, setPrefixId] = useState("");
  const [busy, setBusy] = useState(false);
  const [message, setMessage] = useState<Message | null>(null);

  async function submit(event: FormEvent) {
    event.preventDefault();
    setBusy(true);
    setMessage(null);
    try {
      const result = await api.post<{ message: string }>("/api/vendors", { vendorName, prefixId });
      setMessage(ok(result.message));
      setVendorName("");
      setPrefixId("");
    } catch (error) {
      setMessage(fail(error));
    } finally {
      setBusy(false);
    }
  }

  return (
    <Page title="Vendor Master">
      <Card title="Add New Vendor">
        <form className="form" onSubmit={submit}>
          <Field label="Vendor Name">
            <input value={vendorName} onChange={(e) => setVendorName(e.target.value)} maxLength={50} required />
          </Field>
          <Field label="Prefix ID" hint="Numbers only">
            <input value={prefixId} onChange={(e) => setPrefixId(e.target.value)} inputMode="numeric" pattern="[0-9]+" required />
          </Field>
          <Notice message={message} />
          <div className="actions">
            <button className="button" type="submit" disabled={busy}>
              Submit
            </button>
          </div>
        </form>
      </Card>
    </Page>
  );
}

export function TrainerMaster() {
  const courses = useCourses();
  const vendors = useLoad(() => api.get<string[]>("/api/lookups/vendors"), []).data ?? [];
  const [courseId, setCourseId] = useState("");
  const [trainerType, setTrainerType] = useState("");
  const [vendorName, setVendorName] = useState("");
  const [trainerId, setTrainerId] = useState("");
  const [trainerName, setTrainerName] = useState("");
  const [lookupError, setLookupError] = useState("");
  const [busy, setBusy] = useState(false);
  const [message, setMessage] = useState<Message | null>(null);
  const external = trainerType === "E";

  function reset() {
    setCourseId("");
    setTrainerType("");
    setVendorName("");
    setTrainerId("");
    setTrainerName("");
    setLookupError("");
  }

  function changeType(value: string) {
    setTrainerType(value);
    setVendorName("");
    setTrainerId("");
    setTrainerName("");
    setLookupError("");
  }

  // Internal trainers are employees: the name comes from the employee id.
  async function findEmployee() {
    if (external || !trainerId.trim()) return;
    try {
      const found = await api.get<{ name: string }>("/api/lookups/employee", { emp_id: trainerId.trim() });
      setTrainerName(found.name);
      setLookupError(found.name ? "" : "Please Enter Valid Employee ID.");
    } catch (error) {
      setLookupError(fail(error).text);
    }
  }

  async function submit(event: FormEvent) {
    event.preventDefault();
    setBusy(true);
    setMessage(null);
    try {
      const result = await api.post<{ message: string }>("/api/trainers", {
        courseId,
        trainerType,
        trainerId: external ? null : trainerId,
        trainerName,
        vendorName: external ? vendorName : null,
      });
      setMessage(ok(result.message));
      reset();
    } catch (error) {
      setMessage(fail(error));
    } finally {
      setBusy(false);
    }
  }

  return (
    <Page title="Trainer Master">
      <Card title="Add New Trainer">
        <form className="form" onSubmit={submit}>
          <Field label="Course Name">
            <Select
              value={courseId}
              onChange={setCourseId}
              options={courses.map((c) => ({ value: c.id, label: c.name }))}
              placeholder="Select the Course Name"
            />
          </Field>
          <Field label="Trainer Type">
            <Select value={trainerType} onChange={changeType} options={TRAINER_TYPES} placeholder="Select Type of Trainer" />
          </Field>
          {external && (
            <Field label="Vendor">
              <Select value={vendorName} onChange={setVendorName} options={vendors} placeholder="Select the Vendor" />
            </Field>
          )}
          <Field label="Trainer ID" hint={external ? "Generated automatically for external trainers" : lookupError || undefined}>
            <input
              value={external ? "Auto generate" : trainerId}
              onChange={(e) => setTrainerId(e.target.value)}
              onBlur={findEmployee}
              disabled={external || !trainerType}
            />
          </Field>
          <Field label="Trainer Name">
            <input
              value={trainerName}
              onChange={(e) => setTrainerName(e.target.value)}
              maxLength={50}
              readOnly={!external}
              disabled={!trainerType}
            />
          </Field>
          <Notice message={message} />
          <div className="actions">
            <button className="button" type="submit" disabled={busy}>
              Submit
            </button>
            <button className="button button-quiet" type="button" onClick={reset}>
              Reset
            </button>
          </div>
        </form>
      </Card>
    </Page>
  );
}

interface MailTemplate {
  from: string;
  cc: string;
  subject: string;
  body: string;
}

export function MailMaster() {
  const [type, setType] = useState(0);
  const template = useLoad(() => api.get<MailTemplate>("/api/mail-master", { type }), [type]);
  const [form, setForm] = useState<MailTemplate>({ from: "", cc: "", subject: "", body: "" });
  const [editingBody, setEditingBody] = useState(false);
  const [busy, setBusy] = useState(false);
  const [message, setMessage] = useState<Message | null>(null);

  useEffect(() => {
    if (template.data) {
      setForm(template.data);
      setEditingBody(false);
    }
  }, [template.data]);

  async function save(event: FormEvent) {
    event.preventDefault();
    setBusy(true);
    setMessage(null);
    try {
      const result = await api.put<{ message: string }>("/api/mail-master", {
        type,
        cc: form.cc,
        subject: form.subject,
        body: form.body,
      });
      setMessage(ok(result.message));
      setEditingBody(false);
      template.reload();
    } catch (error) {
      setMessage(fail(error));
    } finally {
      setBusy(false);
    }
  }

  return (
    <Page title="Mail Master">
      <Card title="View and Update Mail Master">
        <form className="form" onSubmit={save}>
          <div className="choices">
            {["Nomination List", "Supervisor's List"].map((label, value) => (
              <label key={label}>
                <input type="radio" name="mail-type" checked={type === value} onChange={() => setType(value)} />
                {label}
              </label>
            ))}
          </div>
          {template.error && <Notice message={{ kind: "error", text: template.error }} />}
          <Field label="From">
            <input value={form.from} disabled />
          </Field>
          <Field label="Cc" hint="Separate addresses with a semicolon">
            <input value={form.cc} onChange={(e) => setForm({ ...form, cc: e.target.value })} />
          </Field>
          <Field label="Subject">
            <input value={form.subject} onChange={(e) => setForm({ ...form, subject: e.target.value })} maxLength={300} />
          </Field>
          <Field label="Body" hint="HTML with placeholders such as <traineename>; keep the placeholders intact.">
            <textarea
              rows={12}
              value={form.body}
              disabled={!editingBody}
              onChange={(e) => setForm({ ...form, body: e.target.value })}
            />
          </Field>
          <Notice message={message} />
          <div className="actions">
            <button className="button" type="submit" disabled={busy}>
              Submit
            </button>
            {!editingBody && (
              <button className="button button-quiet" type="button" onClick={() => setEditingBody(true)}>
                Edit body
              </button>
            )}
          </div>
        </form>
      </Card>
    </Page>
  );
}

export function EmployeeSearch() {
  const [empId, setEmpId] = useState("");
  const [name, setName] = useState("");
  const [grid, setGrid] = useState<Grid | null>(null);
  const [message, setMessage] = useState<Message | null>(null);

  async function search(event: FormEvent) {
    event.preventDefault();
    setMessage(null);
    try {
      setGrid(await api.get<Grid>("/api/employees/search", { emp_id: empId, name }));
    } catch (error) {
      setMessage(fail(error));
    }
  }

  return (
    <Page title="Employee Search">
      <Card>
        <form className="form form-inline" onSubmit={search}>
          <Field label="Employee ID">
            <input value={empId} onChange={(e) => setEmpId(e.target.value)} maxLength={7} />
          </Field>
          <Field label="Employee Name">
            <input value={name} onChange={(e) => setName(e.target.value)} />
          </Field>
          <button className="button" type="submit">
            Search
          </button>
        </form>
        <Notice message={message} />
        <DataGrid grid={grid} empty="No employees found." />
      </Card>
    </Page>
  );
}
