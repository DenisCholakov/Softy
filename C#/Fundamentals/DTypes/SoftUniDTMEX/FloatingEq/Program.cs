using System;
using System.Globalization;

namespace FloatingEq
{
    class Program
    {
        static void Main(string[] args)
        {

            //decimal num1 = decimal.Parse(Console.ReadLine());
            //decimal num2 = decimal.Parse(Console.ReadLine());

            //num1 = decimal.Round(num1, 6);
            //num2 = decimal.Round(num2, 6);

            //Console.WriteLine(num1 == num2);

            double num1 = double.Parse(Console.ReadLine());
            double num2 = double.Parse(Console.ReadLine());

            double diff = Math.Abs(num1 - num2);

            Console.WriteLine(diff < 0.000001);
        }
    }
}
