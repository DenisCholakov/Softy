using System;
using System.IO;
using System.Linq;

namespace ImplementQuickSort
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] nums = Console.ReadLine().Split().Select(int.Parse).ToArray();

            QuickSort(nums, 0, nums.Length);
        }

        private static void QuickSort(int[] nums, int start, int end)
        {
            int pivot = (start + end) / 2;

            

        }
    }
}
