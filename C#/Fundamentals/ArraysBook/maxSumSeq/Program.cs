using System;
using System.Linq;

namespace maxSumSeq
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] arr = Console.ReadLine().Split().Select(int.Parse).ToArray();

            int maxSum = int.MinValue;
            for (int k = 0; k < arr.Length; k++)
            {
                for (int i = 0; i < arr.Length - k; i++)
                {
                    int sum = 0;
                    for (int j = 0; j < k; j++)
                    {
                        sum += arr[i + j];
                    }

                    if (sum > maxSum)
                    {
                        maxSum = sum;
                    }
                }
            }

            Console.WriteLine(maxSum);
        }
    }
}
