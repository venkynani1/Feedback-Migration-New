"""Record My Feedback, supervisor ratings and PM review."""

from __future__ import annotations

import logging

from fastapi import APIRouter, Depends
from pydantic import BaseModel

from .. import db, mailer
from ..deps import User, UserError, current_user
from .lookups import trainers_for_session

log = logging.getLogger(__name__)
router = APIRouter(prefix="/api/feedback", tags=["feedback"])

MAX_COMMENT_LENGTH = 1000
RATING_VALUES = {"5", "4", "3", "2", "1", "NA"}

# updatePMRemarks @Status
STATUS_REJECTED = 0
STATUS_APPROVED = 3

MAIL_WARNING = "The details were saved, but the notification mail could not be sent."


def _header(table: db.Table, name_column: str) -> dict:
    return {
        "employeeId": table.text(0, "Employee_id"),
        "name": table.text(0, name_column),
        "dates": f'{table.text(0, "startdate")} To {table.text(0, "enddate")}',
        "location": table.text(0, "location"),
    }


@router.get("/my")
def my_trainings(user: User = Depends(current_user)) -> dict:
    """The three lists on the Record My Feedback page."""
    params = {"empid": user.emp_id, "IsAdmin": "true" if user.is_admin else "false"}
    return {
        "trainings": db.call_proc("usp_getTrainingEmpidWise", params).first.records(),
        "supervisor": db.call_proc("usp_getTrainingSupWise", params).first.records(),
        "reviews": db.call_proc("usp_getReviews", {"PM_ID": user.emp_id}).first.records(),
    }


# --- trainee feedback -------------------------------------------------------


def _feedback_count(session_id: str, emp_id: str) -> int:
    table = db.call_proc(
        "usp_getFeedBackCount", {"sessionid": session_id, "empid": emp_id}
    ).first
    return int(table.rows[0][0] or 0) if len(table) else 0


@router.get("/context")
def feedback_context(session_id: str, course_id: str, user: User = Depends(current_user)) -> dict:
    table = db.call_proc(
        "usp_getindividual",
        {"empid": user.emp_id, "sessionid": session_id, "courseid": course_id, "Flag": 0},
    ).first
    if len(table) == 0:
        raise UserError("You are not nominated for this training.", 404)
    return {
        **_header(table, "TRAINEENAME"),
        "trainers": trainers_for_session(session_id, user.emp_id),
        # The session-wide questions are asked once, with the first trainer.
        "firstFeedback": _feedback_count(session_id, user.emp_id) == 0,
    }


class FeedbackIn(BaseModel):
    sessionId: str
    courseId: str
    trainerId: str
    administration: int | None = None
    design: int | None = None
    delivery: int | None = None
    comments: str


def _rating(value: int | None) -> bool:
    return value is not None and 1 <= value <= 5


@router.post("")
def submit_feedback(body: FeedbackIn, user: User = Depends(current_user)) -> dict:
    remaining = trainers_for_session(body.sessionId, user.emp_id)
    if body.trainerId not in remaining:
        raise UserError("Please Select Trainers")
    comments = body.comments.strip()
    if not comments:
        raise UserError("Please enter your comments")
    if len(comments) > MAX_COMMENT_LENGTH:
        raise UserError(f"Comments are limited to {MAX_COMMENT_LENGTH} characters.")

    first = _feedback_count(body.sessionId, user.emp_id) == 0
    if not _rating(body.delivery) or (
        first and not (_rating(body.administration) and _rating(body.design))
    ):
        raise UserError("Please answer every question.")

    db.call_proc(
        "usp_Insertfeedbacks_V1",
        {
            "courseid": body.courseId,
            "sessionid": body.sessionId,
            "employee": user.emp_id,
            "response1": body.administration if first else 0,
            "response2": body.design if first else 0,
            "response3": body.delivery,
            "comments": comments,
            "TrainerId": body.trainerId,
            "CommonFlag": 1 if first else 0,
        },
    )
    return {"remainingTrainers": [t for t in remaining if t != body.trainerId]}


# --- supervisor ratings -----------------------------------------------------


def _supervisor_details(supervisor_id: str, session_id: str, pm_id: str | None) -> db.ProcResult:
    return db.call_proc(
        "usp_SupervisorFeedback_Details",
        {"empid": supervisor_id, "sessionid": session_id, "pm_id": pm_id},
    )


@router.get("/supervisor")
def supervisor_form(session_id: str, course_id: str, user: User = Depends(current_user)) -> dict:
    header = db.call_proc(
        "usp_getindividual_Supervisor",
        {"empid": user.emp_id, "sessionid": session_id, "courseid": course_id, "Flag": 0},
    ).first
    details = _supervisor_details(user.emp_id, session_id, None)
    subordinates, saved = details.table(0), details.table(1)

    def rating(i: int, column: str) -> str:
        value = saved.text(i, column).strip()
        return value if value in RATING_VALUES else ""

    saved_by_id = {}
    for i in range(len(saved)):
        saved_by_id[saved.text(i, "Subordinate_id")] = {
            "workArea": saved.text(i, "WorkArea"),
            "techSkills": rating(i, "TechSkills"),
            "softSkills": rating(i, "SoftSkills"),
            "performance": rating(i, "Performance"),
            "overallRating": rating(i, "OvrAllRating"),
            "overallFeedback": saved.text(i, "Overall_Feedback"),
        }
    blank = dict.fromkeys(
        ("workArea", "techSkills", "softSkills", "performance", "overallRating", "overallFeedback"), ""
    )
    rows = []
    for row in subordinates.rows:
        sub_id = db.to_text(row[0])
        rows.append({"subordinateId": sub_id, "name": db.to_text(row[1]), **saved_by_id.get(sub_id, blank)})

    return {
        "header": _header(header, "Supervisor") if len(header) else None,
        "rows": rows,
    }


class SupervisorRow(BaseModel):
    subordinateId: str
    workArea: str = ""
    techSkills: str = ""
    softSkills: str = ""
    performance: str = ""
    overallRating: str = ""
    overallFeedback: str = ""


class SupervisorIn(BaseModel):
    sessionId: str
    courseId: str
    submit: bool
    rows: list[SupervisorRow]


def _check_complete(row: SupervisorRow, name: str) -> None:
    if not row.workArea.strip():
        raise UserError(f"Please provide Work Area for {name}")
    for value, label in (
        (row.techSkills, "rating for Technical Skills of"),
        (row.softSkills, "rating for Soft skills of"),
        (row.performance, "rating for Performance of"),
        (row.overallRating, "Overall rating for"),
    ):
        if value not in RATING_VALUES:
            raise UserError(f"Please provide {label} {name}")
    if not row.overallFeedback.strip():
        raise UserError(f"Please provide Feedback for {name}")


@router.post("/supervisor")
def save_supervisor(body: SupervisorIn, user: User = Depends(current_user)) -> dict:
    subordinates = _supervisor_details(user.emp_id, body.sessionId, None).table(0)
    names = {db.to_text(r[0]): db.to_text(r[1]) for r in subordinates.rows}
    if not body.rows:
        raise UserError("There are no trainees to rate.")
    for row in body.rows:
        if row.subordinateId not in names:
            raise UserError("This trainee does not report to you for the selected session.", 403)
        if body.submit:
            _check_complete(row, names[row.subordinateId])

    for row in body.rows:
        db.call_proc(
            "usp_InsertSupFeedbacks",
            {
                "courseid": body.courseId,
                "sessionid": body.sessionId,
                "SupervisorId": user.emp_id,
                "SubOrdId": row.subordinateId,
                # An unselected rating is stored as "0", as the old drop-downs did.
                "TechSkills": row.techSkills or "0",
                "SoftSkills": row.softSkills or "0",
                "Performance": row.performance or "0",
                "OvrAllRating": row.overallRating or "0",
                "OverallFeed": row.overallFeedback,
                "WorkArea": row.workArea,
                "Status": "1" if body.submit else "0",
            },
        )

    if not body.submit:
        return {"message": "Feedback Saved SuccessFully"}

    warning = None
    try:
        mail, _ = mailer.build_supervisor_submitted_mail(user.emp_id, body.courseId, body.sessionId)
        mailer.send(mail, user.emp_id, body.courseId, body.sessionId)
    except Exception:
        log.exception("Supervisor feedback mail failed for %s / %s", user.emp_id, body.sessionId)
        warning = MAIL_WARNING
    return {"message": "Feedback Submitted SuccessFully", "warning": warning}


# --- PM review --------------------------------------------------------------

REVIEW_COLUMNS = [
    ("Subordinate_Id", "Trainee's Employee Id"),
    ("Subordinate_Name", "Trainee's Name"),
    ("WorkArea", "Mention Work Area (1st 90 days). Specify the area covered by the Trainee."),
    ("TechSkills", "Technical Skills"),
    ("SoftSkills", "Soft Skills"),
    ("PrbSA", "Problem Solving Ability"),
    ("PrgSkills", "Programming skill / ability or Skill in Automation Tools"),
    ("CommSkills", "Communication / Writing Skill Ability"),
    ("BehSkills", "Behavioural Skill [Team Work, Assertiveness]"),
    ("Performance", "Performance"),
    ("OvrAllRating", "Overall Feedback Rating"),
    ("TrnReq", "Is retraining required?If yes, please specify on what skills, in the comments field"),
    ("Overall_Feedback", "Overall Comments for the trainee based on the specified parameters"),
]


def split_supervisor(value: str) -> tuple[str, str]:
    """'Name-12345' -> ('Name', '12345'); the name itself may contain hyphens."""
    name, _, emp_id = value.rpartition("-")
    return name.strip(), emp_id.strip()


def _submitted_ratings(supervisor_id: str, session_id: str, pm_id: str) -> db.Table:
    ratings = _supervisor_details(supervisor_id, session_id, pm_id).table(1)
    if len(ratings) == 0:
        return ratings
    status = ratings.index("Status")
    return db.Table(ratings.columns, [r for r in ratings.rows if db.to_text(r[status]) == "1"])


@router.get("/review")
def review_form(session_id: str, supervisor: str, user: User = Depends(current_user)) -> dict:
    name, supervisor_id = split_supervisor(supervisor)
    ratings = _submitted_ratings(supervisor_id, session_id, user.emp_id)
    if len(ratings) == 0:
        raise UserError("There are no ratings waiting for your review.", 404)

    shown = [(c, label) for c, label in REVIEW_COLUMNS if ratings.has(c)]
    return {
        "header": {
            "employeeId": supervisor_id,
            "name": name,
            "course": ratings.text(0, "Course"),
            "dates": f'{ratings.text(0, "startdate")} To {ratings.text(0, "enddate")}',
            "location": ratings.text(0, "Location"),
        },
        "columns": [label for _, label in shown],
        "rows": [[ratings.text(i, c) for c, _ in shown] for i in range(len(ratings))],
    }


class ReviewIn(BaseModel):
    sessionId: str
    courseId: str
    supervisor: str
    remarks: str
    approve: bool


@router.post("/review")
def submit_review(body: ReviewIn, user: User = Depends(current_user)) -> dict:
    if not body.remarks.strip():
        raise UserError("Please provide Remarks")
    _, supervisor_id = split_supervisor(body.supervisor)
    if len(_submitted_ratings(supervisor_id, body.sessionId, user.emp_id)) == 0:
        raise UserError("There are no ratings waiting for your review.", 404)

    db.call_proc(
        "usp_UpdatePMRemarks",
        {
            "Status": STATUS_APPROVED if body.approve else STATUS_REJECTED,
            "Remarks": body.remarks,
            "PM_ID": user.emp_id,
            "sessionid": body.sessionId,
            "SupervisorId": supervisor_id,
        },
    )

    warning = None
    try:
        status = "Approved" if body.approve else "Rejected"
        mail = mailer.build_review_mail(supervisor_id, body.courseId, body.sessionId, status)
        mailer.send(mail, supervisor_id, body.courseId, body.sessionId)
    except Exception:
        log.exception("Review mail failed for %s / %s", supervisor_id, body.sessionId)
        warning = MAIL_WARNING
    return {"message": f"Ratings {'approved' if body.approve else 'rejected'}.", "warning": warning}
