using System;
using System.ComponentModel;

namespace Calculations
{
    class Program
    {
        static void Main(string[] args)
        {
            string action = Console.ReadLine();
            int num1 = int.Parse(Console.ReadLine());
            int num2 = int.Parse(Console.ReadLine());

            switch (action)
            {
                case "add": Add(num1, num2);
                    break;
                case "multiply": Multiply(num1, num2);
                    break;
                case "substract": Substract(num1, num2);
                    break;
                case "divide": Divide(num1, num2);
                    break;
            }
        }

        private static void Divide(in int num1, in int num2)
        {
            Console.WriteLine(num1 / num2);
        }

        private static void Substract(in int num1, in int num2)
        {
            Console.WriteLine(num1 - num2);
        }

        private static void Multiply(in int num1, in int num2)
        {
            Console.WriteLine(num1 * num2);
        }

        private static void Add(in int num1, in int num2)
        {
            Console.WriteLine(num1 + num2);
        }
    }
}
