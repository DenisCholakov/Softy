using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace GaussTrick
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> nums = Console.ReadLine()
                .Split()
                .Select(int.Parse)
                .ToList();

            int count = nums.Count / 2;
            for (int i = 0; i < count; i++)
            {
                nums[i] += nums[nums.Count - 1];
                nums.RemoveAt(nums.Count - 1);
            }

            Console.WriteLine(String.Join(' ', nums));
        }
    }
}
