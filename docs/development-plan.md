# Development Plan

## Development Principle

Build the smallest complete version before expanding the project.

## Setup Gate

Repository setup is complete when:

- repository infrastructure exists;
- the empty Unity project opens correctly;
- Unity version-control settings are configured;
- the initial commit has been pushed to GitHub.

After this point, do not add more repository infrastructure until
the first vertical slice works.

## First Vertical Slice

CSV → Parser → Data Model → Moving GameObject

Success means:

1. A CSV can be loaded.
2. Rows are parsed into C# objects.
3. Parsed data can be inspected correctly.
4. A GameObject moves using the dataset.

No XR work before this works.
