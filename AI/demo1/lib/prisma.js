import path from "node:path";
import { PrismaBetterSqlite3 } from "@prisma/adapter-better-sqlite3";
import { PrismaClient } from "@prisma/client";

const globalForPrisma = globalThis;
const databaseUrl = process.env.DATABASE_URL || "file:./dev.db";
const sqlitePath = databaseUrl.startsWith("file:")
  ? databaseUrl.slice(5)
  : databaseUrl;
const adapter = new PrismaBetterSqlite3({
  url: path.resolve(process.cwd(), sqlitePath),
});

export const prisma =
  globalForPrisma.prisma ||
  new PrismaClient({
    adapter,
    log: process.env.NODE_ENV === "development" ? ["error", "warn"] : ["error"],
  });

if (process.env.NODE_ENV !== "production") {
  globalForPrisma.prisma = prisma;
}
