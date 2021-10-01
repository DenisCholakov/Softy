using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

public class SumOfCoins
{
    public static void Main(string[] args)
    {
        var availableCoins = new[] { 1, 2, 5, 10, 20, 50 };
        var targetSum = 923;
        try
        {
            var selectedCoins = ChooseCoins(availableCoins, targetSum);

            Console.WriteLine($"Number of coins to take: {selectedCoins.Values.Sum()}");
            foreach (var selectedCoin in selectedCoins)
            {
                Console.WriteLine($"{selectedCoin.Value} coin(s) with value {selectedCoin.Key}");
            }
        }
        catch (IndexOutOfRangeException e)
        {
            Console.WriteLine(e.Message);
        }
    }

    public static Dictionary<int, int> ChooseCoins(IList<int> coins, int targetSum)
    {
        List<int>Sortedcoins = coins.OrderByDescending(x => x).ToList();
        Dictionary<int, int> coinsNeeded = new Dictionary<int, int>();
        int sum = 0;
        int index = 0;
        int currentSum = targetSum;

        while (sum != targetSum)
        {
            if (index >= coins.Count)
            {
                throw new InvalidOperationException("Error");
            }

            int currCoin = Sortedcoins[index];

            if (sum + currCoin <= targetSum)
            {
                
                int count = currentSum / currCoin;
                coinsNeeded.Add(currCoin, count);
                sum += (currCoin * count);
                currentSum -= (currCoin * count);
            }
            else
            {
                index++;
            }
        }

        return coinsNeeded;
    }
}