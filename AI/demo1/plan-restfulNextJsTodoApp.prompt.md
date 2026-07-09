## Plan: RESTful Next.js Todo App

Build the same local-only Next.js todo app, but replace the earlier Server Actions approach with REST-style Route Handlers backed by Prisma and SQLite. The recommended implementation is App Router for pages plus API resource routes for tasks, using standard HTTP verbs and resource-oriented URLs so the CRUD layer stays as RESTful as practical without adding unnecessary enterprise complexity.

**Steps**

1. Phase 1 - Scaffold the app: initialize a Next.js project in c:\Users\denis\Documents\Projects\Softy\AI\demo1 with JavaScript, Tailwind, ESLint, and App Router. Use npm unless you later request a different package manager.
2. Phase 1 - Add data tooling: install Prisma and @prisma/client, initialize Prisma for SQLite, and configure a local database file plus environment variables for local development.
3. Phase 1 - Define the initial data model: create a Task model with only the startup fields needed for CRUD and list rendering. Recommended fields: id, title, notes optional, completed boolean default false, createdAt, updatedAt.
4. Phase 1 - Run the first migration and generate the Prisma client. This blocks all API work.
5. Phase 2 - Create the shared server layer: add a Prisma client singleton and small task service helpers for list, create, update, and delete so the route handlers stay thin.
6. Phase 2 - Implement REST-style endpoints: create `GET /api/tasks` and `POST /api/tasks` in `app/api/tasks/route.js`, then `GET /api/tasks/:id`, `PATCH /api/tasks/:id`, and `DELETE /api/tasks/:id` in `app/api/tasks/[id]/route.js`. Use JSON request and response bodies, proper status codes, and minimal validation.
7. Phase 2 - Decide response shape once and keep it consistent. Recommended: return the task resource for successful create, read, and update; return `204 No Content` for successful delete; return `400` for invalid payloads and `404` when a task id does not exist.
8. Phase 2 - Build the UI against those endpoints instead of Server Actions. Use a small client-side task shell for create, edit, and delete interactions so each mutation goes through `fetch` to the API routes. The page can either fetch initial tasks from the API for consistency or read directly from Prisma on first render and use the API only for mutations; the more RESTful option is to use the API for reads as well.
9. Phase 2 - Implement editing with inline controls and `PATCH` semantics, updating only changed fields rather than resubmitting a full replacement payload.
10. Phase 2 - Add delete handling through `DELETE /api/tasks/:id` and refresh local UI state after mutation.
11. Phase 3 - Add empty, loading, and error states around the fetch-based workflow so the interface stays clear even when a request fails.
12. Phase 3 - Verify the full slice end to end: migration, app startup, linting, and manual API plus browser CRUD checks.

**Relevant files**

- c:\Users\denis\Documents\Projects\Softy\AI\demo1\package.json — project scripts and dependencies.
- c:\Users\denis\Documents\Projects\Softy\AI\demo1\prisma\schema.prisma — SQLite datasource and Task model.
- c:\Users\denis\Documents\Projects\Softy\AI\demo1\.env — local DATABASE_URL.
- c:\Users\denis\Documents\Projects\Softy\AI\demo1\lib\prisma.js — shared Prisma client singleton.
- c:\Users\denis\Documents\Projects\Softy\AI\demo1\lib\tasks.js — optional task service helpers reused by route handlers.
- c:\Users\denis\Documents\Projects\Softy\AI\demo1\app\api\tasks\route.js — collection resource for list and create.
- c:\Users\denis\Documents\Projects\Softy\AI\demo1\app\api\tasks\[id]\route.js — item resource for read, partial update, and delete.
- c:\Users\denis\Documents\Projects\Softy\AI\demo1\app\page.js — page composition and initial data loading strategy.
- c:\Users\denis\Documents\Projects\Softy\AI\demo1\components\TaskApp.jsx — client-side task interactions over `fetch`, if the UI logic is extracted.
- c:\Users\denis\Documents\Projects\Softy\AI\demo1\app\globals.css — visual cleanup for the distraction-free layout.

**Verification**

1. Run the scaffold and confirm the app starts locally with `npm run dev`.
2. Run Prisma migration and verify the SQLite database file is created.
3. Hit the API routes manually or with browser devtools: confirm `GET /api/tasks`, `POST /api/tasks`, `PATCH /api/tasks/:id`, and `DELETE /api/tasks/:id` behave as expected and return appropriate status codes.
4. Run `npm run lint` and resolve any issues.
5. Manual browser check: create, edit, and delete tasks from the UI, then refresh to confirm SQLite persistence.
6. Manual UX check: verify empty, loading, and error states remain understandable on desktop and mobile widths.

**Decisions**

- Included scope remains local-only, no authentication, no deployment, no multi-user behavior.
- CRUD should be modeled as a task resource, not action-specific endpoints such as `/createTask` or `/deleteTask`.
- Recommended API shape: `/api/tasks` for the collection and `/api/tasks/:id` for individual task resources.
- Recommended HTTP semantics: `GET` list or single task, `POST` create, `PATCH` partial update, `DELETE` remove.
- Excluded from v1: projects, categories, priorities, due dates, filters, drag-and-drop ordering, search, recurring tasks, and analytics.

**Further Considerations**

1. Initial read strategy: Option A fetch initial tasks through the API for stricter consistency with the REST approach. Option B render initial tasks directly from Prisma and reserve the API for mutations, which is slightly simpler but less uniform.
2. Validation strategy: keep validation inline in route handlers for startup speed, or extract a tiny validator module if you want cleaner handler code from day one.
3. Response envelope strategy: return raw task objects for simplicity, or wrap responses in `{ data, error }` for stricter client consistency. Raw resources are more minimal for this phase.
