using System;
using System.IO;

namespace VariationsWithoutRepetition
{
    class Program
    {
        private static string[] elements;
        private static int k;
        private static string[] variations;
        private static bool[] isTaken;

        static void Main(string[] args)
        {
            elements = Console.ReadLine().Split();
            k = int.Parse(Console.ReadLine());
            variations = new string[k];
            isTaken = new bool[elements.Length];

            PrintVariations(0);
        }

        private static void PrintVariations(int index)
        {
            if (index >= variations.Length)
            {
                Console.WriteLine(String.Join(' ', variations));
                return;
            }

            for (int i = 0; i < elements.Length; i++)
            {
                if (!isTaken[i])
                {
                    isTaken[i] = true;
                    variations[index] = elements[i];
                    PrintVariations(index + 1);
                    isTaken[i] = false;
                }
            }
        }
    }
}
