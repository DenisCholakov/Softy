"use client";

import { useEffect, useState, useTransition } from "react";

const emptyForm = {
  name: "",
  description: "",
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

export default function ProjectApp() {
  const [projects, setProjects] = useState([]);
  const [form, setForm] = useState(emptyForm);
  const [editingProjectId, setEditingProjectId] = useState(null);
  const [editingForm, setEditingForm] = useState(emptyForm);
  const [error, setError] = useState("");
  const [isLoading, setIsLoading] = useState(true);
  const [isPending, startTransition] = useTransition();

  useEffect(() => {
    let isActive = true;

    async function loadProjects() {
      try {
        const data = await requestJson("/api/projects");

        if (!isActive) {
          return;
        }

        setError("");
        setProjects(data);
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

    loadProjects();

    return () => {
      isActive = false;
    };
  }, []);

  const handleCreate = async (event) => {
    event.preventDefault();

    startTransition(async () => {
      try {
        setError("");
        const createdProject = await requestJson("/api/projects", {
          method: "POST",
          body: JSON.stringify(form),
        });

        setProjects((currentProjects) => [createdProject, ...currentProjects]);
        setForm(emptyForm);
      } catch (requestError) {
        setError(requestError.message);
      }
    });
  };

  const handleDelete = (projectId) => {
    startTransition(async () => {
      try {
        setError("");
        await requestJson(`/api/projects/${projectId}`, {
          method: "DELETE",
        });

        setProjects((currentProjects) =>
          currentProjects.filter((project) => project.id !== projectId),
        );

        if (editingProjectId === projectId) {
          setEditingProjectId(null);
          setEditingForm(emptyForm);
        }
      } catch (requestError) {
        setError(requestError.message);
      }
    });
  };

  const startEditing = (project) => {
    setEditingProjectId(project.id);
    setEditingForm({
      name: project.name,
      description: project.description || "",
    });
  };

  const handleSave = (projectId) => {
    startTransition(async () => {
      try {
        setError("");
        const updatedProject = await requestJson(`/api/projects/${projectId}`, {
          method: "PATCH",
          body: JSON.stringify(editingForm),
        });

        setProjects((currentProjects) =>
          currentProjects.map((project) =>
            project.id === projectId ? updatedProject : project,
          ),
        );
        setEditingProjectId(null);
        setEditingForm(emptyForm);
      } catch (requestError) {
        setError(requestError.message);
      }
    });
  };

  return (
    <div className="grid gap-6 lg:grid-cols-[minmax(0,22rem)_minmax(0,1fr)]">
      <section className="panel p-6 sm:p-7">
        <p className="eyebrow">Structure</p>
        <h2 className="mt-3 text-2xl font-semibold text-slate-950">
          Create a project space.
        </h2>
        <p className="mt-2 text-sm leading-6 text-slate-600">
          Group related work so task assignment stays clear and lightweight.
        </p>

        <form className="mt-6 grid gap-4" onSubmit={handleCreate}>
          <label className="grid gap-2">
            <span className="text-sm font-medium text-slate-700">Name</span>
            <input
              className="input"
              value={form.name}
              onChange={(event) =>
                setForm((currentForm) => ({
                  ...currentForm,
                  name: event.target.value,
                }))
              }
              placeholder="Name the project"
              maxLength={120}
            />
          </label>

          <label className="grid gap-2">
            <span className="text-sm font-medium text-slate-700">
              Description
            </span>
            <textarea
              className="input min-h-28 resize-y"
              value={form.description}
              onChange={(event) =>
                setForm((currentForm) => ({
                  ...currentForm,
                  description: event.target.value,
                }))
              }
              placeholder="Optional scope or intent"
              maxLength={400}
            />
          </label>

          <button className="button-primary" disabled={isPending} type="submit">
            {isPending ? "Saving..." : "Create project"}
          </button>
        </form>
      </section>

      <section className="panel p-6 sm:p-7">
        <div className="flex flex-col gap-2 border-b border-slate-200 pb-5 sm:flex-row sm:items-end sm:justify-between">
          <div>
            <p className="eyebrow">Organization</p>
            <h2 className="mt-2 text-2xl font-semibold text-slate-950">
              Projects
            </h2>
          </div>
          <p className="text-sm text-slate-500">
            {projects.length} {projects.length === 1 ? "project" : "projects"}
          </p>
        </div>

        {error ? (
          <p className="mt-4 rounded-2xl border border-rose-200 bg-rose-50 px-4 py-3 text-sm text-rose-700">
            {error}
          </p>
        ) : null}

        {isLoading ? (
          <div className="mt-6 rounded-3xl border border-dashed border-slate-300 bg-slate-50 px-6 py-10 text-center text-sm text-slate-500">
            Loading projects...
          </div>
        ) : null}

        {!isLoading && !projects.length ? (
          <div className="mt-6 rounded-3xl border border-dashed border-slate-300 bg-slate-50 px-6 py-10 text-center">
            <p className="text-base font-medium text-slate-700">
              No projects yet.
            </p>
            <p className="mt-2 text-sm leading-6 text-slate-500">
              Create one to start grouping related tasks.
            </p>
          </div>
        ) : null}

        <ul className="mt-6 grid gap-4">
          {projects.map((project) => {
            const isEditing = editingProjectId === project.id;

            return (
              <li
                key={project.id}
                className="rounded-3xl border border-slate-200 bg-white p-5 transition"
              >
                <div className="flex flex-col gap-4 sm:flex-row sm:items-start sm:justify-between">
                  <div className="flex-1">
                    {isEditing ? (
                      <div className="grid gap-3">
                        <input
                          className="input"
                          value={editingForm.name}
                          onChange={(event) =>
                            setEditingForm((currentForm) => ({
                              ...currentForm,
                              name: event.target.value,
                            }))
                          }
                        />
                        <textarea
                          className="input min-h-24 resize-y"
                          value={editingForm.description}
                          onChange={(event) =>
                            setEditingForm((currentForm) => ({
                              ...currentForm,
                              description: event.target.value,
                            }))
                          }
                        />
                      </div>
                    ) : (
                      <>
                        <h3 className="text-lg font-semibold text-slate-900">
                          {project.name}
                        </h3>
                        {project.description ? (
                          <p className="mt-2 text-sm leading-6 text-slate-600">
                            {project.description}
                          </p>
                        ) : null}
                      </>
                    )}

                    <p className="mt-3 text-xs uppercase tracking-[0.18em] text-slate-400">
                      Updated {formatDate(project.updatedAt)}
                    </p>
                  </div>

                  <div className="flex gap-2 sm:justify-end">
                    {isEditing ? (
                      <>
                        <button
                          type="button"
                          className="button-secondary"
                          onClick={() => {
                            setEditingProjectId(null);
                            setEditingForm(emptyForm);
                          }}
                          disabled={isPending}
                        >
                          Cancel
                        </button>
                        <button
                          type="button"
                          className="button-primary"
                          onClick={() => handleSave(project.id)}
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
                          onClick={() => startEditing(project)}
                          disabled={isPending}
                        >
                          Edit
                        </button>
                        <button
                          type="button"
                          className="button-danger"
                          onClick={() => handleDelete(project.id)}
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
