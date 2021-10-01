using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ListOfPredicates
{
    class Program
    {
        static void Main(string[] args)
        {
            int upperBound = int.Parse(Console.ReadLine());
            int[] dividers = Console.ReadLine().Split().Select(int.Parse).ToArray();

            Func<int, int[], bool> dividablePredicate = (x, arr) =>
            {
                for (int i = 0; i < arr.Length; i++)
                {
                    if (x % arr[i] != 0)
                    {
                        return false;
                    }
                }

                return true;
            };

            Func<int, Func<int, int[], bool>, int[], List<int>> dividableNumbers = (x, func, arr) =>
            {
                List<int> result = new List<int>();

                for (int i = 1; i <= x; i++)
                {
                    if (func(i, arr))
                    {
                        result.Add(i);
                    }
                }

                return result;
            };

            Action<List<int>> printer = x => Console.WriteLine(String.Join(' ', x));

            printer(dividableNumbers(upperBound, dividablePredicate, dividers));
        }
    }
}
