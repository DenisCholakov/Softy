using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace WordCruncher
{
    class Program
    {
        private static List<string> comabination;
        private static bool[] locked;


        static void Main(string[] args)
        {
            HashSet<string> words = new HashSet<string>(Console.ReadLine().Split(", "));

            string target = Console.ReadLine();
            comabination = new List<string>();
            locked = new bool[words.Count];

            PrintCombinations(target, words.ToArray());
        }

        private static void PrintCombinations(string target, string[] words)
        {
            if (String.IsNullOrEmpty(target))
            {
                Console.WriteLine(String.Join(' ', comabination));
                return;
            }

            for (int i = 0; i < words.Length; i++)
            {
                if (IsWordFit(words[i], target) && !locked[i])
                {
                    comabination.Add(words[i]);
                    locked[i] = true;
                    PrintCombinations(target.Remove(0, words[i].Length), words);
                    locked[i] = false;
                    comabination.Remove(words[i]);
                }
            }
        }

        private static bool IsWordFit(string word, string target)
        {
            if (word.Length > target.Length)
            {
                return false;
            }

            for (int i = 0; i < word.Length; i++)
            {
                if (word[i] != target[i])
                {
                    return false;
                }
            }

            return true;
        }
    }
}
