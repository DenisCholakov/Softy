using System;
using System.Diagnostics.CodeAnalysis;

namespace AddAndSubstract
{
    class Program
    {
        static void Main(string[] args)
        {
            int num1 = int.Parse(Console.ReadLine());
            int num2 = int.Parse(Console.ReadLine());
            int num3 = int.Parse(Console.ReadLine());

            int sum = Sum(num1, num2);
            int number = Substract(sum, num3);
            Console.WriteLine(number);
        }

        private static int Sum(int num1, int num2)
        {
            return num1 + num2;
        }

        private static int Substract(int num1, int num2)
        {
            return num1 - num2;
        }
    }
}
