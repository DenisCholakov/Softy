using System;
using System.Collections.Generic;
using System.IO;

namespace FibonacciDynamic
{
    class Program
    {
        // Contains the known fibonacci numbers
        private static Dictionary<int, long> memo;

        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            memo = new Dictionary<int, long> { { 0, 0 }, { 1, 1 }, { 2, 1 } };

            Console.WriteLine(GetFibonacci(n));
        }

        private static long GetFibonacci(int n)
        {
            if (memo.ContainsKey(n))
            {
                return memo[n];
            }

            memo.Add(n, GetFibonacci(n - 1) + GetFibonacci(n - 2));

            return memo[n];
        }
    }
}
