using System;
using System.Linq;

namespace EqualSums
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] arr = Console.ReadLine().Split().Select(int.Parse).ToArray();

            bool haveSums = false;
            for (int i = 0; i < arr.Length; i++)
            {
                int leftSum = 0;
                int rightSum = 0;

                for (int j = i - 1; j >= 0; j--)
                {
                    leftSum += arr[j];
                }

                for (int j = i + 1; j < arr.Length; j++)
                {
                    rightSum += arr[j];
                }

                if (leftSum == rightSum)
                {
                    Console.Write($"{i} ");
                    haveSums = true;
                }
            }

            if (!haveSums)
            {
                Console.WriteLine("no");
            }
        }
    }
}
