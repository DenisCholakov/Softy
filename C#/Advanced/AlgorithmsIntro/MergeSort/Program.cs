using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MergeSort
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] nums = Console.ReadLine().Split().Select(int.Parse).ToArray();

            Console.WriteLine(String.Join(' ', MergeSort(nums.ToList())));


        }

        private static List<int> MergeSort(List<int> nums)
        {
            if (nums.Count == 1)
            {
                return nums;
            }

            int middleIndex = nums.Count / 2;
            List<int> leftList = MergeSort(nums.GetRange(0, middleIndex));
            List<int> rightList = MergeSort(nums.GetRange(middleIndex, nums.Count - middleIndex));

            return Combine(leftList, rightList);
        }

        private static List<int> Combine(List<int> left, List<int> right)
        {
            List<int> result = new List<int>();
            int leftIndex = 0;
            int rightIndex = 0;

            while (leftIndex < left.Count && rightIndex < right.Count)
            {
                if (left[leftIndex] < right[rightIndex])
                {
                    result.Add(left[leftIndex]);
                    leftIndex++;
                }
                else
                {
                    result.Add(right[rightIndex]);
                    rightIndex++;
                }
            }

            for (int i = leftIndex; i < left.Count; i++)
            {
                result.Add(left[i]);
            }

            for (int i = rightIndex; i < right.Count; i++)
            {
                result.Add(right[i]);
            }

            return result;
        }
    }
}
