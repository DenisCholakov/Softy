using System;
using System.Linq;
using System.Collections.Generic;

namespace SnowWite
{
    class Program
    {
        static void Main(string[] args)
        {
            var dwarfs = new Dictionary<string, int>();
            string input = Console.ReadLine();

            while (input != "Once upon a time")
            {
                string[] dwarf = input.Split(" <::> ");
                string nameColor = $"({dwarf[1]}) {dwarf[0]}";
                int physics = int.Parse(dwarf[2]);
                if (dwarfs.ContainsKey(nameColor))
                {
                    if (dwarfs[nameColor] < physics)
                    {
                        dwarfs[nameColor] = physics;
                    }
                }
                else
                {
                    dwarfs.Add(nameColor, physics);
                }
                input = Console.ReadLine();
            }

            foreach (var pair in dwarfs)
            {
                System.Console.WriteLine($"{pair.Key} <-> {pair.Value}");
            }
        }
    }
}
