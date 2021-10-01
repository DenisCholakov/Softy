using System;
using System.Collections.Generic;
using System.Linq;

namespace PeriodicTable
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            var elements = new HashSet<string>();

            // SortedSet

            for (int i = 0; i < n; i++)
            {
                var input = Console.ReadLine().Split();

                foreach (var element in input)
                {
                    elements.Add(element);
                }
            }

            Console.WriteLine(String.Join(' ', elements.OrderBy(x => x)));
        }
    }
}
