using System;
using System.Linq;
using System.Collections.Generic;

namespace LegendaryMining
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, int> keyMaterials = new Dictionary<string, int>();
            keyMaterials.Add("shards", 0);
            keyMaterials.Add("fragments", 0);
            keyMaterials.Add("motes", 0);
            Dictionary<string, int> junk = new Dictionary<string, int>();
            while (true)
            {
                string[] input = Console.ReadLine().Split();

                for (int i = 1; i < input.Length; i += 2)
                {
                    int qty = int.Parse(input[i - 1]);
                    string material = input[i].ToLower();

                    if (keyMaterials.ContainsKey(material))
                    {
                        keyMaterials[material] += qty;
                        if (keyMaterials[material] >= 250)
                        {
                            keyMaterials[material] -= 250;
                            PrintMaterials(material, keyMaterials, junk);
                            return;
                        }
                    }
                    else
                    {
                        if (junk.ContainsKey(material))
                        {
                            junk[material] += qty;
                        }
                        else
                        {
                            junk.Add(material, qty);
                        }
                    }

                }
            }
        }

        private static void PrintMaterials(string mat, Dictionary<string, int> keyMat, Dictionary<string, int> junk)
        {
            if (mat == "shards")
            {
                System.Console.WriteLine("Shadowmourne obtained!");
            }
            else if (mat == "fragments")
            {
                System.Console.WriteLine("Valanyr obtained!");
            }
            else
            {
                System.Console.WriteLine("Dragonwrath obtained!");
            }
            keyMat = keyMat.OrderByDescending(x => x.Value).ThenBy(x => x.Key)
                            .ToDictionary(pair => pair.Key, pair => pair.Value);

            junk = junk.OrderBy(x => x.Key).ToDictionary(pair => pair.Key, pair => pair.Value);

            foreach (var pair in keyMat)
            {
                System.Console.WriteLine($"{pair.Key}: {pair.Value}");
            }

            foreach (var pair in junk)
            {
                System.Console.WriteLine($"{pair.Key}: {pair.Value}");
            }
        }
    }
}
