using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Race
{
    class Program
    {
        static void Main(string[] args)
        {

            Dictionary<string, int> racers = new Dictionary<string, int>();
            string input = Console.ReadLine();
            string[] competetors = input.Split(", ");

            input = Console.ReadLine();

            foreach (var competetor in competetors)
            {
                racers.Add(competetor, 0);
            }

            while (input != "end of race")
            {
                var nameLetters = Regex.Matches(input, @"[A-Za-z]");
                string name = String.Concat(nameLetters);

                var kilometers = Regex.Matches(input, @"\d");
                int km = kilometers.Select(x => int.Parse(x.Value)).Sum();

                if (racers.ContainsKey(name))
                {
                    racers[name] += km;
                }

                input = Console.ReadLine();
            }

            List<string> topRacers = racers.OrderByDescending(x => x.Value).Select(x => x.Key).Take(3).ToList();

            Console.WriteLine($"1st place: {topRacers[0]}");
            Console.WriteLine($"2nd place: {topRacers[1]}");
            Console.WriteLine($"3rd place: {topRacers[2]}");

        }
    }
}
