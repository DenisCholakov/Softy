using System;
using System.Linq;

namespace FunctionalProgramming
{
    class EvenNumbers
    {
        static void Main(string[] args)
        {
            int[] nums = Console.ReadLine().Split(", ").Select(x => int.Parse(x))
                        .Where(x => x % 2 == 0).OrderBy(x => x).ToArray();

            Console.WriteLine(String.Join(", ", nums));
        }
    }
}
