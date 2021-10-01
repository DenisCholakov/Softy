using System;
using System.Collections.Generic;
using System.Linq;

namespace RodCutting
{
    class Program
    {
        private static int[] bestPrices;
        private static int[] prev;
        static void Main(string[] args)
        {
            var prices = Console.ReadLine().Split().Select(int.Parse).ToArray();
            int length = int.Parse(Console.ReadLine());
            bestPrices = new int[length + 1];
            prev = new int[length + 1];

            for (int i = 0; i <= length; i++)
            {
                bestPrices[i] = -1;
                prev[i] = i;
            }

            FindBestPrice(length, prices);

            Console.WriteLine(bestPrices[length]);

            var stack = new Stack<int>();
            var node = length;

            while (node != 0)
            {
                stack.Push(prev[node]); 
                node -= prev[node];
            }

            Console.WriteLine(String.Join(' ', stack));

        }

        private static int FindBestPrice(int length, int[] prices)
        {
            if (length < 1)
            {
                return prices[length];
            }

            if (bestPrices[length] >= 0)
            {
                return bestPrices[length];
            }

            var bestPrice = prices[length]; 

            for (int i = 1; i <= length / 2; i++)
            {
                var price = FindBestPrice(i, prices) + FindBestPrice(length - i, prices);

                if (price > bestPrice)
                {
                    prev[length] = length - i;
                    bestPrice = price;
                }
            }

            bestPrices[length] = bestPrice;
            return bestPrice;
        }
    }
}
