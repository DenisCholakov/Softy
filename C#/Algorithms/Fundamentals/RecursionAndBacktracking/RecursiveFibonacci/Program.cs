using System;
using System.IO;

namespace RecursiveFibonacci
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());

            int result = Fibonacci(n);

            Console.WriteLine(result);
        }

        private static int Fibonacci(int n)
        {
            if (n <= 1)
            {
                return 1;
            }

            return Fibonacci(n - 1) + Fibonacci(n - 2);
        }
    }
}
