using System;
using System.IO;
using System.Linq;

namespace AppliedArithmetics
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] nums = Console.ReadLine().Split().Select(int.Parse).ToArray();
            string input = Console.ReadLine();
            Action<int[]> printer = x => Console.WriteLine(String.Join(' ', x));

            while (input != "end")
            {
                switch (input)
                {
                    case "add": nums = nums.Select(x => x + 1).ToArray();
                        break;
                    case "multiply": nums = nums.Select(x => x * 2).ToArray();
                        break;
                    case "subtract": nums = nums.Select(x => x - 1).ToArray();
                        break;
                    case "print": printer(nums);
                        break;
                }

                input = Console.ReadLine();
            }
        }
    }
}
