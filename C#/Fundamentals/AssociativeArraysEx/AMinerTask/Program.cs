using System;
using System.Collections.Generic;

namespace AMinerTask
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, int> resources = new Dictionary<string, int>();

            string input = Console.ReadLine();
            while (input != "stop")
            {
                string resource = input;
                int quantity = int.Parse(Console.ReadLine());
                if (resources.ContainsKey(resource))
                {
                    resources[resource] += quantity;
                }
                else
                {
                    resources.Add(resource, quantity);
                }
                input = Console.ReadLine();
            }

            foreach (var pair in resources)
            {
                System.Console.WriteLine($"{pair.Key} -> {pair.Value}");
            }
        }
    }
}
