using System;

namespace TopNumber
{
    class Program
    {
        static void Main(string[] args)
        {
            int num = int.Parse(Console.ReadLine());

            if (num <= 16)
            {
                Console.WriteLine("There is no top numbers!");
            }

            PrintTopNumbers(num);
        }

        private static void PrintTopNumbers(int num)
        {
            for (int i = 17; i <= num; i++)
            {
                if (CheckIfTopNumber(i))
                {
                    Console.WriteLine(i);
                }
            }
        }

        private static bool CheckIfTopNumber(int n)
        {
            bool oddNum = false;
            int sum = 0;
            int digit = 0;

            while (n != 0)
            {
                digit = n % 10;
                sum += digit;
                if (digit % 2 != 0)
                {
                    oddNum = true;
                }

                n /= 10;
            }

            return oddNum && (sum % 8 == 0);
        }
    }
}
