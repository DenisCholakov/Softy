using System;

namespace MiddleCharacter
{
    class Program
    {
        static void Main(string[] args)
        {
            string text = Console.ReadLine();

            PrintMiddleChars(text);
        }

        private static void PrintMiddleChars(string text)
        {
            bool isEven = text.Length % 2 == 0 ? true : false;

            int index = text.Length / 2;
            if (isEven)
            {
                Console.Write(text[index - 1]);
                Console.WriteLine(text[index]);
            }
            else
            {
                Console.WriteLine(text[index]);
            }
        }
    }
}
