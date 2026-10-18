"""Sign-in throttling and the checks that keep unsafe settings out of production."""

from __future__ import annotations

import pytest
from fastapi.testclient import TestClient

from app import db
from app.config import Settings, get_settings
from app.main import create_app
from app.routers import auth


@pytest.fixture
def passwords(monkeypatch, fake_db):
    """StationH stand-in: the password is "right"."""
    monkeypatch.setattr(auth.soap_auth, "is_valid_user", lambda username, password: password == "right")
    fake_db.on("usp_IsAdmin", db.Table(["IsSuperAdmin"], []))
    auth._failures.clear()
    yield
    auth._failures.clear()


def test_user_id_is_locked_after_repeated_failures(client, passwords):
    bad = {"username": "Guess.Me", "password": "wrong"}
    for _ in range(get_settings().login_max_failures):
        assert client.post("/api/auth/login", json=bad).status_code == 401
    # Locked now, whatever the letter case and even with the right password.
    locked = client.post("/api/auth/login", json={"username": "guess.me", "password": "right"})
    assert locked.status_code == 429
    # Someone else is not affected.
    assert client.post("/api/auth/login", json={"username": "other", "password": "right"}).status_code == 200


def test_a_successful_sign_in_clears_earlier_failures(client, passwords):
    for _ in range(get_settings().login_max_failures - 1):
        client.post("/api/auth/login", json={"username": "u1", "password": "wrong"})
    assert client.post("/api/auth/login", json={"username": "u1", "password": "right"}).status_code == 200
    assert "u1" not in auth._failures


def _settings(**overrides) -> Settings:
    safe = dict(environment="production", secret_key="k" * 48, demo_mode=False, auth_mode="soap",
                cookie_secure=True, db_connection_string="DRIVER=x")
    return Settings(_env_file=None, **{**safe, **overrides})


def test_safe_production_settings_have_no_problems():
    assert _settings().production_problems() == []


@pytest.mark.parametrize("override", [
    {"demo_mode": True}, {"auth_mode": "dev"}, {"cookie_secure": False},
    {"secret_key": "short"}, {"db_connection_string": ""},
])
def test_production_refuses_unsafe_settings(override, monkeypatch):
    settings = _settings(**override)
    assert len(settings.production_problems()) == 1
    monkeypatch.setattr("app.main.get_settings", lambda: settings)
    with pytest.raises(RuntimeError, match="Refusing to start in production"):
        create_app()


def test_production_hides_api_pages_but_answers_health(monkeypatch, tmp_path):
    settings = _settings(frontend_dist=tmp_path / "missing")
    monkeypatch.setattr("app.main.get_settings", lambda: settings)
    client = TestClient(create_app())
    assert client.get("/api/docs").status_code == 404
    assert client.get("/api/openapi.json").status_code == 404
    assert client.get("/api/health").json() == {"status": "ok"}


def test_readiness_reports_the_database(client, monkeypatch):
    monkeypatch.setattr(db, "ping", lambda: None)
    assert client.get("/api/health/ready").json() == {"status": "ok", "database": "ok"}

    def unreachable():
        raise RuntimeError("login timeout on cch1wdsthstg")

    monkeypatch.setattr(db, "ping", unreachable)
    response = client.get("/api/health/ready")
    assert response.status_code == 503
    # The server name and error stay in the log, not in the public answer.
    assert "cch1wdsthstg" not in response.text


def test_login_page_is_told_the_idle_timeout(client):
    assert client.get("/api/auth/config").json()["sessionMinutes"] == get_settings().session_minutes
