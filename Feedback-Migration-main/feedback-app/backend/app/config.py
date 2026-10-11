from functools import lru_cache
from pathlib import Path
from typing import Literal

from pydantic_settings import BaseSettings, SettingsConfigDict

BACKEND_DIR = Path(__file__).resolve().parent.parent


class Settings(BaseSettings):
    model_config = SettingsConfigDict(
        env_file=BACKEND_DIR / ".env", env_prefix="FEEDBACK_", extra="ignore"
    )

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

    # The share the old app wrote to. Its COMMONPATH setting had no trailing
    # slash, so files landed in this folder, not in a TrainingFeedback subfolder.
    upload_dir: str = r"\\CCH1WPNTDB\StationhReportSource"
    # The old app allowed the Excel import procedures 2500 seconds.
    import_timeout_seconds: int = 2500

    frontend_dist: Path = BACKEND_DIR.parent / "frontend" / "dist"
    assets_dir: Path = BACKEND_DIR / "assets"


@lru_cache
def get_settings() -> Settings:
    return Settings()
