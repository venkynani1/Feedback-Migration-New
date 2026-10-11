"""Excel Data Upload / View and the Learning Program Report.

The import procedures read the workbook themselves (OPENDATASOURCE), so the
upload is saved to a folder SQL Server can reach and its path is passed on.
"""

from __future__ import annotations

import logging
import re
import tempfile
from datetime import date, datetime
from pathlib import Path

from fastapi import APIRouter, Depends, Form, Response, UploadFile
from pydantic import BaseModel

from .. import db, exports
from ..config import get_settings
from ..deps import User, UserError, require_admin

log = logging.getLogger(__name__)
router = APIRouter(prefix="/api/skillport", tags=["skillport"])

MAX_UPLOAD_BYTES = 50 * 1024 * 1024
FIRST_YEAR = 2010
# The old page saved uploads straight into the StationhReportSource share with
# this in front of the file name; keeping it leaves them recognisable there.
UPLOAD_PREFIX = "TrainingFeedback"

# option label -> (check procedure, import procedure, delete procedure)
UPLOAD_TYPES: dict[str, tuple[str, str, str]] = {
    "Learning Plan for Behavioral": (
        "CheckBehavioralRecordDuplicate",
        "InsertBehaviorSummaryData",
        "DeleteBehavioralDuplicateRecords",
    ),
    "Learning Plan for TCDP": (
        "CheckTCDPRecordDuplicate",
        "InsertTCDPData",
        "DeleteTCDPDuplicateRecords",
    ),
    "Certifications": (
        "CheckCertificationsRecordDuplicate",
        "InsertCertificationsData",
        "DeleteCertificationsDuplicateRecords",
    ),
    "Contributions": (
        "CheckContributionsRecordDuplicate",
        "InsertContributionsData",
        "DeleteContributionsDuplicateRecords",
    ),
    "Learning Plan for PMS": (
        "CheckRecordDuplicate",
        "InsertSkillPortData",
        "DeleteDuplicateRecords",
    ),
    "TrainingHours": (
        "CheckTrainingHoursRecordDuplicate",
        "InsertTrainingHoursData",
        "DeleteTrainingHoursDuplicateRecords",
    ),
}


def _procs(upload_type: str) -> tuple[str, str, str]:
    try:
        return UPLOAD_TYPES[upload_type]
    except KeyError:
        raise UserError("Please select Radio Button") from None


def _period(month: int, year: int) -> dict[str, str]:
    today = date.today()
    if not 1 <= month <= 12 or not FIRST_YEAR <= year <= today.year:
        raise UserError("Please select the Month & Year")
    return {"MONTH": str(month), "YEAR": str(year)}


def _existing(upload_type: str, month: int, year: int) -> db.Table:
    return db.call_proc(_procs(upload_type)[0], _period(month, year)).first


@router.get("/types")
def types(_: User = Depends(require_admin)) -> dict:
    return {"types": list(UPLOAD_TYPES), "firstYear": FIRST_YEAR}


@router.get("/records")
def records(upload_type: str, month: int, year: int, _: User = Depends(require_admin)) -> dict:
    table = _existing(upload_type, month, year)
    if len(table) == 0:
        raise UserError("No records to display.", 404)
    return table.to_json()


def stored_file_name(emp_id: str, original: str, now: datetime) -> str:
    """Unique name for the saved upload.

    The import procedures were written against '.xls' paths, so '.xlsx'
    uploads keep the old page's behaviour of being saved with an '.xls' name.
    """
    stem = re.sub(r"[^A-Za-z0-9_-]", "_", Path(original).stem)[:60] or "upload"
    return f"{UPLOAD_PREFIX}_{emp_id}_{now:%Y%m%d%H%M%S%f}_{stem}.xls"


@router.post("/upload")
async def upload(
    file: UploadFile,
    upload_type: str = Form(...),
    month: int = Form(...),
    year: int = Form(...),
    user: User = Depends(require_admin),
) -> dict:
    period = _period(month, year)
    today = date.today()
    if year == today.year and month > today.month:
        raise UserError("Please check the month of your request.")
    _, import_proc, _ = _procs(upload_type)

    name = file.filename or ""
    if Path(name).suffix.lower() not in (".xls", ".xlsx"):
        raise UserError("The file should be in excel format only.")
    content = await file.read(MAX_UPLOAD_BYTES + 1)
    if not content:
        raise UserError("Please select the file to upload")
    if len(content) > MAX_UPLOAD_BYTES:
        raise UserError("The file is too large.")

    existing = _existing(upload_type, month, year)
    if len(existing) > 0:
        return {
            "saved": 0,
            "message": "Some records already exists. Please delete those records to add new ones",
            "existing": existing.to_json(),
        }

    settings = get_settings()
    # Demo mode has no SQL Server to share a folder with.
    folder = Path(tempfile.gettempdir()) / "feedback-demo" if settings.demo_mode else Path(settings.upload_dir)
    folder.mkdir(parents=True, exist_ok=True)
    path = folder / stored_file_name(user.emp_id, name, datetime.now())
    path.write_bytes(content)
    try:
        result = db.call_proc(
            import_proc,
            {"EMPID": user.emp_id, "FILEPATH": str(path), **period},
            timeout=settings.import_timeout_seconds,
        ).first
        saved = int(result.rows[0][0] or 0) if len(result) else 0
    except Exception:
        log.exception("Import %s failed for %s", import_proc, path)
        raise UserError("Please check the columns before upload") from None
    finally:
        path.unlink(missing_ok=True)

    if saved <= 0:
        raise UserError("Invalid File. Please Check the Columns of the File.")
    return {"saved": saved, "message": f"Totally {saved} Record(s) saved successfully"}


class DeleteIn(BaseModel):
    uploadType: str
    month: int
    year: int


@router.post("/delete")
def delete(body: DeleteIn, _: User = Depends(require_admin)) -> dict:
    delete_proc = _procs(body.uploadType)[2]
    deleted = db.call_proc_rowcount(
        delete_proc,
        _period(body.month, body.year),
        timeout=get_settings().import_timeout_seconds,
    )
    if deleted == 0:
        raise UserError("Deletion failed. Please try after some time")
    return {"deleted": deleted, "message": f"Totally {deleted} Record(s) deleted successfully"}


def _learning_report() -> db.Table:
    return db.call_proc("GetSkillportAssessmentReport").first


@router.get("/learning-report")
def learning_report(_: User = Depends(require_admin)) -> dict:
    return _learning_report().to_json()


@router.get("/learning-report/excel")
def learning_report_excel(_: User = Depends(require_admin)) -> Response:
    table = _learning_report()
    if len(table) == 0:
        raise UserError("No records to display.", 404)
    return exports.download(
        exports.workbook_bytes(table.columns, table.rows),
        "LearningProgramReport.xlsx",
        exports.XLSX,
    )
