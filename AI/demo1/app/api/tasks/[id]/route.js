import {
  deleteTask,
  getTaskById,
  getTaskIdOrNull,
  updateTask,
  validateUpdateTask,
} from "@/lib/tasks";
import { Prisma } from "@prisma/client";

async function getRouteId(context) {
  const params = await context.params;
  return getTaskIdOrNull(params.id);
}

function notFoundResponse() {
  return Response.json({ error: "Task not found." }, { status: 404 });
}

function invalidProjectResponse() {
  return Response.json({ error: "Project not found." }, { status: 400 });
}

export async function GET(_request, context) {
  const taskId = await getRouteId(context);

  if (!taskId) {
    return notFoundResponse();
  }

  const task = await getTaskById(taskId);

  if (!task) {
    return notFoundResponse();
  }

  return Response.json(task, { status: 200 });
}

export async function PATCH(request, context) {
  const taskId = await getRouteId(context);

  if (!taskId) {
    return notFoundResponse();
  }

  let payload;

  try {
    payload = await request.json();
  } catch {
    return Response.json({ error: "Invalid JSON body." }, { status: 400 });
  }

  const validation = validateUpdateTask(payload);

  if (validation.error) {
    return Response.json({ error: validation.error }, { status: 400 });
  }

  try {
    const task = await updateTask(taskId, validation.data);
    return Response.json(task, { status: 200 });
  } catch (error) {
    if (
      error instanceof Prisma.PrismaClientKnownRequestError &&
      error.code === "P2025"
    ) {
      return notFoundResponse();
    }

    if (
      error instanceof Prisma.PrismaClientKnownRequestError &&
      error.code === "P2003"
    ) {
      return invalidProjectResponse();
    }

    throw error;
  }
}

export async function DELETE(_request, context) {
  const taskId = await getRouteId(context);

  if (!taskId) {
    return notFoundResponse();
  }

  try {
    await deleteTask(taskId);
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
