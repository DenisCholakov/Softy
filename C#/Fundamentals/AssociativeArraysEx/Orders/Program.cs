using System;
using System.Collections.Generic;

namespace Orders
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, Product> products = new Dictionary<string, Product>();

            string input = Console.ReadLine();
            while (input != "buy")
            {
                string[] product = input.Split();
                string name = product[0];
                double price = double.Parse(product[1]);
                int qty = int.Parse(product[2]);
                if (products.ContainsKey(name))
                {
                    products[name].Price = price;
                    products[name].Qty += qty;
                }
                else
                {
                    products.Add(name, new Product(name, price, qty));
                }

                input = Console.ReadLine();
            }

            foreach (var pair in products)
            {
                System.Console.WriteLine(pair.Value);
            }
        }
    }

    public class Product
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public int Qty { get; set; }

        public Product(string name, double price, int qty)
        {
            this.Name = name;
            this.Price = price;
            this.Qty = qty;
        }

        public override string ToString() => $"{this.Name} -> {(this.Price * this.Qty):f2}";
    }
}
