using System;
using System.Collections.Generic;
using System.Linq;

namespace BombNumbers
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> nums = Console.ReadLine().Split().Select(int.Parse).ToList();
            int[] bomb = Console.ReadLine().Split().Select(int.Parse).ToArray(); // bomb number and bomb power

            int index = nums.IndexOf(bomb[0]);
            while (index >= 0)
            {
                nums.RemoveAt(index);

                int last = index + bomb[1] > nums.Count - 1 ? nums.Count - index : bomb[1];
                for (int i = 0; i < last; i++)
                {
                    nums.RemoveAt(index);
                }

                int first = index - bomb[1] < 0 ? index : bomb[1];
                for (int i = 1; i <= first; i++)
                {
                    nums.RemoveAt(index - i);
                }
                index = nums.IndexOf(bomb[0]);
            }

            Console.WriteLine(nums.Sum());
        }
    }
}
