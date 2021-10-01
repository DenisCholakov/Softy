using System;
using System.IO.IsolatedStorage;
using System.Linq;

namespace FoldAndSum
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] nums = Console.ReadLine().Split().Select(int.Parse).ToArray();
            int n = nums.Length / 4;
            int[] nums1 = new int[n*2];
            int pos = 0;
            for (int i = n-1; i >= 0; i--)
            {
                nums1[pos++] = nums[i];
            }

            pos = n;
            for (int i = nums.Length - 1; i >= n*3; i--)
            {
                nums1[pos++] = nums[i];
            }

            int[] nums2 = new int[n * 2];
            Array.Copy(nums, n, nums2, 0, n*2);

            for (int i = 0; i < n*2; i++)
            {
                nums1[i] += nums2[i];
            }

            Console.WriteLine(String.Join(' ', nums1));
        }
    }
}
