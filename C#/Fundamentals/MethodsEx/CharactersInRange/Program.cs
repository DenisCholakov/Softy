using System;

namespace CharactersInRange
{
    class Program
    {
        static void Main(string[] args)
        {
            char first = char.Parse(Console.ReadLine());
            char second = char.Parse(Console.ReadLine());

            PrintCharsInBetween(first, second);
        }

        private static void PrintCharsInBetween( char char1, char char2)
        {
            int first = char1 < char2 ? char1 : char2;
            int second = char1 > char2 ? char1 : char2;
            for (int i = first + 1; i < second; i++)
            {
                Console.Write((char)i + " ");
            }

            Console.WriteLine();
        }
    }
}
