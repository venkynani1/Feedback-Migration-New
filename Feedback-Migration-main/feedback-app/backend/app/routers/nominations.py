"""Upload Participant List & Send Mail."""

from __future__ import annotations

import logging
from io import BytesIO

from fastapi import APIRouter, Depends, UploadFile
from fastapi.responses import FileResponse
from openpyxl import load_workbook
from pydantic import BaseModel

from .. import db, mailer
from ..config import get_settings
from ..deps import User, UserError, require_admin
from ..exports import XLSX

log = logging.getLogger(__name__)
router = APIRouter(prefix="/api/nominations", tags=["nominations"])

# The fresher programme: participants are not mailed, their supervisors are.
FRESHER_COURSE = "FTP_001"
LIST_PARTICIPANTS = 0
LIST_SUPERVISORS = 1
MAX_UPLOAD_BYTES = 10 * 1024 * 1024


def _cell_text(value) -> str:
    if value is None:
        return ""
    if isinstance(value, float) and value.is_integer():
        return str(int(value))
    return str(value).strip()


def read_participant_sheet(content: bytes) -> tuple[list[str], list[list[str]]]:
    """First sheet of the workbook: row 1 is the header, the rest are data."""
    try:
        workbook = load_workbook(BytesIO(content), read_only=True, data_only=True)
    except Exception as exc:  # openpyxl raises several unrelated types
        raise UserError("The file could not be read as an Excel workbook.") from exc
    sheet = workbook.worksheets[0]
    rows = [[_cell_text(v) for v in row] for row in sheet.iter_rows(values_only=True)]
    workbook.close()
    rows = [r for r in rows if any(r)]
    if not rows:
        return [], []
    width = len(rows[0])
    data = [(r + [""] * width)[:width] for r in rows[1:]]
    return rows[0], data


@router.get("/sample")
def sample(_: User = Depends(require_admin)) -> FileResponse:
    return FileResponse(
        get_settings().assets_dir / "SampleNominationList.xlsx",
        media_type=XLSX,
        filename="SampleNominationList.xlsx",
    )


@router.post("/parse")
async def parse(file: UploadFile, _: User = Depends(require_admin)) -> dict:
    if not (file.filename or "").lower().endswith(".xlsx"):
        raise UserError("Your file was not uploaded because it does not have a .xlsx extension.")
    content = await file.read(MAX_UPLOAD_BYTES + 1)
    if len(content) > MAX_UPLOAD_BYTES:
        raise UserError("The file is too large.")
    columns, rows = read_participant_sheet(content)
    if not rows or not rows[0][0]:
        raise UserError("No participants were found in the file.")
    return {"columns": columns, "rows": rows}


@router.get("/supervisors")
def supervisors(session_id: str, _: User = Depends(require_admin)) -> dict:
    """Supervisors of the employees nominated for a session."""
    return db.call_proc("usp_getSupervisorsForMail", {"session_id": session_id}).first.to_json()


class NominationIn(BaseModel):
    listType: int
    courseId: str
    sessionId: str
    employeeIds: list[str]


@router.post("")
def submit(body: NominationIn, user: User = Depends(require_admin)) -> dict:
    if body.listType not in (LIST_PARTICIPANTS, LIST_SUPERVISORS):
        raise UserError("Unknown list type")
    if not body.courseId or not body.sessionId:
        raise UserError("Please select the course and session.")
    employee_ids = [e.strip() for e in body.employeeIds if e and e.strip()]
    if not employee_ids:
        raise UserError("There are no participants to add.")

    signature, reply_to = mailer.signature_details(user.emp_id, body.courseId, body.sessionId)

    max_table = db.call_proc("usp_getMaxnoofParticipants", {"courseid": body.courseId}).first
    max_participants = max_table.rows[0][0] if len(max_table) else None
    if max_participants is None:
        raise UserError("No session exists for the selected course.")
    if int(max_participants) < len(employee_ids) - 1:
        raise UserError(f"Nominate Employee strength should not greater than :{max_participants}")

    sends_mail = body.listType == LIST_SUPERVISORS or body.courseId != FRESHER_COURSE
    already_nominated: list[str] = []
    mail_errors: list[str] = []

    for emp_id in employee_ids:
        out = db.call_proc(
            "usp_InsertNominationList",
            {
                "course_Id": body.courseId,
                "session_id": body.sessionId,
                "Employee_id": emp_id,
                "createdby": user.emp_id,
                "Type": body.listType,
            },
            outputs={"IsExistsAlready": "int", "RowAffected": "int"},
        ).outputs
        if not sends_mail:
            continue
        if out.get("IsExistsAlready"):
            already_nominated.append(emp_id)
            continue
        try:
            if body.listType == LIST_PARTICIPANTS:
                mail = mailer.build_nomination_mail(
                    emp_id, body.courseId, body.sessionId, signature, reply_to
                )
            else:
                mail = mailer.build_supervisor_list_mail(
                    emp_id, body.courseId, body.sessionId, signature, reply_to
                )
            mailer.send(mail, emp_id, body.courseId, body.sessionId)
        except Exception:
            # One bad address must not stop the rest of the list.
            log.exception("Mail to %s for session %s failed", emp_id, body.sessionId)
            mail_errors.append(emp_id)

    if body.listType == LIST_SUPERVISORS:
        db.call_proc("usp_insertMasterSupervisorList", {"session_id": body.sessionId})

    mail_status = db.call_proc(
        "usp_getEmplForMailsendStatus",
        {"course_Id": body.courseId, "session_id": body.sessionId},
    ).first
    return {
        "message": "All nominated peoples are successfully added and mail has been send.",
        "alreadyNominated": already_nominated,
        "mailErrors": mail_errors,
        "mailStatus": mail_status.to_json(),
    }
