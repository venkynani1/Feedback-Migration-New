from __future__ import annotations

import logging

from fastapi import APIRouter, Depends, Request
from pydantic import BaseModel

from .. import db, soap_auth
from ..config import get_settings
from ..deps import User, UserError, current_user

log = logging.getLogger(__name__)
router = APIRouter(prefix="/api/auth", tags=["auth"])


class LoginIn(BaseModel):
    username: str
    password: str


def employee_name(emp_id: str) -> str:
    """Display name from HexaView (usp_getindividual with @Flag = 1)."""
    table = db.call_proc(
        "usp_getindividual",
        {"empid": emp_id, "sessionid": None, "courseid": None, "Flag": 1},
    ).first
    return table.text(0, "AdminName") if len(table) else ""


def _me(user: User) -> dict:
    return {"empId": user.emp_id, "name": user.name, "role": user.role}


@router.get("/config")
def config() -> dict:
    """Tells the login page whether this is a demo with sample data."""
    if not get_settings().demo_mode:
        return {"demo": False, "logins": []}
    from .. import demo

    return {"demo": True, "logins": demo.login_hints()}


@router.post("/login")
def login(body: LoginIn, request: Request) -> dict:
    username = body.username.strip()
    if not username:
        raise UserError("User Name is required.")
    if not body.password:
        raise UserError("Password is required.")

    try:
        valid = soap_auth.is_valid_user(username, body.password)
    except soap_auth.AuthServiceError:
        log.exception("StationH authentication failed for %s", username)
        raise UserError("The authentication service is unavailable. Please try again later.", 503)
    if not valid:
        raise UserError("Your login attempt was not successful. Please try again.", 401)

    # StationH logins use the long id; the feedback tables use the 5 digit id.
    ids = db.call_proc("usp_GetFiveDigitEmpID", {"empid": username}).first
    emp_id = ids.text(0, "EMPLID") if len(ids) else ""
    if not emp_id:
        raise UserError("Five digit id not available for this employee", 403)

    admin = db.call_proc("usp_IsAdmin", {"empid": emp_id}).first
    if len(admin) == 0:
        role = "user"
    else:
        role = "super" if int(admin.value(0, "IsSuperAdmin") or 0) else "admin"

    user = User(emp_id=emp_id, name=employee_name(emp_id), role=role)
    request.session.clear()
    request.session["user"] = {"emp_id": user.emp_id, "name": user.name, "role": user.role}
    return _me(user)


@router.post("/logout")
def logout(request: Request) -> dict:
    request.session.clear()
    return {"ok": True}


@router.get("/me")
def me(user: User = Depends(current_user)) -> dict:
    return _me(user)
