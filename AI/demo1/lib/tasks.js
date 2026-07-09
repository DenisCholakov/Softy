import { prisma } from "@/lib/prisma";

function normalizeText(value) {
  if (typeof value !== "string") {
    return undefined;
  }

  return value.trim();
}

function getPayloadObject(payload) {
  if (!payload || typeof payload !== "object" || Array.isArray(payload)) {
    return {};
  }

  return payload;
}

function parsePositiveId(id) {
  const parsedId = Number(id);

  if (!Number.isInteger(parsedId) || parsedId < 1) {
    return null;
  }

  return parsedId;
}

function parseOptionalProjectId(value) {
  if (value === undefined) {
    return { provided: false };
  }

  if (value === null) {
    return { provided: true, value: null };
  }

  const normalizedValue = typeof value === "string" ? value.trim() : value;

  if (normalizedValue === "") {
    return { provided: true, value: null };
  }

  const projectId = parsePositiveId(normalizedValue);

  if (!projectId) {
    return { error: "Project must be a valid project id." };
  }

  return { provided: true, value: projectId };
}

const taskInclude = {
  project: {
    select: {
      id: true,
      name: true,
    },
  },
};

export function validateCreateTask(payload) {
  const source = getPayloadObject(payload);
  const title = normalizeText(source.title);
  const notes = normalizeText(source.notes);
  const project = parseOptionalProjectId(source.projectId);

  if (!title) {
    return { error: "Title is required." };
  }

  if (project.error) {
    return { error: project.error };
  }

  return {
    data: {
      title,
      notes: notes || null,
      ...(project.provided ? { projectId: project.value } : {}),
    },
  };
}

export function validateUpdateTask(payload) {
  const source = getPayloadObject(payload);
  const data = {};

  if (Object.hasOwn(source, "title")) {
    const title = normalizeText(source.title);

    if (!title) {
      return { error: "Title is required." };
    }

    data.title = title;
  }

  if (Object.hasOwn(source, "notes")) {
    const notes = normalizeText(source.notes);
    data.notes = notes || null;
  }

  if (Object.hasOwn(source, "completed")) {
    if (typeof source.completed !== "boolean") {
      return { error: "Completed must be a boolean." };
    }

    data.completed = source.completed;
  }

  if (Object.hasOwn(source, "projectId")) {
    const project = parseOptionalProjectId(source.projectId);

    if (project.error) {
      return { error: project.error };
    }

    data.projectId = project.value;
  }

  if (!Object.keys(data).length) {
    return { error: "No valid task fields were provided." };
  }

  return { data };
}

export function getTaskIdOrNull(id) {
  return parsePositiveId(id);
}

export async function listTasks() {
  return prisma.task.findMany({
    include: taskInclude,
    orderBy: [{ completed: "asc" }, { updatedAt: "desc" }],
  });
}

export async function getTaskById(id) {
  return prisma.task.findUnique({
    include: taskInclude,
    where: { id },
  });
}

export async function createTask(data) {
  return prisma.task.create({
    data,
    include: taskInclude,
  });
}

export async function updateTask(id, data) {
  return prisma.task.update({
    where: { id },
    data,
    include: taskInclude,
  });
}

export async function deleteTask(id) {
  return prisma.task.delete({
    where: { id },
  });
}
