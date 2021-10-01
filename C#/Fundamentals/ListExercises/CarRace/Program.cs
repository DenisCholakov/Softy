using System;
using System.Collections.Generic;
using System.Linq;

namespace CarRace
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> steps = Console.ReadLine().Split().Select(int.Parse).ToList();

            int middle = steps.Count / 2;

            double sum1 = 0;
            for (int i = 0; i < middle; i++)
            {
                sum1 += steps[i];
                if (steps[i] == 0)
                {
                    sum1 *= 0.8;
                }
            }

            double sum2 = 0;
            for (int i = steps.Count - 1; i > middle; i--)
            {
                sum2 += steps[i];
                if (steps[i] == 0)
                {
                    sum2 *= 0.8;
                }
            }

            if (sum1 < sum2)
            {
                Console.WriteLine($"The winner is left with total time: {sum1}");
            }
            else
            {
                Console.WriteLine($"The winner is right with total time: {sum2}");
            }
        }
    }
}
