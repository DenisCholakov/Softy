using System;
using System.Text;

namespace _9.CharToString
{
    class Program
    {
        static void Main(string[] args)
        {
            char firstChar = char.Parse(Console.ReadLine());
            char secondChar = char.Parse(Console.ReadLine());
            char thirdChar = char.Parse(Console.ReadLine());

            string combined = "" + firstChar + secondChar + thirdChar;

            string combinedWhithSB = new StringBuilder().Append(firstChar).Append(secondChar).Append(thirdChar).ToString();

            Console.WriteLine($"{firstChar}{secondChar}{thirdChar}");
        }
    }
}
