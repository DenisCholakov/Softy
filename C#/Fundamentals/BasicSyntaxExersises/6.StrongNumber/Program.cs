using System;
using System.Diagnostics.CodeAnalysis;

namespace _6.StrongNumber
{
    class Program
    {
        static void Main(string[] args)
        {
            int num = int.Parse(Console.ReadLine());

            int tempNum = num;
            int sum = 0;
            while (tempNum != 0)
            {
                int a = tempNum % 10;
                int factoriel = 1;
                for (int i = 1; i <= a; i++)
                {
                    factoriel *= i;
                }
                sum += factoriel;
                tempNum /= 10;
            }

            if (num == sum)
            {
                Console.WriteLine("yes");
            }
            else
            {
                Console.WriteLine("no");
            }   
        }
    }
}
