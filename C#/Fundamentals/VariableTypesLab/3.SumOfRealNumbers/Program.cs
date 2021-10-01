using System;

namespace _3.SumOfRealNumbers
{
    class Program
    {
        static void Main(string[] args)
        {
            int numberCount = int.Parse(Console.ReadLine());


            decimal sum = 0;
            for (int i = 0; i < numberCount; i++)
            {
                decimal realNum = decimal.Parse(Console.ReadLine());
                sum += realNum;
            }

            Console.WriteLine(sum);
        }
    }
}
