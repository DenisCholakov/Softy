using System;
using System.Collections.Generic;
using System.Linq;

namespace CableMerchant
{
    class Program
    {
        private static int[] bestPrices;

        static void Main(string[] args)
        {
            var prices = new List<int> { 0 };
            prices.AddRange(Console.ReadLine().Split().Select(int.Parse).ToArray());
            int connectorPrice = int.Parse(Console.ReadLine());
            bestPrices = new int[prices.Count];


            for (int length = 1; length < prices.Count; length++)
            {
                bestPrices[length] = CutCable(length, prices, connectorPrice);
            }

            Console.WriteLine(String.Join(' ', bestPrices.Skip(1)));
        }

        private static int CutCable(int length, List<int> prices, int connectorPrice)
        {
            if (length <= 1)
            {
                return prices[length];
            }

            if (bestPrices[length] != 0)
            {
                return bestPrices[length];
            }

            bestPrices[length] = prices[length];

            for (int i = 1; i <= length/2; i++)
            {
                var currentPrice = prices[i] + CutCable(length - i, prices, connectorPrice) - 2 * connectorPrice;

                if (currentPrice > bestPrices[length])
                {
                    bestPrices[length] = currentPrice;
                }
            }

            return bestPrices[length];
        }
    }
}
