using System;
using System.Collections.Generic;
using System.Linq;

namespace _03.PlantDiscovery
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            Dictionary<string, Dictionary<string, List<double>>> plants = new Dictionary<string, Dictionary<string, List<double>>>();

            for (int i = 0; i < n; i++)
            {
                string[] input = Console.ReadLine().Split("<->");
                string name = input[0];
                double rarity = double.Parse(input[1]);
                plants.Add(name, new Dictionary<string, List<double>>());
                plants[name].Add("rarity", new List<double>());
                plants[name].Add("rate", new List<double>());
                plants[name]["rarity"].Add(rarity);
            }

            string[] command = Console.ReadLine().Split(new char[] { ' ', ':', '-' }, StringSplitOptions.RemoveEmptyEntries);

            while (command[0] != "Exhibition")
            {
                string name = command[1];
                if (command[0] == "Rate")
                {
                    if (plants.ContainsKey(name))
                    {
                        double rate = double.Parse(command[2]);
                        if (plants[name].ContainsKey("rate"))
                        {
                            plants[name]["rate"].Add(rate);
                        }
                        else
                        {
                            plants[name].Add("rate", new List<double>() { rate });
                        }
                    }
                    else
                    {
                        System.Console.WriteLine("error");
                    }
                }
                else if (command[0] == "Update")
                {
                    double rarity = double.Parse(command[2]);
                    if (plants.ContainsKey(name))
                    {
                        plants[name]["rarity"].Add(rarity);
                        plants[name]["rarity"].RemoveAt(0);
                    }
                    else
                    {
                        System.Console.WriteLine("error");
                    }
                }
                else if (command[0] == "Reset")
                {
                    if (plants.ContainsKey(name))
                    {
                        if (plants[name].ContainsKey("rate"))
                        {
                            plants[name]["rate"] = new List<double>();
                        }
                    }
                    else
                    {
                        System.Console.WriteLine("erroe");
                    }
                }
                else
                {
                    System.Console.WriteLine("error");
                }
                command = Console.ReadLine().Split(new char[] { ' ', ':', '-' }, StringSplitOptions.RemoveEmptyEntries);
            }

            foreach (var pair in plants)
            {
                if (pair.Value["rate"].Count == 0)
                {
                    pair.Value["rate"].Add(0);
                }
            }
            System.Console.WriteLine("Plants for the exhibition:");
            foreach (var pair in plants.OrderByDescending(x => x.Value["rarity"][0]).ThenByDescending(x => x.Value["rate"].Average()))
            {
                System.Console.WriteLine($"{pair.Key}; Rarity: {pair.Value["rarity"][0]}; Rating: {pair.Value["rate"].Average():f2}");
            }
        }
    }
}