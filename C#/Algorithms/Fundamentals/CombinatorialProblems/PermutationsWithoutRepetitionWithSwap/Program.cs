using System;
using System.IO;

namespace PermutationsWithoutRepetitionWithSwap
{
    class Program
    {
        private static string[] elements;

        static void Main(string[] args)
        {
            elements = Console.ReadLine().Split();

            PermutationsWithSwap(0);
        }

        private static void PermutationsWithSwap(int index)
        {
            if (index >= elements.Length)
            {
                Console.WriteLine(String.Join(" ", elements));
                return;
            }

            PermutationsWithSwap(index + 1);

            for (int i = index + 1; i < elements.Length; i++)
            {
                Swap(index, i);
                PermutationsWithSwap(index + 1);
                Swap(index, i);
            }
        }

        private static void Swap(int index, int i)
        {
            var temp = elements[index];
            elements[index] = elements[i];
            elements[i] = temp;
        }
    }
}
