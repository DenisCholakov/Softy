using System;
using System.Collections.Generic;
using System.Linq;

namespace FastFood
{
    class Program
    {
        static void Main(string[] args)
        {
            int preparedFood = int.Parse(Console.ReadLine());
            Queue<int> queue = new Queue<int>(Console.ReadLine().Split().Select(int.Parse).ToArray());
            Console.WriteLine(queue.Max());

            while (queue.Count > 0)
            {
                int currentOrder = queue.Peek();

                if (currentOrder <= preparedFood)
                {
                    preparedFood -= currentOrder;
                    queue.Dequeue();
                }
                else
                {
                    Console.WriteLine("Orders left: " + String.Join(' ', queue));
                    break;
                }
            }

            if (queue.Count == 0)
            {
                Console.WriteLine("Orders complete");
            }
        }
    }
}
