import TaskApp from "@/components/TaskApp";

export default function Home() {
  return (
    <main className="mx-auto flex w-full max-w-6xl flex-1 flex-col px-5 py-10 sm:px-8 lg:px-10 lg:py-14">
      <section className="hero-panel rounded-[2rem] px-6 py-8 sm:px-8 sm:py-10">
        <p className="eyebrow">Local task manager</p>
        <div className="mt-4 max-w-3xl">
          <h1 className="text-4xl font-semibold tracking-tight text-slate-950 sm:text-5xl">
            Organize daily work without the clutter.
          </h1>
          <p className="mt-4 max-w-2xl text-base leading-7 text-slate-600 sm:text-lg">
            A quiet workspace for personal todos, project steps, and long-term
            goals. Create, edit, assign, complete, and delete tasks through a
            small REST-style API.
          </p>
        </div>
      </section>

      <section className="mt-8 flex-1">
        <TaskApp />
      </section>
    </main>
  );
}
