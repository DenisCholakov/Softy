using System;
using System.Linq;

namespace CustomComparator
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] nums = Console.ReadLine().Split().Select(int.Parse).ToArray();
            Array.Sort(nums, (x, y) =>
                x % 2 == 0 && y % 2 != 0 ? -1 :
                x % 2 != 0 && y % 2 == 0 ? 1 : x - y);

            Console.WriteLine(String.Join(' ', nums));
        }
    }
}
