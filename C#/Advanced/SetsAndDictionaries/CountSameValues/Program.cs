using System;
using System.Linq;
using System.Collections.Generic;

namespace CountSameValues
{
    class Program
    {
        static void Main(string[] args)
        {
            double[] nums = Console.ReadLine().Split().Select(double.Parse).ToArray();
            Dictionary<double, int> count = new Dictionary<double, int>();

            foreach (var num in nums)
            {
                if (!count.ContainsKey(num))
                {
                    count.Add(num, 0);
                }

                count[num]++;
            }

            foreach (var pair in count)
            {
                Console.WriteLine($"{pair.Key} - {pair.Value} times");
            }
        }
    }
}
