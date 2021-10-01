using System;

namespace Orders
{
    class Program
    {
        static void Main(string[] args)
        {
            string product = Console.ReadLine();
            int quantity = int.Parse(Console.ReadLine());

            TakeOrder(product, quantity);
        }

        private static void TakeOrder(string product, int quantity)
        {
            double price = GiveProductPrice(product);
            Console.WriteLine($"{(price * quantity):f2}");
        }

        private static double GiveProductPrice(string product)
        {
            switch (product)
            {
                case "coffee": return 1.50;
                case "water": return 1.00;
                case "coke": return 1.40;
                case "snacks": return 2.00;
            }

            return -1;
        }
    }
}
