# Project Instructions

## Project Overview

- This is a Unity game development project.
- Unity version: 2021.3.33f1.
- Primary language: C#.
- Treat the existing project architecture and code as the source of truth.
- Prefer extending existing systems over creating duplicate systems.

## General Development Rules

- Inspect the existing implementation before making changes.
- Make the smallest practical change required to satisfy the request.
- Do not modify unrelated files, scenes, assets, or settings.
- Do not delete or rename existing files, GameObjects, components, or assets unless explicitly requested.
- Preserve existing public APIs and behavior unless the task requires changing them.
- Avoid introducing new packages or dependencies unless explicitly requested.
- Follow the existing naming and coding conventions in the project.

## C# / Unity

- Write production-quality C# suitable for Unity 2021.3.33f1.
- Prefer clear, readable, maintainable code over overly clever implementations.
- Reuse existing utilities, managers, systems, and data structures when appropriate.
- Avoid unnecessary allocations in frequently executed gameplay code.
- Do not introduce LINQ into performance-critical gameplay code unless there is a clear reason.
- Keep methods focused and avoid unnecessary refactoring.
- Do not change Unity serialization-sensitive fields without considering existing scene and prefab data.

## Unity Editor

- Before modifying a scene, inspect the current scene state.
- Before modifying a GameObject, verify that the target GameObject exists.
- Do not modify unrelated GameObjects or components.
- After making Unity Editor changes, verify that the requested change was actually applied.
- Do not make changes to ProjectSettings unless explicitly requested.

## Unity MCP

- Use Unity MCP for Unity Editor operations whenever it is available.
- Prefer reading and inspecting the Unity project through MCP before making Editor changes.
- For MCP write operations:
  1. Identify the exact target.
  2. Make only the requested change.
  3. Verify the result.
- Do not perform destructive Unity MCP operations unless explicitly requested.
- If an MCP operation fails, inspect the error and attempt a targeted fix rather than making unrelated changes.

## Code Changes

Before modifying code:

1. Find the relevant existing implementation.
2. Understand how the current system works.
3. Identify the minimum required change.
4. Make the change.
5. Check for compile errors or obvious integration problems.

Do not rewrite working systems simply to make the code look different.

## Testing and Verification

After making a meaningful code change:

- Check for compilation errors.
- Verify affected references are still valid.
- Verify Unity-side behavior when possible.
- Report what was changed and what was verified.

If verification cannot be performed, clearly state what could not be verified.

## Git

- Keep changes focused and logically grouped.
- Do not create commits unless explicitly requested.
- Do not push changes to GitHub unless explicitly requested.
- Before committing or pushing, summarize the files and changes involved.
- Never expose secrets, API keys, access tokens, or credentials.

## GitHub MCP

- Use GitHub MCP for GitHub operations when available.
- Before modifying a repository, inspect the relevant repository/files first.
- Do not create, delete, rename, or overwrite repository files unless explicitly requested.
- Do not create pull requests, issues, releases, or commits unless explicitly requested.
- Never expose GitHub tokens or other credentials in responses.

## Communication

- Be concise but provide enough technical detail to understand the change.
- When a task is ambiguous, inspect the project first and ask only when the ambiguity materially affects the implementation.
- Before making potentially destructive or broad changes, explain what will be affected.
- After completing a task, provide a short summary of:
  - What changed
  - What was verified
  - Any remaining issues