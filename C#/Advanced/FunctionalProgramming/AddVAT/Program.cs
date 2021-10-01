using System;
using System.Linq;

namespace AddVAT
{
    class Program
    {
        static void Main(string[] args)
        {
            double[] nums = Console.ReadLine().Split(", ").Select(double.Parse)
                        .Select(x => x * 1.2).ToArray();

            foreach (var num in nums)
            {
                Console.WriteLine($"{num:f2}");
            }
        }
    }
}
