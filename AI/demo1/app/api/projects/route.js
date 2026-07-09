import {
  createProject,
  listProjects,
  validateCreateProject,
} from "@/lib/projects";

export const dynamic = "force-dynamic";

export async function GET() {
  const projects = await listProjects();

  return Response.json(projects, { status: 200 });
}

export async function POST(request) {
  let payload;

  try {
    payload = await request.json();
  } catch {
    return Response.json({ error: "Invalid JSON body." }, { status: 400 });
  }

  const validation = validateCreateProject(payload);

  if (validation.error) {
    return Response.json({ error: validation.error }, { status: 400 });
  }

  const project = await createProject(validation.data);

  return Response.json(project, { status: 201 });
}
