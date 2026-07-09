import {
  deleteProject,
  getProjectById,
  getProjectIdOrNull,
  updateProject,
  validateUpdateProject,
} from "@/lib/projects";
import { Prisma } from "@prisma/client";

async function getRouteId(context) {
  const params = await context.params;
  return getProjectIdOrNull(params.id);
}

function notFoundResponse() {
  return Response.json({ error: "Project not found." }, { status: 404 });
}

export async function GET(_request, context) {
  const projectId = await getRouteId(context);

  if (!projectId) {
    return notFoundResponse();
  }

  const project = await getProjectById(projectId);

  if (!project) {
    return notFoundResponse();
  }

  return Response.json(project, { status: 200 });
}

export async function PATCH(request, context) {
  const projectId = await getRouteId(context);

  if (!projectId) {
    return notFoundResponse();
  }

  let payload;

  try {
    payload = await request.json();
  } catch {
    return Response.json({ error: "Invalid JSON body." }, { status: 400 });
  }

  const validation = validateUpdateProject(payload);

  if (validation.error) {
    return Response.json({ error: validation.error }, { status: 400 });
  }

  try {
    const project = await updateProject(projectId, validation.data);
    return Response.json(project, { status: 200 });
  } catch (error) {
    if (
      error instanceof Prisma.PrismaClientKnownRequestError &&
      error.code === "P2025"
    ) {
      return notFoundResponse();
    }

    throw error;
  }
}

export async function DELETE(_request, context) {
  const projectId = await getRouteId(context);

  if (!projectId) {
    return notFoundResponse();
  }

  try {
    await deleteProject(projectId);
    return new Response(null, { status: 204 });
  } catch (error) {
    if (
      error instanceof Prisma.PrismaClientKnownRequestError &&
      error.code === "P2025"
    ) {
      return notFoundResponse();
    }

    throw error;
  }
}
