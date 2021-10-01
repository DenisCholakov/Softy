using System;
using System.Collections.Generic;

namespace OddOcurrances
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] words = Console.ReadLine().Split();
            Dictionary<string, int> occ = new Dictionary<string, int>();

            foreach (string word in words)
            {
                string w = word.ToLower();
                if (occ.ContainsKey(w))
                {
                    occ[w]++;
                }
                else
                {
                    occ.Add(w, 1);
                }
            }

            foreach (var word in occ)
            {
                if (word.Value % 2 == 1)
                {
                    System.Console.Write($"{word.Key} ");
                }
            }
        }
    }
}
