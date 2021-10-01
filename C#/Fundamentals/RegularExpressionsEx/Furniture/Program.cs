using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace Furniture
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> furnitures = new List<string>();
            string input = Console.ReadLine();
            Regex pattern = new Regex(@">>(?<furniture>[A-Za-z\s]+)<<(?<price>\d+\.?\d*)!(?<quantity>\d+)");
            double moneySpent = 0;

            while (input != "Purchase")
            {
                Match validInput = pattern.Match(input);
                if (validInput.Success)
                {
                    furnitures.Add(validInput.Groups["furniture"].Value);
                    moneySpent += (double.Parse(validInput.Groups["price"].Value) * int.Parse(validInput.Groups["quantity"].Value));
                }
                input = Console.ReadLine();
            }

            System.Console.WriteLine("Bought furniture:");

            if (furnitures.Count > 0)
            {
                System.Console.WriteLine(String.Join(Environment.NewLine, furnitures));
            }

            System.Console.WriteLine($"Total money spend: {moneySpent:f2}");
        }
    }
}
