using System;
using System.IO;

namespace NestedLoops
{
    class Program
    {
        private static int[] vector;
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            vector = new int[n];

            PrintVectors(0, n);
        }

        private static void PrintVectors(int index, int n)
        {
            if (index >= vector.Length)
            {
                Console.WriteLine(String.Join(' ', vector));
                return;
            }

            for (int i = 1; i <= n; i++)
            {
                vector[index] = i;
                PrintVectors(index + 1, n);
            }
        }
    }
}
