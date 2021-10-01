using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ReverseAndExclude
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> nums = Console.ReadLine().Split().Select(int.Parse).ToList();
            int n = int.Parse(Console.ReadLine());

            Func<int, int, bool> isDividable = (x, y) => x % y == 0;

            Func<List<int>, Func<int, int, bool>, int, List<int>> nonDividable = (x, func, n) =>
            {
                List<int> result = new List<int>(x);
                result.Reverse();

                foreach (var num in x)
                {
                    if (func(num, n))
                    {
                        result.Remove(num);
                    }
                }

                return result;
            };

            Action<List<int>> printer = x => Console.WriteLine(String.Join(' ', x));

            printer(nonDividable(nums, isDividable, n));
        }
    }
}
