using System;

namespace EvenAndOdds
{
    class Program
    {
        static void Main(string[] args)
        {
            int number = Math.Abs(int.Parse(Console.ReadLine()));

            Console.WriteLine(GetMultipleOfEvenAndOds(number));
        }

        private static int GetMultipleOfEvenAndOds(int number)
        {
            int evenSum = GetSumOfEvenDigits(number);
            int oddSum = GetSumOfOddDigits(number);

            return evenSum * oddSum;
        }

        private static int GetSumOfOddDigits(int number)
        {
            int sum = 0;
            while (number != 0)
            {
                int n = number % 10;
                if (n % 2 != 0)
                {
                    sum += n;
                }

                number /= 10;
            }

            return sum;
        }

        private static int GetSumOfEvenDigits(int number)
        {
            int sum = 0;
            while (number != 0)
            {
                int n = number % 10;
                if (n % 2 == 0)
                {
                    sum += n;
                }

                number /= 10;
            }

            return sum;
        }
    }
}
