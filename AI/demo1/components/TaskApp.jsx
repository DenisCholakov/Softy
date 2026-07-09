"use client";

import { useEffect, useState, useTransition } from "react";

const emptyForm = {
  title: "",
  notes: "",
  projectId: "",
};

async function requestJson(url, options = {}) {
  const response = await fetch(url, {
    cache: "no-store",
    headers: {
      "Content-Type": "application/json",
      ...(options.headers || {}),
    },
    ...options,
  });

  if (response.status === 204) {
    return null;
  }

  const data = await response.json();

  if (!response.ok) {
    throw new Error(data.error || "Request failed.");
  }

  return data;
}

function formatDate(value) {
  return new Intl.DateTimeFormat("en", {
    dateStyle: "medium",
    timeStyle: "short",
  }).format(new Date(value));
}

export default function TaskApp() {
  const [tasks, setTasks] = useState([]);
  const [projects, setProjects] = useState([]);
  const [form, setForm] = useState(emptyForm);
  const [editingTaskId, setEditingTaskId] = useState(null);
  const [editingForm, setEditingForm] = useState(emptyForm);
  const [error, setError] = useState("");
  const [isLoading, setIsLoading] = useState(true);
  const [isPending, startTransition] = useTransition();

  useEffect(() => {
    let isActive = true;

    async function loadData() {
      try {
        const [taskData, projectData] = await Promise.all([
          requestJson("/api/tasks"),
          requestJson("/api/projects"),
        ]);

        if (!isActive) {
          return;
        }

        setError("");
        setTasks(taskData);
        setProjects(projectData);
      } catch (requestError) {
        if (isActive) {
          setError(requestError.message);
        }
      } finally {
        if (isActive) {
          setIsLoading(false);
        }
      }
    }

    loadData();

    return () => {
      isActive = false;
    };
  }, []);

  const handleCreate = async (event) => {
    event.preventDefault();

    startTransition(async () => {
      try {
        setError("");
        const createdTask = await requestJson("/api/tasks", {
          method: "POST",
          body: JSON.stringify(form),
        });

        setTasks((currentTasks) => [createdTask, ...currentTasks]);
        setForm(emptyForm);
      } catch (requestError) {
        setError(requestError.message);
      }
    });
  };

  const handleDelete = (taskId) => {
    startTransition(async () => {
      try {
        setError("");
        await requestJson(`/api/tasks/${taskId}`, {
          method: "DELETE",
        });

        setTasks((currentTasks) =>
          currentTasks.filter((task) => task.id !== taskId),
        );

        if (editingTaskId === taskId) {
          setEditingTaskId(null);
          setEditingForm(emptyForm);
        }
      } catch (requestError) {
        setError(requestError.message);
      }
    });
  };

  const handleToggleComplete = (task) => {
    startTransition(async () => {
      try {
        setError("");
        const updatedTask = await requestJson(`/api/tasks/${task.id}`, {
          method: "PATCH",
          body: JSON.stringify({ completed: !task.completed }),
        });

        setTasks((currentTasks) =>
          currentTasks.map((currentTask) =>
            currentTask.id === task.id ? updatedTask : currentTask,
          ),
        );
      } catch (requestError) {
        setError(requestError.message);
      }
    });
  };

  const startEditing = (task) => {
    setEditingTaskId(task.id);
    setEditingForm({
      title: task.title,
      notes: task.notes || "",
      projectId: task.project?.id ? String(task.project.id) : "",
    });
  };

  const handleSave = (taskId) => {
    startTransition(async () => {
      try {
        setError("");
        const updatedTask = await requestJson(`/api/tasks/${taskId}`, {
          method: "PATCH",
          body: JSON.stringify(editingForm),
        });

        setTasks((currentTasks) =>
          currentTasks.map((task) => (task.id === taskId ? updatedTask : task)),
        );
        setEditingTaskId(null);
        setEditingForm(emptyForm);
      } catch (requestError) {
        setError(requestError.message);
      }
    });
  };

  return (
    <div className="grid gap-6 lg:grid-cols-[minmax(0,22rem)_minmax(0,1fr)]">
      <section className="panel p-6 sm:p-7">
        <p className="eyebrow">Capture</p>
        <h2 className="mt-3 text-2xl font-semibold text-slate-950">
          Add one task at a time.
        </h2>
        <p className="mt-2 text-sm leading-6 text-slate-600">
          Keep the input lightweight so the list stays focused on what matters
          now.
        </p>

        <form className="mt-6 grid gap-4" onSubmit={handleCreate}>
          <label className="grid gap-2">
            <span className="text-sm font-medium text-slate-700">Title</span>
            <input
              className="input"
              value={form.title}
              onChange={(event) =>
                setForm((currentForm) => ({
                  ...currentForm,
                  title: event.target.value,
                }))
              }
              placeholder="Write the next task"
              maxLength={120}
            />
          </label>

          <label className="grid gap-2">
            <span className="text-sm font-medium text-slate-700">Project</span>
            <select
              className="input"
              value={form.projectId}
              onChange={(event) =>
                setForm((currentForm) => ({
                  ...currentForm,
                  projectId: event.target.value,
                }))
              }
            >
              <option value="">No project</option>
              {projects.map((project) => (
                <option key={project.id} value={String(project.id)}>
                  {project.name}
                </option>
              ))}
            </select>
          </label>

          <label className="grid gap-2">
            <span className="text-sm font-medium text-slate-700">Notes</span>
            <textarea
              className="input min-h-28 resize-y"
              value={form.notes}
              onChange={(event) =>
                setForm((currentForm) => ({
                  ...currentForm,
                  notes: event.target.value,
                }))
              }
              placeholder="Optional details or next step"
              maxLength={400}
            />
          </label>

          <button className="button-primary" disabled={isPending} type="submit">
            {isPending ? "Saving..." : "Create task"}
          </button>
        </form>
      </section>

      <section className="panel p-6 sm:p-7">
        <div className="flex flex-col gap-2 border-b border-slate-200 pb-5 sm:flex-row sm:items-end sm:justify-between">
          <div>
            <p className="eyebrow">Focus list</p>
            <h2 className="mt-2 text-2xl font-semibold text-slate-950">
              Tasks
            </h2>
          </div>
          <p className="text-sm text-slate-500">
            {tasks.length} {tasks.length === 1 ? "task" : "tasks"}
          </p>
        </div>

        {error ? (
          <p className="mt-4 rounded-2xl border border-rose-200 bg-rose-50 px-4 py-3 text-sm text-rose-700">
            {error}
          </p>
        ) : null}

        {isLoading ? (
          <div className="mt-6 rounded-3xl border border-dashed border-slate-300 bg-slate-50 px-6 py-10 text-center text-sm text-slate-500">
            Loading tasks...
          </div>
        ) : null}

        {!isLoading && !tasks.length ? (
          <div className="mt-6 rounded-3xl border border-dashed border-slate-300 bg-slate-50 px-6 py-10 text-center">
            <p className="text-base font-medium text-slate-700">
              No tasks yet.
            </p>
            <p className="mt-2 text-sm leading-6 text-slate-500">
              Start with one clear next action and build from there.
            </p>
          </div>
        ) : null}

        <ul className="mt-6 grid gap-4">
          {tasks.map((task) => {
            const isEditing = editingTaskId === task.id;

            return (
              <li
                key={task.id}
                className={`rounded-3xl border p-5 transition ${
                  task.completed
                    ? "border-emerald-200 bg-emerald-50/80"
                    : "border-slate-200 bg-white"
                }`}
              >
                <div className="flex flex-col gap-4 sm:flex-row sm:items-start sm:justify-between">
                  <div className="flex flex-1 gap-3">
                    <button
                      type="button"
                      className={`mt-1 h-5 w-5 rounded-full border transition ${
                        task.completed
                          ? "border-emerald-600 bg-emerald-600"
                          : "border-slate-300 bg-white"
                      }`}
                      onClick={() => handleToggleComplete(task)}
                      aria-label={
                        task.completed
                          ? "Mark as incomplete"
                          : "Mark as complete"
                      }
                      disabled={isPending}
                    />

                    <div className="flex-1">
                      {isEditing ? (
                        <div className="grid gap-3">
                          <input
                            className="input"
                            value={editingForm.title}
                            onChange={(event) =>
                              setEditingForm((currentForm) => ({
                                ...currentForm,
                                title: event.target.value,
                              }))
                            }
                          />
                          <textarea
                            className="input min-h-24 resize-y"
                            value={editingForm.notes}
                            onChange={(event) =>
                              setEditingForm((currentForm) => ({
                                ...currentForm,
                                notes: event.target.value,
                              }))
                            }
                          />
                          <select
                            className="input"
                            value={editingForm.projectId}
                            onChange={(event) =>
                              setEditingForm((currentForm) => ({
                                ...currentForm,
                                projectId: event.target.value,
                              }))
                            }
                          >
                            <option value="">No project</option>
                            {projects.map((project) => (
                              <option
                                key={project.id}
                                value={String(project.id)}
                              >
                                {project.name}
                              </option>
                            ))}
                          </select>
                        </div>
                      ) : (
                        <>
                          <h3
                            className={`text-lg font-semibold ${
                              task.completed
                                ? "text-emerald-900 line-through"
                                : "text-slate-900"
                            }`}
                          >
                            {task.title}
                          </h3>
                          {task.notes ? (
                            <p className="mt-2 text-sm leading-6 text-slate-600">
                              {task.notes}
                            </p>
                          ) : null}
                          <p className="mt-3 text-xs font-semibold uppercase tracking-[0.18em] text-slate-500">
                            {task.project ? task.project.name : "Unassigned"}
                          </p>
                        </>
                      )}

                      <p className="mt-3 text-xs uppercase tracking-[0.18em] text-slate-400">
                        Updated {formatDate(task.updatedAt)}
                      </p>
                    </div>
                  </div>

                  <div className="flex gap-2 sm:justify-end">
                    {isEditing ? (
                      <>
                        <button
                          type="button"
                          className="button-secondary"
                          onClick={() => {
                            setEditingTaskId(null);
                            setEditingForm(emptyForm);
                          }}
                          disabled={isPending}
                        >
                          Cancel
                        </button>
                        <button
                          type="button"
                          className="button-primary"
                          onClick={() => handleSave(task.id)}
                          disabled={isPending}
                        >
                          Save
                        </button>
                      </>
                    ) : (
                      <>
                        <button
                          type="button"
                          className="button-secondary"
                          onClick={() => startEditing(task)}
                          disabled={isPending}
                        >
                          Edit
                        </button>
                        <button
                          type="button"
                          className="button-danger"
                          onClick={() => handleDelete(task.id)}
                          disabled={isPending}
                        >
                          Delete
                        </button>
                      </>
                    )}
                  </div>
                </div>
              </li>
            );
          })}
        </ul>
      </section>
    </div>
  );
}
