from __future__ import annotations

from fastapi.testclient import TestClient

from app.config import get_settings
from app.main import create_app


def test_built_frontend_is_served_with_spa_fallback(tmp_path, monkeypatch):
    (tmp_path / "assets").mkdir()
    (tmp_path / "assets" / "app.js").write_text("console.log(1)")
    (tmp_path / "index.html").write_text("<html>app</html>")
    monkeypatch.setattr(get_settings(), "frontend_dist", tmp_path)

    client = TestClient(create_app())
    assert client.get("/").text == "<html>app</html>"
    assert client.get("/reports/overall").text == "<html>app</html>"
    assert client.get("/assets/app.js").text == "console.log(1)"
    assert client.get("/api/nope").status_code == 404
    assert client.get("/..%2F..%2Fapp%2Fconfig.py").text == "<html>app</html>"


def test_database_failure_gives_a_generic_error(monkeypatch):
    # No connection string is configured in tests, so the login lookup fails.
    client = TestClient(create_app(), raise_server_exceptions=False, headers={"X-Requested-With": "XMLHttpRequest"})
    response = client.post("/api/auth/login", json={"username": "a", "password": "b"})
    assert response.status_code == 500
    assert "contact the system administrator" in response.json()["detail"]


def test_responses_carry_security_headers(tmp_path, monkeypatch):
    (tmp_path / "assets").mkdir()
    (tmp_path / "index.html").write_text("<html>app</html>")
    monkeypatch.setattr(get_settings(), "frontend_dist", tmp_path)

    client = TestClient(create_app())
    page = client.get("/")
    assert page.headers["X-Content-Type-Options"] == "nosniff"
    assert page.headers["X-Frame-Options"] == "DENY"
    assert "frame-ancestors 'none'" in page.headers["Content-Security-Policy"]
    api = client.get("/api/auth/me")
    assert api.headers["Cache-Control"] == "no-store"
    assert api.headers["Content-Security-Policy"].startswith("default-src 'none'")
