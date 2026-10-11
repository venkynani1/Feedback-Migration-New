"""Course, vendor, trainer and mail masters, plus the employee search."""

from __future__ import annotations

import re

from fastapi import APIRouter, Depends
from pydantic import BaseModel

from .. import db
from ..deps import User, UserError, require_admin

router = APIRouter(prefix="/api", tags=["masters"])

_EMAIL = re.compile(r"[\w.+'-]+@[\w-]+(\.[\w-]+)+")


def valid_cc_list(value: str) -> bool:
    """Empty, or e-mail addresses separated by semicolons."""
    return all(_EMAIL.fullmatch(part) for part in value.split(";") if part)


class CourseIn(BaseModel):
    courseId: str
    courseName: str
    description: str
    internalExternal: str
    courseType: str


@router.post("/courses")
def create_course(body: CourseIn, user: User = Depends(require_admin)) -> dict:
    if not all(
        v.strip() for v in (body.courseId, body.courseName, body.description, body.courseType)
    ) or body.internalExternal not in ("I", "E"):
        raise UserError("Please provide all information")

    out = db.call_proc(
        "usp_InsertCourse",
        {
            "courseId": body.courseId.strip(),
            "course": body.courseName.strip(),
            "Description": body.description.strip(),
            "InternalExternal": body.internalExternal,
            "course_type": body.courseType,
            "createdby": user.emp_id,
        },
        outputs={"IsCourseIdExist": "int", "IsCourseNameExist": "int", "RowAffected": "int"},
    ).outputs
    # The procedure inserts nothing when either the id or the name is taken. The
    # old page only reported it when both were, and otherwise claimed success.
    id_taken, name_taken = out.get("IsCourseIdExist") == 1, out.get("IsCourseNameExist") == 1
    if id_taken and name_taken:
        raise UserError("Course Already exist", 409)
    if id_taken:
        raise UserError("Course ID already exist", 409)
    if name_taken:
        raise UserError("Course Name already exist", 409)
    return {"message": "Course successfully created"}


class VendorIn(BaseModel):
    vendorName: str
    prefixId: str


@router.post("/vendors")
def create_vendor(body: VendorIn, _: User = Depends(require_admin)) -> dict:
    if not body.vendorName.strip() or not body.prefixId.strip():
        raise UserError("Please provide all details.")
    if not body.prefixId.strip().isdigit():
        raise UserError("Please Enter Numeric Only")

    out = db.call_proc(
        "usp_InsertVendorDetails",
        {"VendorName": body.vendorName.strip(), "PrefixID": body.prefixId.strip()},
        outputs={"IsExistsAlready": "int"},
    ).outputs
    if out.get("IsExistsAlready"):
        raise UserError("Vendor Details are exist.", 409)
    return {"message": "Vendor Details are successfully Saved."}


class TrainerIn(BaseModel):
    courseId: str
    trainerType: str
    trainerId: str | None = None
    trainerName: str
    vendorName: str | None = None


@router.post("/trainers")
def create_trainer(body: TrainerIn, _: User = Depends(require_admin)) -> dict:
    if not body.trainerName.strip() or not body.courseId or body.trainerType not in ("I", "E"):
        raise UserError("Please provide all details.")

    if body.trainerType == "E":
        # External trainers belong to a vendor and get an auto-generated id.
        if not body.vendorName:
            raise UserError("Please select Vendor Name.")
        trainer_id, vendor = None, body.vendorName
    else:
        if not (body.trainerId or "").strip():
            raise UserError("Please Enter Valid Employee ID.")
        trainer_id, vendor = body.trainerId.strip(), None

    out = db.call_proc(
        "usp_InsertTrainerDetails",
        {
            "TrainerId": trainer_id,
            "TrainerName": body.trainerName.strip(),
            "TrainerType": body.trainerType,
            "VendorName": vendor,
            "course_Id": body.courseId,
        },
        outputs={"IsExistsAlready": "int"},
    ).outputs
    if out.get("IsExistsAlready"):
        raise UserError("Trainer Details are exist. ", 409)
    return {"message": "Trainer Details are successfully Saved."}


def _mail_type(list_type: int) -> int:
    """Page option 0 = Nomination List, 1 = Supervisor's List -> Mail Master type 1 / 2."""
    if list_type not in (0, 1):
        raise UserError("Unknown mail type")
    return list_type + 1


@router.get("/mail-master")
def get_mail_master(type: int = 0, user: User = Depends(require_admin)) -> dict:
    table = db.call_proc("usp_SelectMailMasterDetails", {"Type": _mail_type(type)}).first
    sender = db.call_proc(
        "usp_getindividual",
        {"empid": user.emp_id, "sessionid": None, "courseid": None, "Flag": 1},
    ).first
    found = len(table) > 0
    return {
        "from": sender.text(0, "ReplyMailID") if len(sender) else "",
        "cc": table.text(0, "CccMail") if found else "",
        "subject": table.text(0, "Subject") if found else "",
        "body": table.text(0, "Body") if found else "",
    }


class MailMasterIn(BaseModel):
    type: int
    cc: str
    subject: str
    body: str


@router.put("/mail-master")
def update_mail_master(body: MailMasterIn, _: User = Depends(require_admin)) -> dict:
    if not valid_cc_list(body.cc):
        raise UserError("Invalid Email Format")
    db.call_proc(
        "usp_InsertMailDetails",
        {
            "FrmMail": None,
            "BccMail": None,
            "CccMail": body.cc,
            "Subject": body.subject,
            "Body": body.body,
            "URL": None,
            "Type": _mail_type(body.type),
        },
    )
    return {"message": "Mail Details are successfully updated"}


@router.get("/employees/search")
def search_employees(
    emp_id: str = "", name: str = "", _: User = Depends(require_admin)
) -> dict:
    return db.call_proc(
        "usp_getHexaViewEmployees", {"empid": emp_id.strip(), "EmplName": name.strip()}
    ).first.to_json()
