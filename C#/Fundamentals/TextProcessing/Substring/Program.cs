using System;

namespace Substring
{
    class Program
    {
        static void Main(string[] args)
        {
            string wordToRemove = Console.ReadLine().ToLower();
            string text = Console.ReadLine();
            int startIndex = text.IndexOf(wordToRemove);

            while (startIndex != -1)
            {
                text = text.Remove(startIndex, wordToRemove.Length);
                startIndex = text.IndexOf(wordToRemove);
            }

            System.Console.WriteLine(text);
        }
    }
}
