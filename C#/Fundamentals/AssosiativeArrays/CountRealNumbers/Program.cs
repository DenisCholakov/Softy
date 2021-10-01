using System;
using System.Collections.Generic;
using System.Linq;

namespace CountRealNumbers
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] nums = Console.ReadLine().Split().Select(int.Parse).ToArray();

            SortedDictionary<int, int> occurances = new SortedDictionary<int, int>();

            foreach (int num in nums)
            {
                if (!occurances.ContainsKey(num))
                {
                    occurances.Add(num, 1);
                }
                else
                {
                    occurances[num]++;
                }
            }

            foreach (var occ in occurances)
            {
                System.Console.WriteLine($"{occ.Key} -> {occ.Value}");
            }
        }
    }
}
