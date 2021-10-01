using System;
using System.Collections.Generic;

namespace _7.VendingMachine
{
    class Program
    {
        static void Main(string[] args)
        {
            double[] currency = new double[] { 0.1, 0.2, 0.5, 1, 2 };
            string input = Console.ReadLine();

            double money = 0;
            while (input != "Start")
            {
                double num = double.Parse(input);
                if (Array.IndexOf(currency, num) >= 0)
                {
                    money += num;
                }
                else
                {
                    Console.WriteLine($"Cannot accept {num}");
                }

                input = Console.ReadLine();
            }

            input = Console.ReadLine();

            while (input != "End")
            {
                double price = 0;
                switch (input)
                {
                    case "Nuts": price = 2; break;
                    case "Water": price = 0.7; break;
                    case "Crisps": price = 1.5; break;
                    case "Soda": price = 0.8; break;
                    case "Coke": price = 1.0; break;
                    default: Console.WriteLine("Invalid product"); input = Console.ReadLine(); continue; 
                }

                if (money >= price)
                {
                    Console.WriteLine($"Purchased {input.ToLower()}");
                    money -= price;
                }
                else
                {
                    Console.WriteLine("Sorry, not enough money");
                }

                input = Console.ReadLine();
            }

            Console.WriteLine($"Change: {money:f2}");
        }
    }
}
