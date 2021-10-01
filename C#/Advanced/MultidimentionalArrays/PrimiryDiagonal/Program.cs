using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace PrimiryDiagonal
{
    class Program
    {
        static void Main(string[] args)
        {
            int size = int.Parse(Console.ReadLine());
            int[,] matrix = new int[size, size];
            int sum = 0;

            for (int i = 0; i < size; i++)
            {
                int[] row = Console.ReadLine().Split().Select(int.Parse).ToArray();

                for (int j = 0; j < size; j++)
                {
                    matrix[i, j] = row[j];

                    if (i == j)
                    {
                        sum += matrix[i, j];
                    }
                }
            }

            Console.WriteLine(sum);
        }
    }
}
