using System;
using System.Globalization;

namespace Vowels
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = Console.ReadLine();

            Console.WriteLine(VowelsCount(input));
        }

        private static int VowelsCount(string text)
        {
            char[] vowels = new char[] { 'a', 'e', 'i', 'o', 'u', 'A', 'E', 'I', 'O', 'U'};

            int volwesCount = 0;

            for (int i = 0; i < text.Length; i++)
            {
                if (Array.IndexOf(vowels, text[i]) >= 0)
                {
                    volwesCount++;
                }
            }

            return volwesCount;
        }
    }
}
