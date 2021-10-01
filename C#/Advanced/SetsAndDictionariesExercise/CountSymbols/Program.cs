using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;

namespace CountSymbols
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = Console.ReadLine();
            SortedDictionary<char, int> occurances = new SortedDictionary<char, int>();

            for (int i = 0; i < input.Length; i++)
            {
                if (!occurances.ContainsKey(input[i]))
                {
                    occurances.Add(input[i], 0);
                }

                occurances[input[i]]++;
            }

            foreach (var pair in occurances)
            {
                Console.WriteLine( $"{pair.Key}: {pair.Value} time/s");
            }
        }
    }
}
