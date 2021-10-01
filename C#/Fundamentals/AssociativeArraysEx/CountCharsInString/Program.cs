using System;
using System.Collections.Generic;

namespace CountCharsInString
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] words = Console.ReadLine().Split();

            Dictionary<char, int> chars = new Dictionary<char, int>();
            foreach (var word in words)
            {
                for (int i = 0; i < word.Length; i++)
                {
                    char ch = word[i];
                    if (chars.ContainsKey(ch))
                    {
                        chars[ch]++;
                    }
                    else
                    {
                        chars.Add(ch, 1);
                    }
                }
            }

            foreach (var couple in chars)
            {
                System.Console.Write($"{couple.Key} -> {couple.Value}");
                System.Console.WriteLine();
            }
        }
    }
}
