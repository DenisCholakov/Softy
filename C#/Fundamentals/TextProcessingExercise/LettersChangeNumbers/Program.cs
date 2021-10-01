using System;

namespace LettersChangeNumbers
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] numbers = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);

            double sum = 0;
            foreach (var num in numbers)
            {
                char firstLetter = num[0];
                char lastLetter = num[num.Length - 1];
                double number = double.Parse(num.Substring(1, num.Length - 2));

                if (char.IsUpper(firstLetter))
                {
                    number /= firstLetter - 64;
                }
                else
                {
                    number *= firstLetter - 96;
                }

                if (char.IsUpper(lastLetter))
                {
                    number -= (lastLetter - 64);
                }
                else
                {
                    number += (lastLetter - 96);
                }

                sum += number;
            }

            System.Console.WriteLine($"{sum:f2}");
        }
    }
}
