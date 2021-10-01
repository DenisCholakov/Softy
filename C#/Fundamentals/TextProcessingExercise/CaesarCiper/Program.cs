using System;
using System.Text;

namespace CaesarCiper
{
    class Program
    {
        static void Main(string[] args)
        {
            string text = Console.ReadLine();
            StringBuilder result = new StringBuilder();

            for (int i = 0; i < text.Length; i++)
            {
                result.Append((char)(text[i] + 3));
            }

            System.Console.WriteLine(result);
        }
    }
}
