using System;
using System.Linq;

namespace SumMatrixColumns
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] size = Console.ReadLine().Split(", ").Select(int.Parse).ToArray();
            int[,] matrix = new int[size[0], size[1]];

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                int[] row = Console.ReadLine().Split().Select(int.Parse).ToArray();

                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    matrix[i, j] = row[j];
                }
            }

            for (int i = 0; i < matrix.GetLength(1); i++)
            {
                int columnSum = 0;
                for (int j = 0; j < matrix.GetLength(0); j++)
                {
                    columnSum += matrix[j, i];
                }

                Console.WriteLine(columnSum);
            }
        }
    }
}
