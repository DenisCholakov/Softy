using System;
using System.Collections.Generic;
using System.IO;

namespace BinomialCoefficients
{
    class Program
    {
        private static Dictionary<string, long> memo;

        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            int k = int.Parse(Console.ReadLine());
            memo = new Dictionary<string, long>();

            var binom = PascalTriangle(n, k);

            Console.WriteLine(binom);
        }

        private static long PascalTriangle(int row, int col)
        {
            var id = $"{row}, {col}";

            if (memo.ContainsKey(id))
            {
                return memo[id];
            }
            if (col == 0 || col == row)
            {
                return 1;
            }

            var result = PascalTriangle(row - 1, col) + PascalTriangle(row - 1, col - 1);
            memo.Add(id, result);
            return result;
        }
    }
}
