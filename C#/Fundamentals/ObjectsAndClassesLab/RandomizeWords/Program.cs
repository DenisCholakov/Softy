using System;
using System.Linq;

namespace RandomizeWords
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] words = Console.ReadLine().Split().ToArray();

            Random rand = new Random();

            for (int i = 0; i < words.Length; i++)
            {
                int swapIndex = rand.Next(words.Length);
                string temp = words[i];
                words[i] = words[swapIndex];
                words[swapIndex] = temp;
            }

            Console.WriteLine(String.Join(Environment.NewLine, words));
        }
    }
}
