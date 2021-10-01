using System;
using System.Collections.Generic;
using System.IO;

namespace PathsInLabyrinth
{
    class Program
    {
        private static bool[,] isVisited;
        private static char[,] labyrinth;
        private static List<char> path;
        static void Main(string[] args)
        {
            int rows = int.Parse(Console.ReadLine());
            int cols = int.Parse(Console.ReadLine());

            labyrinth = new char[rows, cols];
            isVisited = new bool[rows, cols];
            path = new List<char>();
            InitializeLabyrinth();

            // PrintLabyrinth();


            FindPaths(0, 0, ' ');
        }

        private static void FindPaths(int posRow, int posCol, char move)
        {
            if (!CanMove(posRow, posCol))
            {
                return;
            }

            path.Add(move);

            if (labyrinth[posRow, posCol] == 'e')
            {
                Console.WriteLine(String.Join("", path).Trim());
                path.RemoveAt(path.Count - 1);
                return;
            }

            isVisited[posRow, posCol] = true;

            FindPaths(posRow - 1, posCol, 'U');
            FindPaths(posRow + 1, posCol, 'D');
            FindPaths(posRow, posCol - 1, 'L');
            FindPaths(posRow, posCol + 1, 'R');

            path.RemoveAt(path.Count - 1);
            isVisited[posRow, posCol] = false;
        }

        private static bool CanMove(int posRow, int posCol)
        {
            return IsInBoundaries(posRow, posCol) && !isVisited[posRow, posCol] && labyrinth[posRow, posCol] != '*';
        }

        private static bool IsInBoundaries(int posRow, int posCol)
        {
            return posRow >= 0 && posRow < labyrinth.GetLength(0)
                            && posCol >= 0 && posCol < labyrinth.GetLength(1);
        }

        private static void PrintLabyrinth()
        {
            for (int i = 0; i < labyrinth.GetLength(0); i++)
            {
                for (int j = 0; j < labyrinth.GetLength(1); j++)
                {
                    Console.Write(labyrinth[i,j]);
                }

                Console.WriteLine();
            }
        }

        private static void InitializeLabyrinth()
        {
            for (int i = 0; i < labyrinth.GetLength(0); i++)
            {
                string currRow = Console.ReadLine();

                for (int j = 0; j < labyrinth.GetLength(1); j++)
                {
                    labyrinth[i, j] = currRow[j];
                }
            }
        }
    }
}
