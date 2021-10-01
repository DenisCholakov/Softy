using System;
using System.Linq;
using System.Collections.Generic;

namespace ForceBook
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, string> forceUsers = new Dictionary<string, string>();
            string input = Console.ReadLine();

            while (input != "Lumpawaroo")
            {
                if (input.Contains('|'))
                {
                    string side = input.Split(" | ")[0];
                    string user = input.Split(" | ")[1];
                    if (!forceUsers.ContainsKey(user))
                    {
                        forceUsers.Add(user, side);
                    }
                }
                else
                {
                    string user = input.Split(" -> ")[0];
                    string side = input.Split(" -> ")[1];
                    if (forceUsers.ContainsKey(user))
                    {
                        forceUsers[user] = side;
                    }
                    else
                    {
                        forceUsers.Add(user, side);
                    }
                    System.Console.WriteLine($"{user} joins the {side} side!");
                }
                input = Console.ReadLine();
            }

            Dictionary<string, List<string>> sides = new Dictionary<string, List<string>>();

            foreach (var pair in forceUsers)
            {
                if (sides.ContainsKey(pair.Value))
                {
                    sides[pair.Value].Add(pair.Key);
                }
                else
                {
                    sides.Add(pair.Value, new List<string>() { pair.Key });
                }
            }

            foreach (var pair in sides.OrderByDescending(x => x.Value.Count).ThenBy(x => x.Key))
            {
                System.Console.WriteLine($"Side: {pair.Key}, Members: {pair.Value.Count}");
                pair.Value.Sort();
                foreach (var user in pair.Value)
                {
                    System.Console.WriteLine($"! {user}");
                }
            }

        }
    }
}
