# XR Motion / Data Explorer

## Project Purpose

Small Unity project for playback and interactive visualization
of time-series spatial data.

## Role of Codex

This project is also being used by the developer to practice writing
C# and solving programming problems independently.

Therefore:

- Do not implement complete features unless explicitly requested.
- Prefer hints, questions, code review and architectural feedback.
- Review developer-written code before proposing replacements.
- Point out bugs and edge cases without automatically rewriting everything.
- Explain trade-offs when several approaches are valid.
- Generate complete implementations only when explicitly requested.

## Architecture Principles

- Keep data parsing separate from visualization.
- Keep the data model independent from Unity GameObjects where practical.
- Keep playback logic separate from UI.
- XR must build on the desktop systems rather than duplicate them.
- Prefer simple solutions over premature abstractions.
- Avoid unnecessary dependencies.

## Unity Rules

- Preserve `.meta` files.
- Do not modify generated Unity folders such as Library, Temp, Logs or Obj.
- Do not add XR dependencies until the desktop MVP works.

## Development Workflow

Repository setup ends once the empty Unity project has been committed
and pushed.

After that, do not propose more repository infrastructure until this works:

CSV → Parser → Data Model → Moving GameObject