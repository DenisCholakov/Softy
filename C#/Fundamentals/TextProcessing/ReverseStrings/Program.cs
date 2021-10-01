using System;
using System.Collections.Generic;

namespace ReverseStrings
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, string> words = new Dictionary<string, string>();
            string word = Console.ReadLine();

            while (word != "end")
            {
                string reverseWord = String.Empty;
                for (int i = word.Length - 1; i >= 0; i--)
                {
                    reverseWord += word[i];
                }
                words.Add(word, reverseWord);
                word = Console.ReadLine();
            }

            foreach (var pair in words)
            {
                System.Console.WriteLine($"{pair.Key} = {pair.Value}");
            }
        }
    }
}
