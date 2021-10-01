using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Cinema
{
    class Program
    {
        private static List<string> names;
        private static HashSet<int> locked;
        private static string[] permutes;
        static void Main(string[] args)
        {
            names = Console.ReadLine().Split(", ").ToList();
            locked = new HashSet<int>();
            permutes = new string[names.Count];

            string input = Console.ReadLine();
            while (input != "generate")
            {
                string[] inputArgs = input.Split(" - ");
                string name = inputArgs[0];
                int position = int.Parse(inputArgs[1]) - 1;

                permutes[position] = name;
                locked.Add(position);
                names.Remove(name);

                input = Console.ReadLine();
            }

            PrintPermutations(0);
        }

        private static void PrintPermutations(int index)
        {
            if (index >= names.Count)
            {
                int j = 0;

                for (int i = 0; i < permutes.Length; i++)
                {
                    if (!locked.Contains(i))
                    {
                        permutes[i] = names[j];
                        j++;
                    }
                }
                
                Console.WriteLine(String.Join(' ', permutes));
                return;
            }

            PrintPermutations(index + 1);

            for (int i = index + 1; i < names.Count; i++)
            {
                Swap(index, i);
                PrintPermutations(index + 1);
                Swap(index, i);
            }
        }

        private static void Swap(int first, int second)
        {
            var temp = names[first];
            names[first] = names[second];
            names[second] = temp;
        }
    }
}
