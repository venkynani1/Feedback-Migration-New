"""Thin wrapper over pyodbc for calling the TrainingFeedback stored procedures.

The old application kept its business rules in stored procedures and called
them through SqlHelper. This module is the equivalent: every database access
in the application goes through call_proc / call_proc_rowcount.
"""

from __future__ import annotations

import re
from dataclasses import dataclass, field
from datetime import date, datetime, time
from decimal import Decimal
from typing import Any

from .config import get_settings

_IDENTIFIER = re.compile(r"^[A-Za-z_][A-Za-z0-9_]*$")


def _jsonable(value: Any) -> Any:
    if isinstance(value, Decimal):
        return float(value)
    if isinstance(value, (datetime, date, time)):
        return value.isoformat()
    if isinstance(value, (bytes, bytearray)):
        return value.hex()
    return value


@dataclass
class Table:
    """One result set. Column lookups are case-insensitive, like a DataTable."""

    columns: list[str] = field(default_factory=list)
    rows: list[list[Any]] = field(default_factory=list)

    def __len__(self) -> int:
        return len(self.rows)

    def index(self, name: str) -> int:
        wanted = name.lower()
        for i, column in enumerate(self.columns):
            if column.lower() == wanted:
                return i
        raise KeyError(name)

    def has(self, name: str) -> bool:
        return any(c.lower() == name.lower() for c in self.columns)

    def value(self, row: int, name: str) -> Any:
        return self.rows[row][self.index(name)]

    def text(self, row: int, name: str) -> str:
        """Like Convert.ToString(row[name]): NULL becomes an empty string."""
        return to_text(self.value(row, name))

    def column(self, name: str) -> list[Any]:
        i = self.index(name)
        return [row[i] for row in self.rows]

    def records(self) -> list[dict[str, Any]]:
        return [
            {c: _jsonable(v) for c, v in zip(self.columns, row)} for row in self.rows
        ]

    def to_json(self) -> dict[str, Any]:
        return {
            "columns": self.columns,
            "rows": [[_jsonable(v) for v in row] for row in self.rows],
        }


@dataclass
class ProcResult:
    tables: list[Table] = field(default_factory=list)
    outputs: dict[str, Any] = field(default_factory=dict)

    @property
    def first(self) -> Table:
        return self.tables[0] if self.tables else Table()

    def table(self, i: int) -> Table:
        return self.tables[i] if i < len(self.tables) else Table()


def to_text(value: Any) -> str:
    if value is None:
        return ""
    if isinstance(value, Decimal):
        return format(value, "f")
    return str(value)


def _connect():
    import pyodbc

    settings = get_settings()
    if not settings.db_connection_string:
        raise RuntimeError("FEEDBACK_DB_CONNECTION_STRING is not configured")
    return pyodbc.connect(
        settings.db_connection_string,
        autocommit=True,
        timeout=settings.db_login_timeout_seconds,
    )


def _check(name: str) -> str:
    if not _IDENTIFIER.match(name):
        raise ValueError(f"invalid SQL identifier: {name!r}")
    return name


def call_proc(
    name: str,
    params: dict[str, Any] | None = None,
    *,
    outputs: dict[str, str] | None = None,
    timeout: int | None = None,
) -> ProcResult:
    """Execute a stored procedure with named parameters.

    params maps parameter names (without '@') to values. outputs maps OUTPUT
    parameter names to their SQL type, e.g. {"IsExistsAlready": "int"}; their
    values come back in ProcResult.outputs.
    """
    params = params or {}
    outputs = outputs or {}
    if get_settings().demo_mode:
        from . import demo

        return demo.call_proc(_check(name), params)

    args = [f"@{_check(p)} = ?" for p in params]
    args += [f"@{_check(o)} = @out_{o} OUTPUT" for o in outputs]

    sql = "SET NOCOUNT ON; "
    for o, sql_type in outputs.items():
        if not re.match(r"^[A-Za-z]+(\(\d+\))?$", sql_type):
            raise ValueError(f"invalid SQL type: {sql_type!r}")
        sql += f"DECLARE @out_{o} {sql_type}; "
    sql += f"EXEC [dbo].[{_check(name)}] {', '.join(args)}; "
    if outputs:
        sql += "SELECT " + ", ".join(f"@out_{o} AS [{o}]" for o in outputs) + ";"

    connection = _connect()
    try:
        if timeout is not None:
            connection.timeout = timeout
        cursor = connection.cursor()
        cursor.execute(sql, list(params.values()))
        tables: list[Table] = []
        while True:
            if cursor.description:
                tables.append(
                    Table(
                        columns=[d[0] for d in cursor.description],
                        rows=[list(r) for r in cursor.fetchall()],
                    )
                )
            if not cursor.nextset():
                break
    finally:
        connection.close()

    result = ProcResult(tables=tables)
    if outputs:
        out_table = tables.pop()
        result.outputs = dict(zip(out_table.columns, out_table.rows[0]))
    return result


def call_proc_rowcount(
    name: str, params: dict[str, Any] | None = None, *, timeout: int | None = None
) -> int:
    """Execute a procedure and return the rows affected (ExecuteNonQuery)."""
    params = params or {}
    if get_settings().demo_mode:
        from . import demo

        return demo.call_proc_rowcount(_check(name), params)
    args = ", ".join(f"@{_check(p)} = ?" for p in params)
    connection = _connect()
    try:
        if timeout is not None:
            connection.timeout = timeout
        cursor = connection.cursor()
        cursor.execute(f"EXEC [dbo].[{_check(name)}] {args}", list(params.values()))
        # ExecuteNonQuery adds up every statement in the procedure.
        affected = max(cursor.rowcount, 0)
        while cursor.nextset():
            affected += max(cursor.rowcount, 0)
        return affected
    finally:
        connection.close()
