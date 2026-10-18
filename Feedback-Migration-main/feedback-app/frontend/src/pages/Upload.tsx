import { useState } from "react";
import { api, type Grid } from "../api";
import { useDialogs } from "../components/Dialog";
import { Card, DataGrid, fail, Field, Loading, Notice, ok, Page, Select, useLoad, type Message } from "../components/ui";

const MONTHS = [
  "January", "February", "March", "April", "May", "June",
  "July", "August", "September", "October", "November", "December",
].map((label, i) => ({ value: String(i + 1), label }));

interface UploadResult {
  saved: number;
  message: string;
  existing?: Grid;
}

export function ExcelUpload() {
  const options = useLoad(() => api.get<{ types: string[]; firstYear: number }>("/api/skillport/types"), []).data;
  const dialogs = useDialogs();
  const thisYear = new Date().getFullYear();
  const years = options
    ? Array.from({ length: thisYear - options.firstYear + 1 }, (_, i) => String(options.firstYear + i))
    : [];

  const [month, setMonth] = useState("");
  const [year, setYear] = useState("");
  const [uploadType, setUploadType] = useState("");
  const [file, setFile] = useState<File | null>(null);
  const [fileKey, setFileKey] = useState(0);
  const [records, setRecords] = useState<Grid | null>(null);
  const [busy, setBusy] = useState(false);
  const [message, setMessage] = useState<Message | null>(null);

  function clear() {
    setMonth("");
    setYear("");
    setUploadType("");
    setFile(null);
    setFileKey((k) => k + 1);
  }

  function checkChoices(): void {
    if (!month || !year) throw new Error("Please select the Month & Year");
    if (!uploadType) throw new Error("Please select any one of the options");
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

  const save = () =>
    run(async () => {
      checkChoices();
      if (!file) throw new Error("Please select the file to upload");
      const form = new FormData();
      form.append("file", file);
      form.append("upload_type", uploadType);
      form.append("month", month);
      form.append("year", year);
      const result = await api.post<UploadResult>("/api/skillport/upload", form);
      if (result.existing) {
        setRecords(result.existing);
        setMessage({ kind: "error", text: result.message });
        return;
      }
      setRecords(null);
      setMessage(ok(result.message));
      clear();
    });

  const view = () =>
    run(async () => {
      checkChoices();
      setRecords(null);
      setRecords(await api.get<Grid>("/api/skillport/records", { upload_type: uploadType, month, year }));
    });

  const remove = () =>
    run(async () => {
      const sure = await dialogs.confirm("Do you want to delete these records?", { confirmLabel: "Delete", danger: true });
      if (!sure) return;
      const result = await api.post<{ message: string }>("/api/skillport/delete", {
        uploadType,
        month: Number(month),
        year: Number(year),
      });
      setRecords(null);
      setMessage(ok(result.message));
      clear();
    });

  return (
    <Page title="Excel Data Upload">
      <Card title="Excel Data Upload/View">
        <div className="form">
          <div className="form-inline">
            <Field label="Month">
              <Select value={month} onChange={setMonth} options={MONTHS} />
            </Field>
            <Field label="Year">
              <Select value={year} onChange={setYear} options={years} />
            </Field>
          </div>
          <fieldset className="choices choices-stacked">
            <legend>Select the Option</legend>
            {(options?.types ?? []).map((type) => (
              <label key={type}>
                <input type="radio" name="upload-type" checked={uploadType === type} onChange={() => setUploadType(type)} />
                {type}
              </label>
            ))}
          </fieldset>
          <Field label="Excel File Upload">
            <input
              key={fileKey}
              type="file"
              accept=".xls,.xlsx"
              onChange={(e) => setFile(e.target.files?.[0] ?? null)}
            />
          </Field>
          {busy && <Loading label="Please wait" />}
          <Notice message={message} />
          <div className="actions">
            <button className="button" type="button" disabled={busy} onClick={save}>
              Save
            </button>
            <button className="button button-quiet" type="button" disabled={busy} onClick={view}>
              View
            </button>
          </div>
        </div>
      </Card>
      {records && (
        <Card title="Excel Data View">
          <DataGrid grid={records} />
          <div className="actions">
            <button className="button button-danger" type="button" disabled={busy} onClick={remove}>
              Delete
            </button>
          </div>
        </Card>
      )}
    </Page>
  );
}

export function LearningReport() {
  const report = useLoad(() => api.get<Grid>("/api/skillport/learning-report"), []);
  const [message, setMessage] = useState<Message | null>(null);

  async function exportExcel() {
    setMessage(null);
    try {
      await api.download("/api/skillport/learning-report/excel", {}, "LearningProgramReport.xlsx");
    } catch (error) {
      setMessage(fail(error));
    }
  }

  return (
    <Page title="Learning Program Report">
      <Card>
        {report.error && <Notice message={{ kind: "error", text: report.error }} />}
        {report.loading && !report.data && <Loading label="Please wait" />}
        <DataGrid
          grid={report.data}
          serial
          show={[
            ["EmployeeID", "Employee ID"],
            ["EmployeeName", "Employee Name"],
            ["PmsCompetencyBasedPlan", "PMS Competency Based Plan"],
            ["LearningProgramCompletionStatus", "Learning Program Completion Status"],
            ["LearningProgramcompletionDate", "Learning Program Completion Date"],
            ["AssessmentCompletionStatus", "Assessment Completion Status"],
            ["AssessmentCompletionDate", "Assessment Completion Date"],
            ["Technology", "Technology"],
            ["Role", "Role"],
          ]}
        />
        {report.data && report.data.rows.length > 0 && (
          <div className="actions">
            <button className="button" type="button" onClick={exportExcel}>
              Export to Excel
            </button>
          </div>
        )}
        <Notice message={message} />
      </Card>
    </Page>
  );
}
