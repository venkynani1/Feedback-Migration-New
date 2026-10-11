from __future__ import annotations

from dataclasses import dataclass

from fastapi import Depends, HTTPException, Request, status


@dataclass(frozen=True)
class User:
    emp_id: str
    name: str
    role: str  # "user", "admin" or "super"

    @property
    def is_admin(self) -> bool:
        return self.role in ("admin", "super")


def current_user(request: Request) -> User:
    data = request.session.get("user")
    if not data:
        raise HTTPException(status.HTTP_401_UNAUTHORIZED, "Not signed in")
    return User(emp_id=data["emp_id"], name=data.get("name", ""), role=data["role"])


def require_admin(user: User = Depends(current_user)) -> User:
    if not user.is_admin:
        raise HTTPException(status.HTTP_403_FORBIDDEN, "Administrator access required")
    return user


class UserError(HTTPException):
    """A validation or business-rule message meant to be shown to the user."""

    def __init__(self, message: str, status_code: int = status.HTTP_400_BAD_REQUEST):
        super().__init__(status_code, message)
