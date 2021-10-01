using System;
using System.Runtime.InteropServices;

namespace Triangle
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());

            PrintTriangle(n, 1);
        }

        private static void PrintTriangle(int n, int i)
        {
            if (n == i)
            {
                PrintAllNumbers(i);
                return;
            }

            PrintAllNumbers(i);
            PrintTriangle(n, i+1);
            PrintAllNumbers(i);
        }

        private static void PrintAllNumbers(int i)
        {
            for (int j = 1; j <= i; j++)
            {
                Console.Write(j + " ");
            }

            Console.WriteLine();
            return;
        }
    }
}
