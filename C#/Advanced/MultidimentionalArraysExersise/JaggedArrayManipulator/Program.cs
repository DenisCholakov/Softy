using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace JaggedArrayManipulator
{
    class Program
    {
        static void Main(string[] args)
        {
            int lines = int.Parse(Console.ReadLine());
            double[][] matrix = new double[lines][];
            matrix[0] = Console.ReadLine().Split().Select(double.Parse).ToArray();

            for (int i = 1; i < matrix.Length; i++)
            {
                matrix[i] = Console.ReadLine().Split().Select(double.Parse).ToArray();

                if (matrix[i].Length == matrix[i-1].Length)
                {
                    for (int j = 0; j < matrix[i].Length; j++)
                    {
                        matrix[i][j] *= 2;
                        matrix[i - 1][j] *= 2;
                    }
                }
                else
                {
                    for (int j = 0; j < matrix[i].Length; j++)
                    {
                        matrix[i][j] /= 2;
                    }

                    for (int k = 0; k < matrix[i - 1].Length; k++)
                    {
                        matrix[i - 1][k] /= 2;
                    }
                }
            }

            string input = Console.ReadLine();

            while (input != "End")
            {
                string[] command = input.Split();
                if (command[0] == "Add")
                {
                    int row = int.Parse(command[1]);
                    int col = int.Parse(command[2]);
                    int value = int.Parse(command[3]);

                    if (!IndexIsValid(row, col, matrix))
                    {
                        input = Console.ReadLine();
                        continue;
                    }

                    matrix[row][col] += value;
                }
                else if (command[0] == "Subtract")
                {
                    int row = int.Parse(command[1]);
                    int col = int.Parse(command[2]);
                    int value = int.Parse(command[3]);

                    if (!IndexIsValid(row, col, matrix))
                    {
                        input = Console.ReadLine();
                        continue;
                    }

                    matrix[row][col] -= value;
                }
                input = Console.ReadLine();
            }

            PrintMatrix(matrix);
        }

        private static void PrintMatrix(double[][] matrix)
        {
            foreach (var line in matrix)
            {
                Console.WriteLine(String.Join(' ', line));
            }
        }

        private static bool IndexIsValid(int row, int col, double[][] matrix)
        {
            if (row < 0 || row >= matrix.Length)
            {
                return false;
            }
            else if (col < 0 || col >= matrix[row].Length)
            {
                return false;
            }

            return true;
        }
    }
}
