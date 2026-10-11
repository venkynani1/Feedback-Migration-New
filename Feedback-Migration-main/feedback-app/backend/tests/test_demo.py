"""Walks the main flows end to end against the demo database."""

from __future__ import annotations

from io import BytesIO

import pytest
from fastapi.testclient import TestClient
from openpyxl import Workbook

from app import demo
from app.config import get_settings
from app.main import create_app

AJAX = {"X-Requested-With": "XMLHttpRequest"}


@pytest.fixture
def app(monkeypatch):
    monkeypatch.setattr(get_settings(), "demo_mode", True)
    demo.reset()
    yield create_app()
    demo.reset()


def sign_in(app, username: str) -> TestClient:
    client = TestClient(app, headers=AJAX)
    response = client.post("/api/auth/login", json={"username": username, "password": "demo"})
    assert response.status_code == 200, response.text
    return client


def test_config_lists_demo_logins_and_bad_password_fails(app):
    client = TestClient(app, headers=AJAX)
    config = client.get("/api/auth/config").json()
    assert config["demo"] and [l["username"] for l in config["logins"]] == ["admin", "employee", "supervisor", "manager"]
    assert client.post("/api/auth/login", json={"username": "admin", "password": "nope"}).status_code == 401
    assert sign_in(app, "admin").get("/api/auth/me").json()["role"] == "super"
    assert sign_in(app, "employee").get("/api/auth/me").json()["role"] == "user"


def test_employee_records_feedback_for_each_trainer(app):
    client = sign_in(app, "employee")
    training = client.get("/api/feedback/my").json()["trainings"][0]
    assert (training["Tot_Feedbacks"], training["Bal_Feedbacks"]) == (2, 2)
    key = {"session_id": training["sessionid"], "course_id": training["Courseid"]}
    context = client.get("/api/feedback/context", params=key).json()
    assert context["firstFeedback"] and len(context["trainers"]) == 2

    body = {"sessionId": training["sessionid"], "courseId": training["Courseid"], "comments": "Useful course"}
    first = client.post("/api/feedback", json={**body, "trainerId": context["trainers"][0],
                                               "administration": 5, "design": 4, "delivery": 5})
    assert first.json()["remainingTrainers"] == [context["trainers"][1]]
    assert not client.get("/api/feedback/context", params=key).json()["firstFeedback"]
    last = client.post("/api/feedback", json={**body, "trainerId": context["trainers"][1], "delivery": 3})
    assert last.json()["remainingTrainers"] == []
    remaining = [t["sessionid"] for t in client.get("/api/feedback/my").json()["trainings"]]
    assert training["sessionid"] not in remaining


def test_supervisor_submits_and_manager_approves(app):
    supervisor = sign_in(app, "supervisor")
    pending = supervisor.get("/api/feedback/my").json()["supervisor"][0]
    key = {"session_id": pending["sessionid"], "course_id": pending["Courseid"]}
    form = supervisor.get("/api/feedback/supervisor", params=key).json()
    assert [r["name"] for r in form["rows"]] == ["Ravi Kumar", "Neha Singh"]

    rows = [{**r, "workArea": "Testing", "techSkills": "4", "softSkills": "5", "performance": "4",
             "overallRating": "4", "overallFeedback": "Doing well"} for r in form["rows"]]
    done = supervisor.post("/api/feedback/supervisor", json={
        "sessionId": pending["sessionid"], "courseId": pending["Courseid"], "submit": True, "rows": rows})
    assert done.json() == {"message": "Feedback Submitted SuccessFully", "warning": None}
    assert supervisor.get("/api/feedback/my").json()["supervisor"] == []

    manager = sign_in(app, "manager")
    reviews = manager.get("/api/feedback/my").json()["reviews"]
    assert sorted(r["Supervisors"] for r in reviews) == ["Meera Nair-30001", "Rahul Verma-30002"]
    review = next(r for r in reviews if r["Supervisors"].startswith("Meera"))
    shown = manager.get("/api/feedback/review", params={
        "session_id": review["Session_id"], "supervisor": review["Supervisors"]}).json()
    assert len(shown["rows"]) == 2
    approved = manager.post("/api/feedback/review", json={
        "sessionId": review["Session_id"], "courseId": review["Course_id"],
        "supervisor": review["Supervisors"], "remarks": "Agreed", "approve": True})
    assert approved.json()["warning"] is None
    assert len(manager.get("/api/feedback/my").json()["reviews"]) == 1


def test_admin_screens_have_data(app):
    admin = sign_in(app, "admin")
    year = admin.get("/api/lookups/years").json()[0]
    courses = {c["name"]: c["id"] for c in admin.get("/api/lookups/courses").json()}
    java_sessions = admin.get("/api/lookups/sessions", params={"course_id": courses["Core Java"], "year": year}).json()
    session_id = java_sessions[0]
    by_session = {"session_id": session_id}

    assert admin.get("/api/reports/feedback-balance", params=by_session).json()["rows"][0][1:] == [4, 2, 8, 3]
    assert len(admin.get("/api/reports/comments", params=by_session).json()) == 5
    defaulters = admin.get("/api/reports/defaulters", params=by_session).json()["rows"]
    assert [r[1] for r in defaulters] == ["Ravi Kumar", "Priya Menon"]

    overall = admin.get("/api/reports/overall", params={**by_session, "trainer_id": "OverAll"}).json()
    assert overall["ratingError"] is None and len(overall["rating"]["rows"]) == 3
    pdf = admin.get("/api/reports/overall/pdf", params={**by_session, "trainer_id": "OverAll", "course_name": "Core Java"})
    assert pdf.content.startswith(b"%PDF")

    trainer = admin.get("/api/lookups/session-trainers", params=by_session).json()[0]
    individual = admin.get("/api/reports/individual", params={**by_session, "trainer_id": trainer, "employee_id": "All"}).json()
    assert "Q23" in individual["columns"] and len(individual["rows"]) == 4

    fresher = admin.get("/api/lookups/sessions", params={"course_id": "FTP_001", "year": admin.get("/api/lookups/years").json()[-1]}).json()
    assert admin.get("/api/reports/effectiveness", params={"session_id": fresher[0]}).json()["rows"]
    assert admin.get("/api/mail-master").json()["subject"]
    assert admin.get("/api/skillport/learning-report").json()["rows"]


def test_admin_creates_session_and_nominates(app):
    admin = sign_in(app, "admin")
    trainers = admin.get("/api/lookups/course-trainers", params={"course_id": "SQL_201", "trainer_type": "I"}).json()
    created = admin.post("/api/sessions", json={
        "courseId": "SQL_201", "courseName": "SQL Server Fundamentals", "location": "Mumbai",
        "startDate": "2024-01-08", "endDate": "2024-01-09", "trainers": trainers}).json()
    result = admin.post("/api/nominations", json={
        "listType": 0, "courseId": "SQL_201", "sessionId": created["sessionId"], "employeeIds": ["20003", "20004"]}).json()
    assert result["mailErrors"] == [] and len(result["mailStatus"]["rows"]) == 2
    assert {r[2] for r in result["mailStatus"]["rows"]} == {"Delivered"}


def test_excel_upload_view_and_delete(app):
    admin = sign_in(app, "admin")
    workbook = Workbook()
    workbook.active.append(["EmployeeID", "Certification"])
    workbook.active.append(["20001", "OCA"])
    buffer = BytesIO()
    workbook.save(buffer)

    form = {"upload_type": "Certifications", "month": "1", "year": "2024"}
    saved = admin.post("/api/skillport/upload", data=form, files={"file": ("certs.xlsx", buffer.getvalue())})
    assert saved.json()["message"] == "Totally 1 Record(s) saved successfully"
    records = admin.get("/api/skillport/records", params=form).json()
    assert records["rows"] == [["20001", "OCA", "1", "2024"]]
    again = admin.post("/api/skillport/upload", data=form, files={"file": ("certs.xlsx", buffer.getvalue())})
    assert again.json()["saved"] == 0
    deleted = admin.post("/api/skillport/delete", json={"uploadType": "Certifications", "month": 1, "year": 2024})
    assert deleted.json()["deleted"] == 1


def test_course_is_refused_when_only_the_id_or_only_the_name_is_taken(app):
    admin = sign_in(app, "admin")
    course = {"description": "d", "internalExternal": "I", "courseType": "Technical"}
    same_id = admin.post("/api/courses", json={**course, "courseId": "JAVA_101", "courseName": "Something New"})
    assert (same_id.status_code, same_id.json()["detail"]) == (409, "Course ID already exist")
    same_name = admin.post("/api/courses", json={**course, "courseId": "NEW_1", "courseName": "Core Java"})
    assert (same_name.status_code, same_name.json()["detail"]) == (409, "Course Name already exist")
    both = admin.post("/api/courses", json={**course, "courseId": "JAVA_101", "courseName": "Core Java"})
    assert both.json()["detail"] == "Course Already exist"
    created = admin.post("/api/courses", json={**course, "courseId": "NEW_1", "courseName": "Something New"})
    assert created.json() == {"message": "Course successfully created"}
