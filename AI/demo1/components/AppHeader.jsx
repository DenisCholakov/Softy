import Link from "next/link";

export default function AppHeader() {
  return (
    <header className="border-b border-slate-900/8 bg-white/60 backdrop-blur-xl">
      <div className="mx-auto flex w-full max-w-6xl items-center justify-between gap-4 px-5 py-4 sm:px-8 lg:px-10">
        <Link href="/" className="min-w-0">
          <p className="text-xs font-semibold uppercase tracking-[0.24em] text-amber-700">
            Taska Local
          </p>
          <p className="mt-1 truncate text-sm text-slate-600">
            Personal task management for focused local work.
          </p>
        </Link>

        <nav className="flex items-center gap-3">
          <Link className="button-secondary" href="/">
            Todos
          </Link>
          <Link className="button-secondary" href="/projects">
            Projects
          </Link>
        </nav>
      </div>
    </header>
  );
}
