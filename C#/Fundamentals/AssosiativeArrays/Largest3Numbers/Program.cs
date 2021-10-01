using System;
using System.Collections.Generic;
using System.Linq;

namespace Largest3Numbers
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> nums = Console.ReadLine().Split().Select(int.Parse).ToList();

            System.Console.WriteLine(String.Join(' ', nums.OrderByDescending(x => x).Take(3)));
        }
    }
}
