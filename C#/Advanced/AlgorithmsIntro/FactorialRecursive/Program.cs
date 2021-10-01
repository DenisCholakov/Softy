using System;
using System.IO;

namespace FactorialRecursive
{
    class Program
    {
        static void Main(string[] args)
        {
            int num = int.Parse(Console.ReadLine());

            Console.WriteLine(Factorial(num));
        }

        private static int Factorial(int num)
        {
            if (num == 1)
            {
                return 1;
            }

            return num * Factorial(num - 1);
        }
    }
}
