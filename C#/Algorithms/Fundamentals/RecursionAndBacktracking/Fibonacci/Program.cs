using System;
using System.IO;

namespace Fibonacci
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());

            int previous = 1;
            int next = 1;

            for (int i = 2; i <= n; i++)
            {
                int temp = next;
                next += previous;
                previous = temp;
            }

            Console.WriteLine(next);
        }
    }
}
