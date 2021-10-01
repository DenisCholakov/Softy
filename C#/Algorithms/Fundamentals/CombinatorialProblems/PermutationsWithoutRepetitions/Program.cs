using System;
using System.IO;
using System.Linq;

namespace PermutationsWithoutRepetitions
{
    class Program
    {
        private static string[] elements;
        private static bool[] isUsed;
        private static string[] permutations;

        static void Main(string[] args)
        {
            elements = Console.ReadLine().Split();
            isUsed = new bool[elements.Length];
            permutations = new string[elements.Length];

            PrintPermuations(0);
        }

        private static void PrintPermuations(int index)
        {
            if (index >= elements.Length)
            {
                Console.WriteLine(String.Join(' ', permutations));
            }

            for (int i = 0; i < elements.Length; i++)
            {
                if (!isUsed[i])
                {
                    permutations[index] = elements[i];
                    isUsed[i] = true;
                    PrintPermuations(index + 1);
                    isUsed[i] = false;
                }
            }
        }
    }
}
