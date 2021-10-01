using System;

namespace MathOperations
{
    class Program
    {
        static void Main(string[] args)
        {
            double num1 = double.Parse(Console.ReadLine());
            string operation = Console.ReadLine();
            double num2 = double.Parse(Console.ReadLine());

            Console.WriteLine(Math.Round(MathOperations(num1, num2, operation),2));
        }

        private static double MathOperations(double n1, double n2, string p)
        {
            switch (p)
            {
                case "+": return n1 + n2;
                case "-": return n1 - n2;
                case "/": return n1 / n2;
                case "*": return n1 * n2;
            }

            return -1;
        }
    }
}
