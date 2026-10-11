"""Client for the StationH SOAP authentication service (IsValidUser)."""

from __future__ import annotations

import logging
import xml.etree.ElementTree as ET
from xml.sax.saxutils import escape

import httpx

from .config import get_settings

log = logging.getLogger(__name__)

_NS = "http://tempuri.org/"
_ENVELOPE = (
    '<?xml version="1.0" encoding="utf-8"?>'
    '<soap:Envelope xmlns:soap="http://schemas.xmlsoap.org/soap/envelope/">'
    "<soap:Body>"
    f'<IsValidUser xmlns="{_NS}">'
    "<userid>{userid}</userid><password>{password}</password>"
    "</IsValidUser>"
    "</soap:Body>"
    "</soap:Envelope>"
)


class AuthServiceError(Exception):
    """The authentication service could not be reached or gave a bad reply."""


def build_request(userid: str, password: str) -> str:
    return _ENVELOPE.format(userid=escape(userid), password=escape(password))


def parse_response(xml_text: str) -> int:
    try:
        root = ET.fromstring(xml_text)
    except ET.ParseError as exc:
        raise AuthServiceError("authentication service returned invalid XML") from exc
    node = root.find(f".//{{{_NS}}}IsValidUserResult")
    if node is None or node.text is None:
        raise AuthServiceError("authentication service reply had no result")
    try:
        return int(node.text.strip())
    except ValueError as exc:
        raise AuthServiceError("authentication service result was not a number") from exc


def is_valid_user(userid: str, password: str) -> bool:
    """True when StationH accepts the credentials (IsValidUser returns 1)."""
    settings = get_settings()
    if settings.demo_mode:
        from . import demo

        return demo.check_login(userid, password)
    if settings.auth_mode == "dev":
        log.warning("FEEDBACK_AUTH_MODE=dev: accepting login for %s without a password check", userid)
        return bool(password)

    try:
        response = httpx.post(
            settings.soap_auth_url,
            content=build_request(userid, password).encode("utf-8"),
            headers={
                "Content-Type": "text/xml; charset=utf-8",
                "SOAPAction": f'"{_NS}IsValidUser"',
            },
            timeout=settings.soap_timeout_seconds,
        )
        response.raise_for_status()
    except httpx.HTTPError as exc:
        raise AuthServiceError(f"authentication service request failed: {exc}") from exc
    return parse_response(response.text) == 1
