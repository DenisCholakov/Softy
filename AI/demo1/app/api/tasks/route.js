import { createTask, listTasks, validateCreateTask } from "@/lib/tasks";
import { Prisma } from "@prisma/client";

export const dynamic = "force-dynamic";

function invalidProjectResponse() {
  return Response.json({ error: "Project not found." }, { status: 400 });
}

export async function GET() {
  const tasks = await listTasks();

  return Response.json(tasks, { status: 200 });
}

export async function POST(request) {
  let payload;

  try {
    payload = await request.json();
  } catch {
    return Response.json({ error: "Invalid JSON body." }, { status: 400 });
  }

  const validation = validateCreateTask(payload);

  if (validation.error) {
    return Response.json({ error: validation.error }, { status: 400 });
  }

  try {
    const task = await createTask(validation.data);

    return Response.json(task, { status: 201 });
  } catch (error) {
    if (
      error instanceof Prisma.PrismaClientKnownRequestError &&
      error.code === "P2003"
    ) {
      return invalidProjectResponse();
    }

    throw error;
  }
}
