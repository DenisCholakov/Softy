using System;
using System.Collections.Generic;
using System.Linq;

namespace FindEvenOrOdds
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] bounds = Console.ReadLine().Split().Select(int.Parse).ToArray();
            string needed = Console.ReadLine();

            Predicate<int> evenOrOdd = x => x % 2 == 0;

            if (needed == "odd")
            {
                evenOrOdd = x => x % 2 != 0;
            }

            Func<int[], Predicate<int>, List<int>> findNumbers = (x, func) =>
            {
                List<int> result = new List<int>();

                for (int i = x[0]; i <= x[1]; i++)
                {
                    if (func(i))
                    {
                        result.Add(i);
                    }
                }

                return result;
            };
            Console.WriteLine(String.Join(' ', findNumbers(bounds, evenOrOdd)));
        }
    }
}
