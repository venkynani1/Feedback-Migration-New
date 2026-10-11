"""Drop-down data shared by several pages."""

from __future__ import annotations

from fastapi import APIRouter, Depends

from .. import db
from ..deps import User, current_user, require_admin
from .auth import employee_name

router = APIRouter(prefix="/api/lookups", tags=["lookups"])


def trainers_for_session(session_id: str, emp_id: str | None = None) -> list[str]:
    """Trainers of a session; with emp_id, only those the employee has not rated yet."""
    table = db.call_proc(
        "usp_getTrainersForSessionid", {"sessionid": session_id, "empid": emp_id}
    ).first
    return [db.to_text(v) for v in table.column("TrainerID")] if len(table) else []


def session_details(session_id: str) -> db.Table:
    """One row per trainer: startdate, enddate, max_participants, Trainer Name, PreparedDate."""
    return db.call_proc("usp_getSessionSessionIdWise", {"sessionid": session_id}).first


@router.get("/years")
def years(_: User = Depends(require_admin)) -> list[int]:
    table = db.call_proc("usp_getYear").first
    return [int(v) for v in table.column("code_id")] if len(table) else []


@router.get("/courses")
def courses(_: User = Depends(require_admin)) -> list[dict]:
    table = db.call_proc("usp_getAllCourse").first
    return [
        {"id": db.to_text(table.value(i, "courseid")), "name": db.to_text(table.value(i, "course"))}
        for i in range(len(table))
    ]


@router.get("/course-types")
def course_types(_: User = Depends(require_admin)) -> list[str]:
    table = db.call_proc("usp_getCourseType").first
    return [db.to_text(v) for v in table.column("CourceTypeName")] if len(table) else []


@router.get("/sessions")
def sessions(course_id: str, year: int, _: User = Depends(require_admin)) -> list[str]:
    table = db.call_proc(
        "usp_getSessionCourseWise", {"courseid": course_id, "year": year}
    ).first
    return [db.to_text(v) for v in table.column("sessionid")] if len(table) else []


@router.get("/session-details")
def session_dates(session_id: str, _: User = Depends(require_admin)) -> dict:
    table = session_details(session_id)
    if len(table) == 0:
        return {"startDate": "", "endDate": ""}
    return {"startDate": table.text(0, "startdate"), "endDate": table.text(0, "enddate")}


@router.get("/session-trainers")
def session_trainers(session_id: str, _: User = Depends(require_admin)) -> list[str]:
    return trainers_for_session(session_id)


@router.get("/session-employees")
def session_employees(session_id: str, _: User = Depends(require_admin)) -> list[str]:
    table = db.call_proc("usp_getEmpIdForSessionid", {"sessionid": session_id}).first
    return [db.to_text(v) for v in table.column("Employee_id")] if len(table) else []


@router.get("/vendors")
def vendors(_: User = Depends(require_admin)) -> list[str]:
    table = db.call_proc(
        "usp_SelectVendorTrainerDetails", {"vendorName": None, "flag": 1}
    ).first
    return [db.to_text(v) for v in table.column("VendorName")] if len(table) else []


@router.get("/course-trainers")
def course_trainers(
    course_id: str,
    trainer_type: str,
    vendor_name: str | None = None,
    _: User = Depends(require_admin),
) -> list[str]:
    """Trainers registered for a course, formatted 'Name(Id)'."""
    table = db.call_proc(
        "usp_SelectTrainerDetails",
        {"TrainerType": trainer_type, "vendorName": vendor_name, "courseid": course_id},
    ).first
    return [db.to_text(v) for v in table.column("Trainer")] if len(table) else []


@router.get("/employee")
def employee(emp_id: str, _: User = Depends(require_admin)) -> dict:
    return {"name": employee_name(emp_id.strip())}
