import { useEffect } from "react";
import { api } from "../api";
import { useAuth } from "../auth";
import { Field, Select, useLoad } from "./ui";

export interface Course {
  id: string;
  name: string;
}

export const FRESHER_COURSE: Course = { id: "FTP_001", name: "Fresher Training Program" };

export function useCourses() {
  return useLoad(() => api.get<Course[]>("/api/lookups/courses"), []).data ?? [];
}

export interface Selection {
  courseId: string;
  courseName: string;
  sessionId: string;
}

export const NO_SELECTION: Selection = { courseId: "", courseName: "", sessionId: "" };

interface Props {
  value: Selection;
  onChange: (value: Selection) => void;
  /** Lock the course drop-down to a single course. */
  fixedCourse?: Course;
}

/** Course and session drop-downs; sessions follow the year chosen in the header. */
export function SessionPicker({ value, onChange, fixedCourse }: Props) {
  const { year } = useAuth();
  const allCourses = useCourses();
  const courses = fixedCourse ? [fixedCourse] : allCourses;

  useEffect(() => {
    if (fixedCourse && value.courseId !== fixedCourse.id) {
      onChange({ courseId: fixedCourse.id, courseName: fixedCourse.name, sessionId: "" });
    }
  }, [fixedCourse, value.courseId, onChange]);

  const sessions = useLoad(
    value.courseId && year !== null
      ? () => api.get<string[]>("/api/lookups/sessions", { course_id: value.courseId, year })
      : null,
    [value.courseId, year],
  );

  // A session belongs to one year; changing the year invalidates the choice.
  useEffect(() => {
    if (value.sessionId && sessions.data && !sessions.data.includes(value.sessionId)) {
      onChange({ ...value, sessionId: "" });
    }
  }, [sessions.data, value, onChange]);

  return (
    <>
      <Field label="Course">
        <Select
          value={value.courseId}
          disabled={Boolean(fixedCourse)}
          options={courses.map((c) => ({ value: c.id, label: c.name }))}
          onChange={(courseId) =>
            onChange({
              courseId,
              courseName: courses.find((c) => c.id === courseId)?.name ?? "",
              sessionId: "",
            })
          }
        />
      </Field>
      <Field label="Session" hint={year !== null ? `Sessions starting in ${year}` : undefined}>
        <Select
          value={value.sessionId}
          options={sessions.data ?? []}
          disabled={!value.courseId}
          onChange={(sessionId) => onChange({ ...value, sessionId })}
        />
      </Field>
    </>
  );
}
