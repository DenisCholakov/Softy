using System;

namespace _12.RefactorSpecialNumbers
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());

            
            for (int i = 1; i <= n; i++)
            {
                int num = i;
                int sum = 0;
                while (num != 0)
                {
                    sum += num % 10;
                    num /= 10;
                }
                bool isMagic = (sum == 5) || (sum == 7) || (sum == 11);

                Console.WriteLine("{0} -> {1}", i, isMagic);
            }

        }
    }
}
