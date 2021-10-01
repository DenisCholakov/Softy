using System;

namespace MultiplicationSign
{
    class Program
    {
        static void Main(string[] args)
        {
            int n1 = int.Parse(Console.ReadLine());
            int n2 = int.Parse(Console.ReadLine());
            int n3 = int.Parse(Console.ReadLine());

            int neg = FindNumberOfNegNumb(n1, n2, n3);

            if (neg == 2 || neg == 0)
            {
                Console.WriteLine("positive");
            }
            else if (neg < 0)
            {
                Console.WriteLine("zero");
            }
            else
            {
                Console.WriteLine("negative");
            }
        }

        private static int FindNumberOfNegNumb(int n1, int n2, int n3)
        {
            int neg = 0;

            if (n1 < 0)
            {
                neg++;
            }

            if (n2 < 0)
            {
                neg++;
            }

            if (n3 < 0)
            {
                neg++;
            }

            if (n1 == 0 || n2 == 0 || n3 == 0)
            {
                neg = -1;
            }

            return neg;
        }
    }
}