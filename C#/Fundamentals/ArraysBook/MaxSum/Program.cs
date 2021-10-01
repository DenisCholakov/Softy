using System;
using System.Linq;

namespace MaxSum
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            int k = int.Parse(Console.ReadLine());
            int[] arr = Console.ReadLine().Split().Select(int.Parse).ToArray();

            int maxSum = int.MinValue;
            int maxIndex = 0;
            for (int i = 0; i <= arr.Length - k; i++)
            {
                int sum = 0;
                for (int j = 0; j < k; j++)
                {
                    sum += arr[i + j];
                }

                if (maxSum < sum)
                {
                    maxSum = sum;
                    maxIndex = i;
                }
            }

            for (int i = maxIndex; i < maxIndex + k; i++)
            {
                Console.Write(arr[i] + " ");
            }
            
        }
    }
}
