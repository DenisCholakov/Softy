---
description: "Use when making app code, UI, route, or other changes that alter user-visible or runtime behavior and should be verified in a running browser. Requires manual testing in the VS Code embedded browser first, with Google Chrome as the fallback."
name: "Manual Browser Testing"
---

# Manual Browser Testing

- After changes in this repository that alter user-visible or runtime behavior, manually test the changed behavior in a running browser before treating the work as complete.
- Manual browser testing is not required for changes that do not affect behavior, such as comments, formatting, or other non-functional edits.
- Start the app if needed. Use `npm run dev` unless the task clearly requires a different local start command.
- Open the application in the VS Code embedded browser first and verify the affected page, flow, or interaction manually.
- If the embedded browser is unavailable or cannot be used, open the application in Google Chrome and run the same manual verification there.
- Do not claim completion until the manual browser test has been performed or a concrete blocker has been reported.
- Summarize the manual check in the final response, including the browser used, the page or flow tested, the action performed, and the observed result.

# Cleanup

- After the manual browser test is complete, close the browser tab to free up resources.
- If new resources are created during testing (e.g., test data in the database), clean them up to maintain tidy development environment.
