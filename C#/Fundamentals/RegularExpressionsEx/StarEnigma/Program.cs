using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;

namespace StarEnigma
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> attackedPlanets = new List<string>();
            List<string> destroyedPlanets = new List<string>();
            Regex rx = new Regex(@"[star]", RegexOptions.IgnoreCase);
            string pattern = @"[^@!:>-]*?@(?<name>[A-Z][a-z]+)[^@!:>-]*?:(?<power>\d+)[^@!:-]*?!(?<type>[AD])![^@!:-]*?->(?<soldierCount>\d+)[^@!:>-]*";
            int n = int.Parse(Console.ReadLine());

            for (int i = 0; i < n; i++)
            {
                string input = Console.ReadLine();
                int decrypter = rx.Matches(input).Count;
                string message = String.Empty;
                foreach (char ch in input)
                {
                    message += (char)(ch - decrypter);
                }

                var planet = Regex.Match(message, pattern);

                if (planet.Success)
                {
                    string name = planet.Groups["name"].Value;
                    int power = int.Parse(planet.Groups["power"].Value);
                    string type = planet.Groups["type"].Value;
                    int soldiers = int.Parse(planet.Groups["soldierCount"].Value);

                    if (type == "A")
                    {
                        attackedPlanets.Add(name);
                    }
                    else if (type == "D")
                    {
                        destroyedPlanets.Add(name);
                    }
                }
            }

            System.Console.WriteLine($"Attacked planets: {attackedPlanets.Count}");

            foreach (var planet in attackedPlanets.OrderBy(x => x))
            {
                System.Console.WriteLine($"-> {planet}");
            }

            System.Console.WriteLine($"Destroyed planets: {destroyedPlanets.Count}");

            foreach (var planet in destroyedPlanets.OrderBy(x => x))
            {
                System.Console.WriteLine($"-> {planet}");
            }
        }
    }
}
