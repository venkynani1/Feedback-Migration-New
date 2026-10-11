from functools import lru_cache
from pathlib import Path
from typing import Literal

from pydantic_settings import BaseSettings, SettingsConfigDict

BACKEND_DIR = Path(__file__).resolve().parent.parent


class Settings(BaseSettings):
    model_config = SettingsConfigDict(
        env_file=BACKEND_DIR / ".env", env_prefix="FEEDBACK_", extra="ignore"
    )

    # "production" refuses to start with any of the shortcuts below switched on.
    environment: Literal["production", "development"] = "production"
    secret_key: str
    # Sample data and demo logins instead of SQL Server and StationH (see demo.py).
    demo_mode: bool = False
    db_connection_string: str = ""
    db_login_timeout_seconds: int = 15

    auth_mode: Literal["soap", "dev"] = "soap"
    soap_auth_url: str = (
        "http://che1wpisg18/StationHAuthenticationold/StationHAuthentication.asmx"
    )
    soap_timeout_seconds: float = 15.0

    session_minutes: int = 30
    cookie_secure: bool = True
    # Failed sign-ins allowed for one user id before it is locked for a while.
    login_max_failures: int = 5
    login_lockout_minutes: int = 15

    # Rotating log file; empty logs to the console only.
    log_file: str = ""

    # The share the old app wrote to. Its COMMONPATH setting had no trailing
    # slash, so files landed in this folder, not in a TrainingFeedback subfolder.
    upload_dir: str = r"\\CCH1WPNTDB\StationhReportSource"
    # The old app allowed the Excel import procedures 2500 seconds.
    import_timeout_seconds: int = 2500

    frontend_dist: Path = BACKEND_DIR.parent / "frontend" / "dist"
    assets_dir: Path = BACKEND_DIR / "assets"

    def production_problems(self) -> list[str]:
        """Settings that must not reach production; empty when all is well."""
        problems = []
        if self.demo_mode:
            problems.append("FEEDBACK_DEMO_MODE is on")
        if self.auth_mode != "soap":
            problems.append("FEEDBACK_AUTH_MODE is not soap, so passwords are not checked")
        if not self.cookie_secure:
            problems.append("FEEDBACK_COOKIE_SECURE is off")
        if len(self.secret_key) < 32:
            problems.append("FEEDBACK_SECRET_KEY is shorter than 32 characters")
        if not self.db_connection_string:
            problems.append("FEEDBACK_DB_CONNECTION_STRING is empty")
        return problems


@lru_cache
def get_settings() -> Settings:
    return Settings()
