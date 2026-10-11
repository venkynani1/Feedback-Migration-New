from __future__ import annotations

import os

os.environ["FEEDBACK_ENVIRONMENT"] = "development"
os.environ["FEEDBACK_SECRET_KEY"] = "test-secret-key"
os.environ["FEEDBACK_AUTH_MODE"] = "dev"
os.environ["FEEDBACK_COOKIE_SECURE"] = "false"
os.environ["FEEDBACK_DB_CONNECTION_STRING"] = ""
# A local .env may switch demo mode on; the tests choose it per test.
os.environ["FEEDBACK_DEMO_MODE"] = "false"

import pytest
from fastapi.testclient import TestClient

from app import db
from app.main import create_app

AJAX = {"X-Requested-With": "XMLHttpRequest"}


class FakeDb:
    """Stands in for SQL Server: canned result sets per stored procedure."""

    def __init__(self):
        self.responses: dict[str, object] = {}
        self.calls: list[tuple[str, dict]] = []

    def on(self, proc: str, *tables: db.Table, outputs: dict | None = None, handler=None):
        self.responses[proc] = handler or db.ProcResult(list(tables), outputs or {})

    def call_proc(self, name, params=None, *, outputs=None, timeout=None):
        params = dict(params or {})
        self.calls.append((name, params))
        response = self.responses.get(name, db.ProcResult())
        if callable(response):
            response = response(params)
        return db.ProcResult(list(response.tables), dict(response.outputs))

    def call_proc_rowcount(self, name, params=None, *, timeout=None):
        self.calls.append((name, dict(params or {})))
        response = self.responses.get(name, 0)
        return response if isinstance(response, int) else 0

    def calls_to(self, proc: str) -> list[dict]:
        return [params for name, params in self.calls if name == proc]


@pytest.fixture
def fake_db(monkeypatch) -> FakeDb:
    fake = FakeDb()
    monkeypatch.setattr(db, "call_proc", fake.call_proc)
    monkeypatch.setattr(db, "call_proc_rowcount", fake.call_proc_rowcount)
    fake.on("usp_GetFiveDigitEmpID", db.Table(["EMPLID"], [["12345"]]))
    fake.on("usp_getindividual", db.Table(["AdminName", "ReplyMailID"], [["Asha Rao", "asha@example.com"]]))
    return fake


@pytest.fixture
def client(fake_db) -> TestClient:
    return TestClient(create_app(), headers=AJAX)


def _login(client: TestClient, fake_db: FakeDb, admin_rows: list[list]) -> TestClient:
    fake_db.on("usp_IsAdmin", db.Table(["IsSuperAdmin"], admin_rows))
    response = client.post("/api/auth/login", json={"username": "1000012345", "password": "x"})
    assert response.status_code == 200, response.text
    return client


@pytest.fixture
def user_client(client, fake_db) -> TestClient:
    return _login(client, fake_db, [])


@pytest.fixture
def admin_client(client, fake_db) -> TestClient:
    return _login(client, fake_db, [[0]])
