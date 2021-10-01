using System;
using System.IO;

namespace CombinationsWithRepetition
{
    class Program
    {
        private static int[] combination;

        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            int k = int.Parse(Console.ReadLine());
            combination = new int[k];

            PrintCombinations(n, k, 0, 1);
        }

        private static void PrintCombinations(int n, int k, int index, int startNumber)
        {
            if (index >= combination.Length)
            {
                Console.WriteLine(String.Join(' ', combination));
                return;
            }

            for (int i = startNumber; i <= n; i++)
            {
                combination[index] = i;
                PrintCombinations(n, k, index + 1, i);
            }
        }
    }
}
