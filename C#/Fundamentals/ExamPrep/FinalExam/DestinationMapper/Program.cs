using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace DestinationMapper
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> destinations = new List<string>();
            string placesPattern = @"([=\/])[A-Z][A-Za-z]{2,}(\1)";
            string input = Console.ReadLine();

            MatchCollection places = Regex.Matches(input, placesPattern);

            int points = 0;
            foreach (Match place in places)
            {
                destinations.Add(place.Value.Trim(new char[] { '=', '/' }));
                points += (place.Value.Length - 2);
            }

            System.Console.Write("Destinations:");

            if (destinations.Count == 0)
            {
                System.Console.WriteLine();
            }
            else
            {
                System.Console.WriteLine(" " + String.Join(", ", destinations));
            }

            System.Console.WriteLine($"Travel Points: {points}");

        }
    }
}
