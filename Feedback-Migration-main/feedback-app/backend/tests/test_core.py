from __future__ import annotations

from datetime import date, datetime
from decimal import Decimal

import pytest

from app import db, mailer, soap_auth
from app.routers import feedback, sessions, skillport
from app.routers.masters import valid_cc_list


class FakeCursor:
    def __init__(self, result_sets):
        self.result_sets = list(result_sets)
        self.executed = None
        self.rowcount = 7

    def execute(self, sql, params):
        self.executed = (sql, params)

    @property
    def description(self):
        return [(c,) for c in self.result_sets[0][0]] if self.result_sets else None

    def fetchall(self):
        return self.result_sets[0][1]

    def nextset(self):
        del self.result_sets[:1]
        return bool(self.result_sets)


class FakeConnection:
    def __init__(self, cursor):
        self._cursor = cursor
        self.closed = False

    def cursor(self):
        return self._cursor

    def close(self):
        self.closed = True


def test_call_proc_builds_named_exec_and_reads_outputs(monkeypatch):
    cursor = FakeCursor([(["a"], [(1,)]), (["IsExistsAlready"], [(1,)])])
    connection = FakeConnection(cursor)
    monkeypatch.setattr(db, "_connect", lambda: connection)

    result = db.call_proc(
        "usp_InsertSession", {"courseid": "C1", "location": "Pune"}, outputs={"IsExistsAlready": "int"}
    )

    sql, params = cursor.executed
    assert "EXEC [dbo].[usp_InsertSession] @courseid = ?, @location = ?, @IsExistsAlready = @out_IsExistsAlready OUTPUT" in sql
    assert params == ["C1", "Pune"]
    assert result.outputs == {"IsExistsAlready": 1}
    assert [t.columns for t in result.tables] == [["a"]]
    assert connection.closed


def test_call_proc_rejects_unsafe_names():
    with pytest.raises(ValueError):
        db.call_proc("usp_x; DROP TABLE y")
    with pytest.raises(ValueError):
        db.call_proc("usp_x", {"a b": 1})


def test_call_proc_rowcount(monkeypatch):
    cursor = FakeCursor([])
    monkeypatch.setattr(db, "_connect", lambda: FakeConnection(cursor))
    assert db.call_proc_rowcount("DeleteDuplicateRecords", {"MONTH": "3", "YEAR": "2024"}) == 7
    assert cursor.executed == ("EXEC [dbo].[DeleteDuplicateRecords] @MONTH = ?, @YEAR = ?", ["3", "2024"])


def test_table_is_case_insensitive_and_json_safe():
    table = db.Table(["Employee_id", "Score", "When"], [["1", Decimal("4.5"), date(2024, 1, 2)]])
    assert table.text(0, "employee_ID") == "1"
    assert table.to_json()["rows"] == [["1", 4.5, "2024-01-02"]]
    assert db.to_text(None) == ""


def test_soap_request_escapes_and_response_parses():
    xml = soap_auth.build_request("a<b", "p&w")
    assert "<userid>a&lt;b</userid><password>p&amp;w</password>" in xml
    reply = (
        '<soap:Envelope xmlns:soap="http://schemas.xmlsoap.org/soap/envelope/"><soap:Body>'
        '<IsValidUserResponse xmlns="http://tempuri.org/"><IsValidUserResult>1</IsValidUserResult>'
        "</IsValidUserResponse></soap:Body></soap:Envelope>"
    )
    assert soap_auth.parse_response(reply) == 1
    with pytest.raises(soap_auth.AuthServiceError):
        soap_auth.parse_response("<html>error</html>")


def test_trainer_name_formatting():
    assert mailer.join_trainer_names(["a(1)"]) == "A(1)"
    assert mailer.join_trainer_names(["a", "b"]) == "A and B"
    assert mailer.join_trainer_names(["a", "b", "c"]) == "A,B and C"
    assert mailer.strip_trainer_ids("JOHN SMITH(12345)") == "JOHN SMITH"
    assert mailer.display_trainer_name("ALL TRAINER(ALLTR)") == "ALL TRAINER"


def test_session_id_matches_existing_format():
    assert (
        sessions.build_session_id("Chennai", "FTP_001", date(2015, 5, 30), date(2015, 6, 1))
        == "Che_FTP_001_5/30/2015To6/1/2015"
    )


def test_split_supervisor_keeps_hyphenated_names():
    assert feedback.split_supervisor("Anne-Marie Roy-54321") == ("Anne-Marie Roy", "54321")


def test_stored_file_name_is_safe():
    name = skillport.stored_file_name("12345", r"..\..\evil name.xlsx", datetime(2024, 1, 2, 3, 4, 5))
    assert name == "TrainingFeedback_12345_20240102030405000000_evil_name.xls"


def test_cc_list_validation():
    assert valid_cc_list("")
    assert valid_cc_list("a@x.com;b.c@y.co.in;")
    assert not valid_cc_list("not-an-address")


def _mail_details(template_row, value_columns, value_rows):
    template = db.Table(["Subject", "Body", "CccMail", "BccMail"], [template_row])
    return db.ProcResult([template, db.Table(value_columns, value_rows)])


def test_nomination_mail(fake_db):
    fake_db.on(
        "usp_getMailRelatedDetails",
        handler=lambda p: _mail_details(
            ["Nominated for <Course Name>", "Dear <traineename>, <coursename> by <Singlerorplural> <trainername> "
             "from <startdate> to <EndDate>. <signature> <replymailid>", "cc@x.com", "bcc@x.com"],
            ["Course Name", "To Mail ID", "Traine Name", "StartDate", "EndDate", "Trainer Name"],
            [["Java", "t@x.com", "Ravi", "01 Jan 2024", "02 Jan 2024", "Anil(111)"],
             ["Java", "t@x.com", "Ravi", "01 Jan 2024", "02 Jan 2024", "Bala(222)"]],
        ),
    )
    mail = mailer.build_nomination_mail("54321", "C1", "S1", "Asha Rao", "asha@x.com")
    assert mail.subject == "Nominated for 'JAVA'"
    assert mail.body == (
        "Dear RAVI, JAVA by trainers 'ANIL and BALA' from 01 Jan 2024 to 02 Jan 2024. ASHA RAO asha@x.com"
    )
    assert (mail.sender, mail.to, mail.cc, mail.bcc) == ("asha@x.com", "t@x.com", "cc@x.com", "bcc@x.com")
    assert fake_db.calls_to("usp_getMailRelatedDetails")[0]["Flag"] == mailer.FLAG_NOMINATION


@pytest.mark.parametrize(
    "status, subject, cc, body",
    [
        (
            "Approved",
            "Rating Approved",
            "pm@x.com",
            "Sup: PM has Approved the rating submitted for <ul><li>T1</li></ul> ",
        ),
        (
            "Rejected",
            "Rating Rejected",
            "pm@x.com",
            "Sup: PM  has completed the Rating Review process. Please refer the PM Remarks "
            "for further details.  " + mailer.REJECTED_ROW,
        ),
        (
            "Auto Approved",
            "Rating Approved",
            "",
            "Sup: The rating has been submitted and auto approved for <ul><li>T1</li></ul> ",
        ),
    ],
)
def test_review_mail_variants(fake_db, status, subject, cc, body):
    fake_db.on(
        "usp_getMailRelatedDetails",
        handler=lambda p: _mail_details(
            ["Rating <status>", "<Reporting Manager Name>: <PM Name><status> <Trainee Details> <row>", "", "b@x.com"],
            ["PM_MAIL", "PM Name", "To Mail ID", "Supervisor Name", "PM_Remarks", "Subordinate Name"],
            [["pm@x.com", "PM ", "sup@x.com", "Sup", "ok", "T1"]],
        ),
    )
    mail = mailer.build_review_mail("54321", "C1", "S1", status)
    assert mail.subject == subject
    assert mail.cc == cc
    assert mail.body == body


def test_send_records_delivery_status(fake_db):
    mail = mailer.Mail("f@x.com", "t@x.com", "", "", "s", "b")
    assert mailer.send(mail, "54321", "C1", "S1") == "Delivered"
    fake_db.on("usp_sendmail", db.Table(["r"], [[5]]))
    assert mailer.send(mail, "54321", "C1", "S1") == "Failed"
    assert [c["Status"] for c in fake_db.calls_to("usp_InsertEmpMailstat")] == ["Delivered", "Failed"]
