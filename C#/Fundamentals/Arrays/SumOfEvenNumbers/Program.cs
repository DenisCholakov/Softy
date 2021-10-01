using System;
using System.Linq;

namespace SumOfEvenNumbers
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] strNums = Console.ReadLine().Split(' ');

            int sum = 0;
            for (int i = 0; i < strNums.Length; i++)
            {
                int num = int.Parse(strNums[i]);

                if (num % 2 == 0)
                {
                    sum += num;
                }
            }

            Console.WriteLine(sum);
        }
    }
}
