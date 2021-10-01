using System;
using System.Collections.Generic;
using System.IO;

namespace AreasInMatrix
{
    public class Node
    {
        public Node(int row, int col)
        {
            this.Row = row;
            this.Col = col;
        }

        public int Row { get; set; }
        public int Col { get; set; }
    }
    class Program
    {
        private static char[,] matrix;
        private static bool[,] visited;

        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            int m = int.Parse(Console.ReadLine());
            var areas = new SortedDictionary<char, int>();
            int areasCount = 0;

            matrix = ReadMatrix(n, m);
            visited = new bool[n, m];

            for (int r = 0; r < matrix.GetLength(0); r++)
            {
                for (int c = 0; c < matrix.GetLength(1); c++)
                {
                    if (visited[r, c])
                    {
                        continue;
                    }

                    DFS(r, c);

                    if (!areas.ContainsKey(matrix[r, c]))
                    {
                        areas.Add(matrix[r, c], 0);
                    }

                    areas[matrix[r, c]]++;
                    areasCount++;
                }
            }

            Console.WriteLine($"Areas: {areasCount}");

            foreach (var kvp in areas)
            {
                Console.WriteLine($"Letter '{kvp.Key}' -> {kvp.Value}");
            }
        }

        private static void DFS(int row, int col)
        {
            if (visited[row, col])
            {
                return;
            }

            var children = GetChildren(row, col);
            visited[row, col] = true;

            foreach (var child in children)
            {
                DFS(child.Row, child.Col);
            }
        }

        private static List<Node> GetChildren(int row, int col)
        {
            var result = new List<Node>();

            if (IsInside(row - 1, col) && IsChild(row, col, row - 1, col))
            {
                result.Add(new Node(row - 1, col));
            }
            
            if (IsInside(row + 1, col) && IsChild(row, col, row + 1, col))
            {
                result.Add(new Node(row + 1, col));
            }
            
            if (IsInside(row, col - 1) && IsChild(row, col, row, col - 1))
            {
                result.Add(new Node(row, col - 1));
            }
            
            if (IsInside(row, col + 1) && IsChild(row, col, row, col + 1))
            {
                result.Add(new Node(row, col + 1));
            }

            return result;
        }

        private static bool IsChild(int pRow, int pCol, int row, int col)
        {
            return !visited[row, col] && matrix[pRow, pCol] == matrix[row, col];
        }

        private static bool IsInside(int row, int col)
        {
            return row >= 0 && row < matrix.GetLength(0)
                    && col >= 0 && col < matrix.GetLength(1);
        }

        private static char[,] ReadMatrix(int n, int m)
        {
            var result = new char[n, m];

            for (int row = 0; row < n; row++)
            {
                var current = Console.ReadLine();

                for (int col = 0; col < m; col++)
                {
                    result[row, col] = current[col];
                }
            }

            return result;
        }
    }
}
