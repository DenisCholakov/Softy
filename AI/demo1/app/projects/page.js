import ProjectApp from "@/components/ProjectApp";

export const metadata = {
  title: "Projects | Taska Local",
};

export default function ProjectsPage() {
  return (
    <main className="mx-auto flex w-full max-w-6xl flex-1 flex-col px-5 py-10 sm:px-8 lg:px-10 lg:py-14">
      <section className="hero-panel rounded-[2rem] px-6 py-8 sm:px-8 sm:py-10">
        <p className="eyebrow">Projects</p>
        <div className="mt-4 max-w-3xl">
          <h1 className="text-4xl font-semibold tracking-tight text-slate-950 sm:text-5xl">
            Organize work beyond single tasks.
          </h1>
          <p className="mt-4 max-w-2xl text-base leading-7 text-slate-600 sm:text-lg">
            Create projects, refine their scope, and use them as the structure
            behind task assignment.
          </p>
        </div>
      </section>

      <section className="mt-8 flex-1">
        <ProjectApp />
      </section>
    </main>
  );
}
