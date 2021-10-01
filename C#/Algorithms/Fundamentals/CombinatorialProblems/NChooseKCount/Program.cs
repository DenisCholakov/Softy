using System;
using System.IO;

namespace NChooseKCount
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            int k = int.Parse(Console.ReadLine());

            Console.WriteLine(Pascal(n, k));
        }

        private static int Pascal(int row, int col)
        {
            if (row <= 1 || col == 0 || col == row)
            {
                return 1;
            }

            return Pascal(row - 1, col - 1) + Pascal(row - 1, col);
        }
    }
}
