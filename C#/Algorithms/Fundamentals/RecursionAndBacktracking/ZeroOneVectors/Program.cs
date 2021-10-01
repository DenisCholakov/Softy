using System;
using System.IO;

namespace ZeroOneVectors
{
    class Program
    {
        private static int[] vector;

        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            vector = new int[n];

            GenerateZeroOneVectors(0);
        }

        private static void GenerateZeroOneVectors(int n)
        {
            if (n >= vector.Length)
            {
                Console.WriteLine(String.Join("", vector));
                return;
            }

            for (int i = 0; i <= 1; i++)
            {
                vector[n] = i;
                GenerateZeroOneVectors(n + 1);
            }
        }
    }
}
