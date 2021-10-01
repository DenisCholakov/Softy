using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace MoveDownRight
{
    class Program
    {
        static void Main(string[] args)
        {
            int rows = int.Parse(Console.ReadLine());
            int cols = int.Parse(Console.ReadLine());

            int[,] values = ReadMatrix(rows, cols);

            int[,] sums = new int[rows, cols];
            sums[0, 0] = values[0, 0];

            for (int c = 1; c < sums.GetLength(1); c++)
            {
                sums[0, c] = sums[0, c - 1] + values[0, c];
            }

            for (int r = 1; r < sums.GetLength(0); r++)
            {
                sums[r, 0] = sums[r - 1, 0] + values[r, 0];
            }

            for (int r = 1; r < sums.GetLength(0); r++)
            {
                for (int c = 1; c < sums.GetLength(1); c++)
                {
                    int previous = Math.Max(sums[r, c - 1], sums[r - 1, c]);
                    sums[r, c] = previous + values[r, c];
                }
            }

            var sequence = new Stack<string>();
            sequence.Push($"[{rows - 1}, {cols - 1}]");

            int row = rows - 1;
            int col = cols - 1;

            while (row > 0 && col > 0)
            {
                if (sums[row - 1, col] > sums[row, col - 1])
                {
                    row--;
                }
                else
                {
                    col--;
                }

                sequence.Push($"[{row}, {col}]");
            }

            while (row > 0)
            {
                row--;
                sequence.Push($"[{row}, {col}]");
            }

            while (col > 0)
            {
                col--;
                sequence.Push($"[{row}, {col}]");
            }

            Console.WriteLine(String.Join(' ', sequence));
        }

        private static int[,] ReadMatrix(int rows, int cols)
        {
            var result = new int[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                var currentRow = Console.ReadLine().Split().Select(int.Parse).ToArray();

                for (int j = 0; j < cols; j++)
                {
                    result[i, j] = currentRow[j];
                }
            }

            return result;
        }
    }
}
