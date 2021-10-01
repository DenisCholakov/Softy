using System;
using System.IO;

namespace CombinationsWithoutRepetition
{
    class Program
    {
        private static string[] elements;
        private static int k;
        private static string[] combinations;

        static void Main(string[] args)
        {
            elements = Console.ReadLine().Split();
            k = int.Parse(Console.ReadLine());
            combinations = new string[k];

            PrintCombinations(0, 0);
        }

        private static void PrintCombinations(int index, int elemIndx)
        {
            if (index >= k)
            {
                Console.WriteLine(String.Join(' ', combinations));
                return;
            }

            for (int i = elemIndx; i < elements.Length; i++)
            {
                combinations[index] = elements[i];
                PrintCombinations(index +1, i + 1);
            }
        }
    }
}
