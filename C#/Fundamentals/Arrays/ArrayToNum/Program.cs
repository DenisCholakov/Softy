using System;
using System.Linq;

namespace ArrayToNum
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] nums = Console.ReadLine().Split().Select(int.Parse).ToArray();

            while (nums.Length != 1)
            {
                int[] tempArr = new int[nums.Length-1];

                for (int i = 0; i < tempArr.Length; i++)
                {
                    tempArr[i] = nums[i] + nums[i + 1];
                }

                nums = tempArr;
            }

            Console.WriteLine(nums[0]);
        }
    }
}
