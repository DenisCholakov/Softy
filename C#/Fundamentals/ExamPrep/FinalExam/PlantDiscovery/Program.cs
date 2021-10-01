using System;
using System.Linq;
using System.Collections.Generic;

namespace PlantDiscovery
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, Plant> plants = new Dictionary<string, Plant>();
            int n = int.Parse(Console.ReadLine());

            for (int i = 0; i < n; i++)
            {
                string[] input1 = Console.ReadLine().Split("<->");
                string name = input1[0];
                int rarity = int.Parse(input1[1]);

                if (plants.ContainsKey(name))
                {
                    plants[name].Rarity = rarity;
                }
                else
                {
                    plants.Add(name, new Plant(name, rarity));
                }
            }

            string[] input2 = Console.ReadLine().Split(new char[] { ':', '-', ' ' }, StringSplitOptions.RemoveEmptyEntries);

            while (input2[0] != "Exhibition")
            {
                string plant = input2[1];

                if (input2[0] == "Rate")
                {
                    double rate = double.Parse(input2[2]);
                    if (plants.ContainsKey(plant))
                    {
                        plants[plant].Ratings.Add(rate);
                    }
                    else
                    {
                        System.Console.WriteLine("error");
                    }
                }
                else if (input2[0] == "Update")
                {
                    int newRarity = int.Parse(input2[2]);
                    if (plants.ContainsKey(plant))
                    {
                        plants[plant].Rarity = newRarity;
                    }
                    else
                    {
                        System.Console.WriteLine("error");
                    }
                }
                else if (input2[0] == "Reset")
                {
                    if (plants.ContainsKey(plant))
                    {
                        plants[plant].Ratings.Clear();
                    }
                    else
                    {
                        System.Console.WriteLine("error");
                    }
                }
                else
                {
                    System.Console.WriteLine("error");
                }

                input2 = Console.ReadLine().Split(new char[] { ':', '-', ' ' }, StringSplitOptions.RemoveEmptyEntries);
            }

            foreach (var pair in plants)
            {
                if (pair.Value.Ratings.Count == 0)
                {
                    pair.Value.Ratings.Add(0);
                }
            }

            System.Console.WriteLine("Plants for the exhibition:");
            foreach (var pair in plants.OrderByDescending(x => x.Value.Rarity).ThenByDescending(x => x.Value.Ratings.Average()))
            {
                System.Console.WriteLine($"- {pair.Key}; Rarity: {pair.Value.Rarity}; Rating: {pair.Value.Ratings.Average():f2}");
            }
        }
    }

    public class Plant
    {
        public string Name { get; set; }
        public int Rarity { get; set; }
        public List<double> Ratings { get; set; }

        public Plant(string name, int rarity)
        {
            this.Name = name;
            this.Rarity = rarity;
            this.Ratings = new List<double>();
        }
    }
}
