using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DividingPresents
{
    class Program
    {
        private static Dictionary<int, int> sums;
        static void Main(string[] args)
        {
            int[] presents = Console.ReadLine().Split().Select(int.Parse).ToArray();
            sums = new Dictionary<int, int>();

            sums = GetPossibleSums(presents);

            int presentsSum = presents.Sum();
            int target = presentsSum / 2;

            while (!sums.ContainsKey(target))
            {
                target--;
            }

            int alanPresentsSum = target;
            int bobPresentsSum = presentsSum - target;
            int difference = bobPresentsSum - alanPresentsSum;


            var alanPresents = GetAlanPresents(presents, target);
            

            Console.WriteLine($"Difference: {difference}");
            Console.WriteLine($"Alan:{alanPresentsSum} Bob:{bobPresentsSum}");
            Console.WriteLine($"Alan takes: {String.Join(' ', alanPresents)}");
            Console.WriteLine("Bob takes the rest.");
        }

        private static List<int> GetAlanPresents(int[] presents, int target)
        {
            var result = new List<int>();

            while (target > 0)
            {
                result.Add(sums[target]);
                target -= sums[target];
            }

            return result;
        }

        private static Dictionary<int, int> GetPossibleSums(int[] presents)
        {
            var result = new Dictionary<int, int> { { 0, 0 } };

            foreach (var value in presents)
            {
                var sums = result.Keys.ToArray();

                foreach (var sum in sums)
                {
                    var newSum = sum + value;

                    if (!result.ContainsKey(newSum))
                    {
                        result.Add(newSum, value);
                    }
                }
            }

            return result.OrderBy(kvp => kvp.Key).ToDictionary(x => x.Key, x => x.Value);
        }
    }
}
