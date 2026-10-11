"""Notification mails.

Mail templates live in the database (Mail Master) and contain placeholders such
as <traineename>. usp_getMailRelatedDetails returns the template (table 0) and
the values for it (table 1); mail is sent by SQL Server through usp_sendmail and
logged with usp_InsertEmpMailstat. This is the same flow as the old
NominationList / FeedbackSup / FeedbackReview pages.
"""

from __future__ import annotations

import re
from dataclasses import dataclass
from datetime import datetime
from html import escape

from . import db

# usp_getMailRelatedDetails @Flag values
FLAG_NOMINATION = 0
FLAG_SIGNATURE = 1
FLAG_SUPERVISOR_LIST = 2
FLAG_SUPERVISOR_SUBMITTED = 3
FLAG_REVIEW = 4

REJECTED_ROW = (
    '<tr> <td bgcolor="White" height="25" style="color: #0033CC; '
    'background-color: #FFFFFF;">Please re-submit.</td></tr>'
)


@dataclass
class Mail:
    sender: str
    to: str
    cc: str
    bcc: str
    subject: str
    body: str


def join_trainer_names(names: list[str]) -> str:
    """'A', 'A and B', 'A,B and C' - upper-cased, as the old pages built it."""
    names = [n.upper() for n in names]
    if len(names) <= 1:
        return "".join(names)
    joined = ""
    remaining = len(names)
    for name in names:
        if remaining == 1:
            joined += name
        elif remaining == 2:
            joined += name + " and "
        else:
            joined += name + ","
        remaining -= 1
    return joined


def strip_trainer_ids(name: str) -> str:
    """'JOHN SMITH(12345)' -> 'JOHN SMITH'."""
    return re.sub(r"[0-9]", "", re.sub(r"[()]", "", name))


def display_trainer_name(joined: str) -> str:
    if joined == "ALL TRAINER(ALLTR)":
        return joined[: joined.index("(")]
    return strip_trainer_ids(joined)


def html_list(items: list[str]) -> str:
    return "<ul>" + "".join(f"<li>{escape(i)}</li>" for i in items) + "</ul>"


def _details(emp_id: str, course_id: str | None, session_id: str | None, flag: int):
    result = db.call_proc(
        "usp_getMailRelatedDetails",
        {
            "course_Id": course_id,
            "session_id": session_id,
            "Employee_id": emp_id,
            "Flag": flag,
        },
    )
    return result.table(0), result.table(1)


def signature_details(admin_id: str, course_id: str, session_id: str) -> tuple[str, str]:
    """(admin name in upper case, reply mail id) for the signed-in admin."""
    _, values = _details(admin_id, course_id, session_id, FLAG_SIGNATURE)
    return values.text(0, "AdminName").upper(), values.text(0, "ReplyMailID")


def build_nomination_mail(
    emp_id: str, course_id: str, session_id: str, signature: str, reply_to: str
) -> Mail:
    template, values = _details(emp_id, course_id, session_id, FLAG_NOMINATION)
    course = values.text(0, "Course Name").upper()
    trainers = [db.to_text(v) for v in values.column("Trainer Name")]
    body = template.text(0, "Body")
    for placeholder, value in (
        ("<traineename>", values.text(0, "Traine Name").upper()),
        ("<coursename>", course),
        ("<startdate>", values.text(0, "StartDate")),
        ("<EndDate>", values.text(0, "EndDate")),
        ("<Singlerorplural>", "trainer" if len(trainers) == 1 else "trainers"),
        ("<trainername>", f"'{display_trainer_name(join_trainer_names(trainers))}'"),
        ("<signature>", signature.upper()),
        ("<replymailid>", reply_to),
    ):
        body = body.replace(placeholder, value)
    return Mail(
        sender=reply_to,
        to=values.text(0, "To Mail ID"),
        cc=template.text(0, "CccMail"),
        bcc=template.text(0, "BccMail"),
        subject=template.text(0, "Subject").replace("<Course Name>", f"'{course}'"),
        body=body,
    )


def build_supervisor_list_mail(
    emp_id: str, course_id: str, session_id: str, signature: str, reply_to: str
) -> Mail:
    template, values = _details(emp_id, course_id, session_id, FLAG_SUPERVISOR_LIST)
    body = template.text(0, "Body")
    for placeholder, value in (
        ("<Supervisor Name>", values.text(0, "Traine Name").upper()),
        ("<Singlerorplural>", "Sub-ordinate" if len(values) == 1 else "Sub-ordinates"),
        ("<signature>", signature.upper()),
        ("<replymailid>", reply_to),
    ):
        body = body.replace(placeholder, value)
    return Mail(
        sender=reply_to,
        to=values.text(0, "To Mail ID"),
        cc=template.text(0, "CccMail"),
        bcc=template.text(0, "BccMail"),
        subject=template.text(0, "Subject"),
        body=body,
    )


def build_review_mail(supervisor_id: str, course_id: str, session_id: str, status: str) -> Mail:
    """Mail to the supervisor after the PM approves or rejects the ratings.

    status is "Approved", "Rejected" or "Auto Approved".
    """
    template, values = _details(supervisor_id, course_id, session_id, FLAG_REVIEW)
    pm_mail = values.text(0, "PM_MAIL")
    pm_name = values.text(0, "PM Name")
    cc = pm_mail
    trainees = ""
    body = template.text(0, "Body")
    for placeholder, value in (
        ("<Reporting Manager Name>", values.text(0, "Supervisor Name")),
        ("<remarks>", values.text(0, "PM_Remarks")),
        ("<signature>", pm_name),
        ("<replymailid>", pm_mail),
    ):
        body = body.replace(placeholder, value)

    subordinate_list = html_list([db.to_text(v) for v in values.column("Subordinate Name")])
    if status == "Rejected":
        body = body.replace("<row>", REJECTED_ROW).replace(
            "<status>",
            " has completed the Rating Review process. Please refer the PM Remarks for further details.",
        )
    elif status == "Auto Approved":
        body = (
            body.replace("<PM Name>", "")
            .replace("<row>", "")
            .replace("<status>", "The rating has been submitted and auto approved for")
        )
        trainees = subordinate_list
        cc = ""
    else:
        body = body.replace("<row>", "").replace(
            "<status>", f"has {status} the rating submitted for"
        )
        trainees = subordinate_list
    body = body.replace("<PM Name>", pm_name).replace("<Trainee Details>", trainees)

    subject_status = "Approved" if status == "Auto Approved" else status
    return Mail(
        sender=pm_mail,
        to=values.text(0, "To Mail ID"),
        cc=cc,
        bcc=template.text(0, "BccMail"),
        subject=template.text(0, "Subject").replace("<status>", subject_status),
        body=body,
    )


def build_supervisor_submitted_mail(
    supervisor_id: str, course_id: str, session_id: str
) -> tuple[Mail, int]:
    """Mail sent when a supervisor submits ratings; returns (mail, flag used).

    When the ratings were auto approved there is no PM step, so the supervisor
    gets the review mail straight away.
    """
    template, values = _details(supervisor_id, course_id, session_id, FLAG_SUPERVISOR_SUBMITTED)
    if values.text(0, "PM_Remarks").strip() == "Auto Approved":
        return (
            build_review_mail(supervisor_id, course_id, session_id, "Auto Approved"),
            FLAG_REVIEW,
        )

    supervisor_mail = values.text(0, "SUPERVISOR_MAIL")
    supervisor_name = values.text(0, "Supervisor Name")
    body = template.text(0, "Body")
    for placeholder, value in (
        ("< PM Name>", values.text(0, "PM Name")),
        ("<Reporting Manager Name>", supervisor_name),
        (
            "<Trainee Details>",
            html_list([db.to_text(v) for v in values.column("Subordinate Name")]),
        ),
        ("<signature>", supervisor_name),
        ("<replymailid>", supervisor_mail),
    ):
        body = body.replace(placeholder, value)
    mail = Mail(
        sender=supervisor_mail,
        to=values.text(0, "To Mail ID"),
        cc=supervisor_mail,
        bcc=template.text(0, "BccMail"),
        subject=template.text(0, "Subject"),
        body=body,
    )
    return mail, FLAG_SUPERVISOR_SUBMITTED


def send(mail: Mail, emp_id: str, course_id: str, session_id: str) -> str:
    """Send through usp_sendmail and record the outcome. Returns the status."""
    sent = db.call_proc(
        "usp_sendmail",
        {
            "from": mail.sender,
            "to": mail.to,
            "subject": mail.subject,
            "body": mail.body,
            "cc": mail.cc,
            "bcc": mail.bcc,
        },
    ).first
    # The procedure reports failure by returning a single non-zero value.
    return_value = int(sent.rows[0][0]) if len(sent) == 1 else 0
    status = "Delivered" if return_value == 0 else "Failed"

    db.call_proc(
        "usp_InsertEmpMailstat",
        {
            "course_Id": course_id,
            "session_id": session_id,
            "Nominatedate": datetime.now(),
            "Employee_id": emp_id,
            "Status": status,
            "FromMail": mail.sender,
            "ToMail": mail.to,
            "BccMail": mail.bcc,
            "CcMail": mail.cc,
            "Subject": mail.subject,
            "Body": mail.body,
        },
    )
    return status
