using System;
using System.Collections.Generic;
using System.Linq;

namespace PrintEvenNumbers
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] numbers = Console.ReadLine().Split().Select(int.Parse).ToArray();
            Queue<int> queue = new Queue<int>(numbers);
            List<int> result = new List<int>();

            while (queue.Count != 0)
            {
                int currentNum = queue.Dequeue();
                if (currentNum % 2 == 0)
                {
                    result.Add(currentNum);
                }
            }

            Console.WriteLine(String.Join(", ", result));
        }
    }
}
