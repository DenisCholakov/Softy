using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SumWithUnlimitedAmountOfCoins
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] coins = Console.ReadLine().Split().Select(int.Parse).ToArray();
            int targetSum = int.Parse(Console.ReadLine());
            int[] combCount = new int[targetSum + 1];

            foreach (var value in coins)
            {
                combCount[value]++;

                for (int i = value; i < combCount.Length; i++)
                {
                    if (combCount[i - value] != 0)
                    {
                        combCount[i] = combCount[i] + combCount[i - value];
                    }                  
                }
            }

            Console.WriteLine(combCount[targetSum]);
        }
    }
}
