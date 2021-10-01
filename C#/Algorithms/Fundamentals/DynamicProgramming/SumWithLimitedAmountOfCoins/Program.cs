using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SumWithLimitedAmountOfCoins
{
    class Program
    {
        private static Dictionary<int, int> sumsCount;
        static void Main(string[] args)
        {
            int[] coins = Console.ReadLine().Split().Select(int.Parse).ToArray();
            int targetSum = int.Parse(Console.ReadLine());
            sumsCount = new Dictionary<int, int> { { 0, 1 } };

            GetAllSumsCount(coins);

            Console.WriteLine(sumsCount[targetSum]);
        }

        private static void GetAllSumsCount(int[] coins)
        {
            foreach (var value in coins)
            {
                var sums = sumsCount.Keys.ToArray();

                foreach (var sum in sums)
                {
                    var newSum = sum + value;

                    if (!sumsCount.ContainsKey(newSum))
                    {
                        sumsCount.Add(newSum, 0);
                    }

                    sumsCount[newSum]++;
                }
            }
        }
    }
}
