"""Admin reports: comments, defaulters, overall and individual analysis,
training effectiveness."""

from __future__ import annotations

import logging
from decimal import Decimal, InvalidOperation

from fastapi import APIRouter, Depends, Response
from pydantic import BaseModel

from .. import db, exports, mailer
from ..deps import User, UserError, require_admin
from .auth import employee_name
from .lookups import session_details, trainers_for_session

log = logging.getLogger(__name__)
router = APIRouter(prefix="/api/reports", tags=["reports"])

OVERALL = "OverAll"
ALL_EMPLOYEES = "All"
NOT_APPLICABLE_ERROR = (
    "Unable to run report.Participant have selected the option 5 (Not Applicable)."
)
NO_FEEDBACK = "No feedbacks are given by nominated staff"
NO_EMPLOYEE_FEEDBACK = "Selected employee Not provides feedbacks for these trainers"

QUESTION_DESCRIPTIONS = {
    "21": "Please rate the training execution on the aspects of schedule intimation, "
    "course plan, timings and the training venue.",
    "22": "Please rate the training program on the aspects of course content, learning "
    "objectives, course duration, quality of the course materials and effectiveness "
    "of activities/exercises.",
    "23": "Please rate the trainer on the aspects of subject knowledge, training "
    "handling, interaction with participants and expertise in hands-on",
}
PIVOT_QUESTIONS = [str(n) for n in range(1, 19)] + ["21", "22", "23"]


# --- View and Validate Comments ---------------------------------------------


def _comments(session_id: str, trainer_id: str | None = None) -> db.Table:
    return db.call_proc(
        "usp_getFeedbackCommentsForSessionid",
        {"sessionid": session_id, "TrainerId": trainer_id},
    ).first


@router.get("/feedback-balance")
def feedback_balance(session_id: str, _: User = Depends(require_admin)) -> dict:
    return db.call_proc(
        "usp_getFeedbackBalanceForSessionid", {"sessionid": session_id}
    ).first.to_json()


@router.get("/comments")
def comments(session_id: str, _: User = Depends(require_admin)) -> list[dict]:
    table = _comments(session_id)
    return [
        {
            "employeeId": table.text(i, "Employee_id"),
            "comments": table.text(i, "Comments"),
            "display": table.text(i, "CommentsValid") == "1",
        }
        for i in range(len(table))
    ]


class CommentFlag(BaseModel):
    employeeId: str
    display: bool


class CommentsIn(BaseModel):
    sessionId: str
    comments: list[CommentFlag]


@router.put("/comments")
def approve_comments(body: CommentsIn, _: User = Depends(require_admin)) -> dict:
    """Mark which comments may appear on the feedback analysis PDF."""
    for item in body.comments:
        db.call_proc(
            "usp_UpdateCommentsvaildDetails",
            {
                "Employee_id": item.employeeId,
                "session_id": body.sessionId,
                "Commentsvalid": 1 if item.display else 0,
            },
        )
    return {"message": "Valid Comments are updated."}


# --- Feedback Defaulters ----------------------------------------------------


def _defaulters(session_id: str) -> db.Table:
    """Nominated employees who still owe feedback for the session."""
    table = db.call_proc(
        "usp_getFeedbackBalanceEmpIdForSessionid", {"sessionid": session_id}
    ).first
    if len(table) == 0:
        return table
    pending = table.index("Pending FeedBack")
    return db.Table(table.columns, [r for r in table.rows if int(r[pending] or 0) not in (0, -1)])


@router.get("/defaulters")
def defaulters(session_id: str, _: User = Depends(require_admin)) -> dict:
    return _defaulters(session_id).to_json()


# --- Overall Reports --------------------------------------------------------


def _rating(session_id: str, trainer_id: str) -> db.Table | None:
    """Section scores, or None when the procedure fails (no usable answers)."""
    try:
        return db.call_proc(
            "usp_rATINGForOverallrpt_V1", {"sessionid": session_id, "TrainerId": trainer_id}
        ).first
    except Exception:
        log.exception("Rating report failed for %s / %s", session_id, trainer_id)
        return None


def _score(table: db.Table, row: int) -> Decimal:
    try:
        return Decimal(table.text(row, "SCORE").strip())
    except InvalidOperation:
        return Decimal(0)


def overall_trainer_average(session_id: str, trainers: list[str]) -> Decimal | None:
    """Average trainer-delivery score shown on the OverAll rating table.

    Kept exactly as the old page computed it: the 'OverAll' result is averaged
    with every trainer except the last one in the list.
    """
    total = Decimal(0)
    for trainer_id in [OVERALL] + trainers[:-1]:
        table = _rating(session_id, trainer_id)
        if table is None or len(table) < 3:
            return None
        total += _score(table, len(table) - 1 if len(table) == 4 else 2)
    return total / len(trainers) if trainers else None


@router.get("/overall")
def overall(session_id: str, trainer_id: str, _: User = Depends(require_admin)) -> dict:
    pattern = db.call_proc(
        "usp_getFeedbackForOverallrpt_V1", {"sessionid": session_id, "TrainerId": trainer_id}
    ).first
    if len(pattern) == 0:
        raise UserError(NO_FEEDBACK, 404)

    rating = _rating(session_id, trainer_id)
    rating_error = None
    if rating is None or len(rating) < 3:
        rating, rating_error = None, NOT_APPLICABLE_ERROR
    elif trainer_id == OVERALL:
        average = overall_trainer_average(session_id, trainers_for_session(session_id))
        if average is None:
            rating, rating_error = None, NOT_APPLICABLE_ERROR
        else:
            rating.rows[2][rating.index("SCORE")] = str(round(average, 2))

    return {
        "pattern": pattern.to_json(),
        "rating": rating.to_json() if rating else None,
        "ratingError": rating_error,
    }


@router.get("/overall/pdf")
def overall_pdf(
    session_id: str,
    trainer_id: str,
    course_name: str,
    user: User = Depends(require_admin),
) -> Response:
    details = session_details(session_id)
    rating = _rating(session_id, trainer_id)
    if len(details) == 0 or rating is None or len(rating) < 3:
        raise UserError(NOT_APPLICABLE_ERROR)

    start, end = details.text(0, "startdate"), details.text(0, "enddate")
    if trainer_id == OVERALL:
        names = [db.to_text(v) for v in details.column("Trainer Name")]
        trainer_name = mailer.strip_trainer_ids(mailer.join_trainer_names(names))
    else:
        trainer_name = mailer.strip_trainer_ids(trainer_id)

    scores = [_score(rating, i) for i in range(3)]
    approved = _comments(session_id, "Overall")
    comment_list = [
        approved.text(i, "Comments").strip()
        for i in range(len(approved))
        if approved.text(i, "CommentsValid") == "1"
    ]

    def shown(score: Decimal) -> str:
        return str(score)[:3]

    pdf = exports.feedback_analysis_pdf(
        course_title=course_name,
        course_dates=start if start == end else f"{start} To {end}",
        trainer_name=trainer_name,
        participants=details.text(0, "max_participants"),
        administration=shown(scores[0]),
        design=shown(scores[1]),
        delivery=shown(scores[2]),
        overall=shown(sum(scores) / 3),
        comments=comment_list,
        prepared_by=user.name or employee_name(user.emp_id),
        prepared_date=details.text(0, "PreparedDate"),
    )
    return exports.download(pdf, "TrainingFeedbackAnalysis.pdf", "application/pdf")


# --- Individual Reports -----------------------------------------------------


def _drop_empty_columns(columns: list[str], rows: list[list]) -> tuple[list[str], list[list]]:
    keep = [i for i in range(len(columns)) if any(r[i] is not None for r in rows)]
    return [columns[i] for i in keep], [[r[i] for i in keep] for r in rows]


def pivot_all_employees(
    answers: db.Table,
    comments: db.Table,
    pending: db.Table,
    session_id: str,
    trainer_id: str,
) -> tuple[list[str], list[list], list[dict]]:
    """One row per employee with a column per question.

    answers rows are (question no, question, response, session, employee id,
    employee name) ordered by employee. Employees who have not answered are
    appended with zeroes so the sheet lists everyone nominated.
    """
    columns = (
        ["SessionId", "Trainer", "Employee Id", "Employee Name"]
        + [f"Q{q}" for q in PIVOT_QUESTIONS]
        + ["Comments"]
    )
    position = {c: i for i, c in enumerate(columns)}
    comment_for = {
        db.to_text(c[0]): db.to_text(c[1])
        for c in comments.rows
        if len(c) > 3 and db.to_text(c[3]) == trainer_id
    }

    questions: list[dict] = []
    seen_questions: set[str] = set()
    rows: list[list] = []
    by_employee: dict[tuple[str, str], list] = {}
    for answer in answers.rows:
        number, question, response = db.to_text(answer[0]), db.to_text(answer[1]), answer[2]
        session, emp_id, emp_name = (db.to_text(v) for v in answer[3:6])
        if number not in seen_questions:
            seen_questions.add(number)
            questions.append({"number": number, "question": question})

        row = by_employee.get((emp_id, session))
        if row is None:
            row = [None] * len(columns)
            row[position["SessionId"]] = session
            row[position["Trainer"]] = trainer_id
            row[position["Employee Id"]] = emp_id
            row[position["Employee Name"]] = emp_name
            row[position["Comments"]] = comment_for.get(emp_id)
            by_employee[(emp_id, session)] = row
            rows.append(row)
        if f"Q{number}" in position:
            row[position[f"Q{number}"]] = db.to_text(response)

    columns, rows = _drop_empty_columns(columns, rows)

    answered = {r[columns.index("Employee Id")] for r in rows}
    for defaulter in pending.rows:
        emp_id = db.to_text(defaulter[0])
        if emp_id in answered:
            continue
        row = []
        for column in columns:
            if column == "SessionId":
                row.append(session_id)
            elif column == "Trainer":
                row.append(trainer_id)
            elif column == "Employee Id":
                row.append(emp_id)
            elif column == "Employee Name":
                row.append(db.to_text(defaulter[1]))
            elif column == "Comments":
                row.append("")
            else:
                row.append("0")
        rows.append(row)

    if any(q["number"] == "21" for q in questions):
        for q in questions:
            q["description"] = QUESTION_DESCRIPTIONS.get(q["number"], "")
    return columns, rows, questions


def single_employee(
    answers: db.Table, comments: db.Table, emp_id: str, trainer_id: str
) -> tuple[list[str], list[list]]:
    columns = answers.columns + ["Comments"]
    rows = [list(r) + [None] for r in answers.rows]
    for comment in comments.rows:
        if (
            len(comment) > 3
            and db.to_text(comment[0]) == emp_id
            and db.to_text(comment[3]) == trainer_id
        ):
            rows[0][-1] = comment[1]
            break
    response = answers.index("Response")
    rows = [r for r in rows if r[response] is not None]
    return _drop_empty_columns(columns, rows)


def _individual(session_id: str, trainer_id: str, employee_id: str) -> dict:
    answers = db.call_proc(
        "usp_getFeedbackdetQuesansEmployeeid_V1",
        {"sessionid": session_id, "empid": employee_id, "TrainerID": trainer_id},
    ).first
    if len(answers) == 0:
        raise UserError(NO_EMPLOYEE_FEEDBACK, 404)
    all_comments = _comments(session_id)

    if employee_id == ALL_EMPLOYEES:
        columns, rows, questions = pivot_all_employees(
            answers, all_comments, _defaulters(session_id), session_id, trainer_id
        )
    else:
        columns, rows = single_employee(answers, all_comments, employee_id, trainer_id)
        questions = []
    if not rows:
        raise UserError(NO_EMPLOYEE_FEEDBACK, 404)
    table = db.Table(columns, rows)
    return {**table.to_json(), "questions": questions}


@router.get("/individual")
def individual(
    session_id: str, trainer_id: str, employee_id: str, _: User = Depends(require_admin)
) -> dict:
    return _individual(session_id, trainer_id, employee_id)


@router.get("/individual/excel")
def individual_excel(
    session_id: str,
    trainer_id: str,
    employee_id: str,
    course_name: str,
    _: User = Depends(require_admin),
) -> Response:
    report = _individual(session_id, trainer_id, employee_id)
    content = exports.workbook_bytes(
        report["columns"],
        report["rows"],
        heading=[
            ("Session", course_name),
            # Session ids end with the date range, e.g. ..._5/30/2015To6/1/2015
            ("Date", session_id.split("_")[-1]),
            ("Trainer", trainer_id),
        ],
    )
    return exports.download(content, "FeedbackDetails.xlsx", exports.XLSX)


# --- Training Effectiveness Report ------------------------------------------


def _effectiveness(session_id: str) -> db.Table:
    return db.call_proc("usp_getTrnEffectivenessReport", {"sessionid": session_id}).first


@router.get("/effectiveness")
def effectiveness(session_id: str, _: User = Depends(require_admin)) -> dict:
    return _effectiveness(session_id).to_json()


@router.get("/effectiveness/excel")
def effectiveness_excel(session_id: str, _: User = Depends(require_admin)) -> Response:
    table = _effectiveness(session_id)
    if len(table) == 0:
        raise UserError("Record Not found", 404)
    hidden = table.index("serial number") if table.has("serial number") else -1
    columns = [c for i, c in enumerate(table.columns) if i != hidden]
    rows = [[v for i, v in enumerate(r) if i != hidden] for r in table.rows]
    return exports.download(
        exports.workbook_bytes(columns, rows), "FeedbackDetails.xlsx", exports.XLSX
    )
