using System;
using System.IO;
using System.Linq;

namespace ArraySumRecursive
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] nums = Console.ReadLine().Split().Select(int.Parse).ToArray();

            Console.WriteLine(RecursiveSum(nums, 0));
        }

        public static int RecursiveSum(int[] arr, int index)
        {
            if (index == arr.Length)
            {
                return 0;
            }

            return arr[index] + RecursiveSum(arr, index + 1);
        }
    }
}
