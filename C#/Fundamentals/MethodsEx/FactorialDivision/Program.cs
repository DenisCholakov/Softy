using System;

namespace FactorialDivision
{
    class Program
    {
        static void Main(string[] args)
        {
            decimal num1 = decimal.Parse(Console.ReadLine());
            decimal num2 = decimal.Parse(Console.ReadLine());

            decimal result = Factorial(num1) / Factorial(num2);

            Console.WriteLine($"{result:f2}");
        }

        private static decimal Factorial(decimal num)
        {
            if (num == 1)
            {
                return 1;
            }

            return num * Factorial(num - 1);
        }
    }
}
