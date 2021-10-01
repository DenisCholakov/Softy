using System;
using System.Linq;

namespace SumNumbers
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] nums = Console.ReadLine().Split(", ", StringSplitOptions.RemoveEmptyEntries)
                        .Select(int.Parse).ToArray();

            Func<int[], int> count = x => x.Length;
            Func<int[], int> sum = x => x.Sum();

            Console.WriteLine(count(nums));
            Console.WriteLine(sum(nums));
        }
    }
}
