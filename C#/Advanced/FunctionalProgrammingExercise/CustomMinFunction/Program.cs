using System;
using System.IO;
using System.Linq;

namespace CustomMinFunction
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] nums = Console.ReadLine().Split().Select(int.Parse).ToArray();

            Func<int[], int> minValue = x =>
            {
                int minValue = int.MaxValue;

                for (int i = 0; i < x.Length; i++)
                {
                    if (x[i] < minValue)
                    {
                        minValue = x[i];
                    }
                }

                return minValue;
            };

            Console.WriteLine(minValue(nums));
        }
    }
}
