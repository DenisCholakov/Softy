using System;
using System.Collections.Generic;
using System.Linq;

namespace FashionBoutique
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] pileOfClothes = Console.ReadLine().Split().Select(int.Parse).ToArray();
            int rackCapacity = int.Parse(Console.ReadLine());
            Stack<int> stack = new Stack<int>(pileOfClothes);

            int rackCounter = 0;
            int sum = 0;

            while (stack.Count > 0)
            {
                int pile = stack.Pop();
                sum += pile;

                if (sum == rackCapacity)
                {
                    rackCounter++;
                    sum = 0;
                }
                else if(sum > rackCapacity)
                {
                    rackCounter++;
                    sum = pile;
                }

            }

            if (sum > 0)
            {
                rackCounter++;
            }

            Console.WriteLine(rackCounter);
        }
    }
}
