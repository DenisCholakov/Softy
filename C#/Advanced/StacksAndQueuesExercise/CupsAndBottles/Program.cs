using System;
using System.Collections.Generic;
using System.Linq;

namespace CupsAndBottles
{
    class Program
    {
        static void Main(string[] args)
        {
            Queue<int> cups = new Queue<int>(Console.ReadLine().Split().Select(int.Parse));
            Stack<int> bottles = new Stack<int>(Console.ReadLine().Split().Select(int.Parse));
            int wastedWater = 0;

            while (cups.Count != 0 && bottles.Count != 0)
            {
                int currentBottle = bottles.Pop();
                int currentCup = cups.Dequeue();

                if (currentBottle >= currentCup)
                {
                    wastedWater += currentBottle - currentCup;
                }
                else
                {
                    while (true)
                    {
                        currentCup -= currentBottle;

                        if (currentCup <= 0)
                        {
                            wastedWater -= currentCup;
                            break;
                        }

                        currentBottle = bottles.Pop();
                    }
                }
            }

            if (bottles.Count != 0)
            {
                Console.WriteLine($"Bottles: {String.Join(' ', bottles)}");
            }
            else if (cups.Count != 0)
            {
                Console.WriteLine($"Cups: {String.Join(' ', cups)}");
            }

            Console.WriteLine($"Wasted litters of water: {wastedWater}");
        }
    }
}
