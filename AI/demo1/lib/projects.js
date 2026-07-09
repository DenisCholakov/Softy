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

function parseProjectId(id) {
  const parsedId = Number(id);

  if (!Number.isInteger(parsedId) || parsedId < 1) {
    return null;
  }

  return parsedId;
}

export function validateCreateProject(payload) {
  const source = getPayloadObject(payload);
  const name = normalizeText(source.name);
  const description = normalizeText(source.description);

  if (!name) {
    return { error: "Name is required." };
  }

  return {
    data: {
      name,
      description: description || null,
    },
  };
}

export function validateUpdateProject(payload) {
  const source = getPayloadObject(payload);
  const data = {};

  if (Object.hasOwn(source, "name")) {
    const name = normalizeText(source.name);

    if (!name) {
      return { error: "Name is required." };
    }

    data.name = name;
  }

  if (Object.hasOwn(source, "description")) {
    const description = normalizeText(source.description);
    data.description = description || null;
  }

  if (!Object.keys(data).length) {
    return { error: "No valid project fields were provided." };
  }

  return { data };
}

export function getProjectIdOrNull(id) {
  return parseProjectId(id);
}

export async function listProjects() {
  return prisma.project.findMany({
    orderBy: [{ updatedAt: "desc" }],
  });
}

export async function getProjectById(id) {
  return prisma.project.findUnique({
    where: { id },
  });
}

export async function createProject(data) {
  return prisma.project.create({
    data,
  });
}

export async function updateProject(id, data) {
  return prisma.project.update({
    where: { id },
    data,
  });
}

export async function deleteProject(id) {
  return prisma.project.delete({
    where: { id },
  });
}
