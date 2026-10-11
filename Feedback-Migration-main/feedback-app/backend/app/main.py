from __future__ import annotations

import logging
from logging.handlers import RotatingFileHandler

from fastapi import FastAPI, Request
from fastapi.responses import FileResponse, JSONResponse
from fastapi.staticfiles import StaticFiles
from starlette.middleware.sessions import SessionMiddleware

from .config import get_settings
from .routers import auth, feedback, lookups, masters, nominations, reports, sessions, skillport

log = logging.getLogger(__name__)

SAFE_METHODS = {"GET", "HEAD", "OPTIONS"}

SECURITY_HEADERS = {
    "X-Content-Type-Options": "nosniff",
    "X-Frame-Options": "DENY",
    "Referrer-Policy": "same-origin",
}
# The built front end is a script and a stylesheet served from this origin.
# React sets some styles inline; downloads are handed over as blob: links.
PAGE_CSP = (
    "default-src 'self'; style-src 'self' 'unsafe-inline'; img-src 'self' data:; "
    "frame-ancestors 'none'; base-uri 'self'; form-action 'self'"
)
API_CSP = "default-src 'none'; frame-ancestors 'none'"


def _log_to_file(path: str) -> None:
    root = logging.getLogger()
    if any(isinstance(h, RotatingFileHandler) for h in root.handlers):
        return
    handler = RotatingFileHandler(path, maxBytes=5_000_000, backupCount=10, encoding="utf-8")
    handler.setFormatter(logging.Formatter("%(asctime)s %(levelname)s %(name)s: %(message)s"))
    root.addHandler(handler)
    if root.level > logging.INFO or root.level == logging.NOTSET:
        root.setLevel(logging.INFO)


def create_app() -> FastAPI:
    settings = get_settings()
    production = settings.environment == "production"
    if production and settings.production_problems():
        raise RuntimeError(
            "Refusing to start in production: "
            + "; ".join(settings.production_problems())
            + ". Fix the settings, or set FEEDBACK_ENVIRONMENT=development on a developer machine."
        )
    if settings.log_file:
        _log_to_file(settings.log_file)
    if settings.demo_mode:
        log.warning("FEEDBACK_DEMO_MODE is on: using sample data and demo logins, not the real database")
    # The interactive API pages describe every endpoint; keep them to developer machines.
    app = FastAPI(
        title="Training Feedback",
        docs_url=None if production else "/api/docs",
        redoc_url=None,
        openapi_url=None if production else "/api/openapi.json",
    )

    @app.middleware("http")
    async def require_ajax_header(request: Request, call_next):
        # Browsers do not let another site add custom headers to a cross-site
        # request, so this blocks cross-site form posts using the login cookie.
        if (
            request.url.path.startswith("/api/")
            and request.method not in SAFE_METHODS
            and request.headers.get("x-requested-with") != "XMLHttpRequest"
        ):
            return JSONResponse({"detail": "Missing X-Requested-With header"}, status_code=403)
        response = await call_next(request)
        for header, value in SECURITY_HEADERS.items():
            response.headers.setdefault(header, value)
        if request.url.path.startswith("/api/"):
            # Reports and employee data must not be kept by shared browsers or proxies.
            response.headers.setdefault("Cache-Control", "no-store")
            if not request.url.path.startswith("/api/docs"):
                response.headers.setdefault("Content-Security-Policy", API_CSP)
        else:
            response.headers.setdefault("Content-Security-Policy", PAGE_CSP)
        return response

    app.add_middleware(
        SessionMiddleware,
        secret_key=settings.secret_key,
        session_cookie="feedback_session",
        max_age=settings.session_minutes * 60,
        same_site="lax",
        https_only=settings.cookie_secure,
    )

    @app.exception_handler(Exception)
    async def unhandled(request: Request, exc: Exception):
        log.exception("Unhandled error on %s %s", request.method, request.url.path)
        return JSONResponse(
            {"detail": "An unexpected error occured. Please contact system administrator."},
            status_code=500,
        )

    @app.get("/api/health", include_in_schema=False)
    def health() -> dict:
        """For the proxy or a monitor: answers when the application is up."""
        return {"status": "ok"}

    for module in (auth, lookups, masters, sessions, nominations, feedback, reports, skillport):
        app.include_router(module.router)

    dist = settings.frontend_dist
    if dist.is_dir():
        app.mount("/assets", StaticFiles(directory=dist / "assets"), name="assets")

        @app.get("/{path:path}", include_in_schema=False)
        def spa(path: str):
            if path.startswith("api/"):
                return JSONResponse({"detail": "Not Found"}, status_code=404)
            candidate = (dist / path).resolve()
            if path and candidate.is_file() and dist.resolve() in candidate.parents:
                return FileResponse(candidate)
            return FileResponse(dist / "index.html")

    return app


app = create_app()
