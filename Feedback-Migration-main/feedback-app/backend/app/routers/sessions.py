"""Create New Session."""

from __future__ import annotations

from datetime import date

from fastapi import APIRouter, Depends
from pydantic import BaseModel

from .. import db
from ..deps import User, UserError, require_admin

router = APIRouter(prefix="/api/sessions", tags=["sessions"])

# The old app capped participants at 500 from 27 Apr 2015 onwards.
MAX_PARTICIPANTS = 500


def short_date(value: date) -> str:
    """M/d/yyyy, the format existing session ids were built with."""
    return f"{value.month}/{value.day}/{value.year}"


def build_session_id(location: str, course_id: str, start: date, end: date) -> str:
    return f"{location[:3]}_{course_id}_{short_date(start)}To{short_date(end)}"


def existing_count(session_id: str) -> int:
    table = db.call_proc("usp_CheckSessionAvailability", {"sessionid": session_id}).first
    return int(table.value(0, "SessionCount") or 0) if len(table) else 0


class SessionKey(BaseModel):
    courseId: str
    location: str
    startDate: date
    endDate: date


class SessionIn(SessionKey):
    courseName: str
    trainers: list[str]
    # True when the admin confirmed creating another session for the same
    # course, location and dates.
    createAnother: bool = False


def _validate(body: SessionKey) -> None:
    if not body.courseId:
        raise UserError("Please Select Course Name.")
    if not body.location:
        raise UserError("Please Select Location Name.")
    if body.startDate > date.today():
        raise UserError("From Date should be current date or less than current date.")
    if body.startDate > body.endDate:
        raise UserError("From Date should be less than or equal to To date.")


@router.post("/check")
def check(body: SessionKey, _: User = Depends(require_admin)) -> dict:
    _validate(body)
    session_id = build_session_id(body.location, body.courseId, body.startDate, body.endDate)
    return {"sessionId": session_id, "existingCount": existing_count(session_id)}


@router.post("")
def create(body: SessionIn, user: User = Depends(require_admin)) -> dict:
    _validate(body)
    trainers = [t.strip() for t in body.trainers if t.strip()]
    if not trainers:
        raise UserError("Minimum one trainers should select for session.")

    session_id = build_session_id(body.location, body.courseId, body.startDate, body.endDate)
    if body.createAnother:
        count = existing_count(session_id)
        if count > 0:
            session_id = f"{session_id}_{count}"

    out = db.call_proc(
        "usp_InsertSession",
        {
            "courseid": body.courseId,
            "courseName": body.courseName,
            "sessionid": session_id,
            "startdate": body.startDate,
            "enddate": body.endDate,
            "max_participants": MAX_PARTICIPANTS,
            # The old page always sent an empty trainer name here; trainers are
            # stored per session by usp_InsertSessionTrainerDetails below.
            "Trainername": "",
            "location": body.location,
            "createdby": user.emp_id,
        },
        outputs={"IsExistsAlready": "int"},
    ).outputs
    if out.get("IsExistsAlready"):
        raise UserError("Same session already Exist.", 409)

    for trainer in trainers:
        db.call_proc(
            "usp_InsertSessionTrainerDetails", {"sessionid": session_id, "TrainerID": trainer}
        )
    return {"sessionId": session_id, "message": f"Session created successfully.\nSession ID: {session_id}"}
