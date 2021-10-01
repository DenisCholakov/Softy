using System;

namespace TribonacciSequence
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            int[] tribonacciNumbers = new int[n];

            for (int i = 0; i < n; i++)
            {
                if (i == 0 || i == 1)
                {
                    tribonacciNumbers[i] = 1;
                    continue;
                }
                else if (i == 2)
                {
                    tribonacciNumbers[i] = 2;
                    continue;
                }
                tribonacciNumbers[i] =
                    tribonacciNumbers[i - 1] + tribonacciNumbers[i - 2] + tribonacciNumbers[i - 3];
            }

            Console.WriteLine(String.Join(' ', tribonacciNumbers));
        }
    }
}
