using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ConnectedAreasInMatrix
{
    public class Area
    {
        public int row;
        public int col;
        public int size;

        public Area(int row, int col, int size)
        {
            this.row = row;
            this.col = col;
            this.size = size;
        }
    }
    class Program
    {
        private static char[,] matrix;
        private static bool[,] isVisited;
        private static List<Area> areas;

        static void Main(string[] args)
        {
            int r = int.Parse(Console.ReadLine());
            int c = int.Parse(Console.ReadLine());
            matrix = new char[r, c];
            isVisited = new bool[r, c];
            areas = new List<Area>();

            ReadMatrix();

            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    if (matrix[row, col] == '*')
                    {
                        continue;
                    }
                    else if (isVisited[row, col])
                    {
                        continue;
                    }

                    int areaSize = FindArea(row, col);

                    areas.Add(new Area(row, col, areaSize));
                }
            }

            Console.WriteLine($"Total areas found: {areas.Count}");
            int index = 1;
            
            foreach (var area in areas.OrderByDescending(a => a.size).ThenBy(a => a.row).ThenBy(a => a.col))
            {
                Console.WriteLine($"Area #{index} at ({area.row}, {area.col}), size: {area.size}");

                index++;
            }
        }

        private static int FindArea(int row, int col)
        {
            if (IsBorder(row, col))
            {
                return 0;
            }

            isVisited[row, col] = true;

            return 1 + FindArea(row - 1, col) + FindArea(row + 1, col) + FindArea(row, col - 1) + FindArea(row, col + 1);
        }

        private static bool IsBorder(int row, int col)
        {
            return IsOutside(row, col) || matrix[row, col] == '*' || isVisited[row, col];
        }

        private static bool IsOutside(int row, int col)
        {
            return row < 0 || col < 0 ||
                    row >= matrix.GetLength(0) ||
                    col >= matrix.GetLength(1);
        }

        private static void ReadMatrix()
        {
            for (int r = 0; r < matrix.GetLength(0); r++)
            {
                string row = Console.ReadLine();

                for (int c = 0; c < matrix.GetLength(1); c++)
                {
                    matrix[r, c] = row[c];
                }
            }
        }
    }
}
