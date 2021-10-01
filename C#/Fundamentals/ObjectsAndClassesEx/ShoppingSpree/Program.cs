using System;
using System.Collections.Generic;

namespace ShoppingSpree
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Person> people = new List<Person>();
            List<Product> products = new List<Product>();
            string input = Console.ReadLine();

            string[] person = input.Split(';', StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < person.Length; i++)
            {
                string[] info = person[i].Split('=');
                people.Add(new Person(info[0], decimal.Parse(info[1])));
            }

            input = Console.ReadLine();
            string[] product = input.Split(';', StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < product.Length; i++)
            {
                string[] info = product[i].Split('=');
                products.Add(new Product(info[0], decimal.Parse(info[1])));
            }

            input = Console.ReadLine();

            while (input != "END")
            {
                string[] buy = input.Split();
                int personIndex = people.FindIndex(x => x.Name == buy[0]);
                int productIndex = products.FindIndex(x => x.Name == buy[1]);
                if (people[personIndex].Money >= products[productIndex].Cost)
                {
                    people[personIndex].BagOfProducts.Add(products[productIndex].Name);
                    people[personIndex].Money -= products[productIndex].Cost;
                    System.Console.WriteLine($"{buy[0]} bought {buy[1]}");
                }
                else
                {
                    System.Console.WriteLine($"{buy[0]} can't afford {buy[1]}");
                }

                input = Console.ReadLine();
            }

            people.ForEach(x => x.Print());
        }
    }

    class Person
    {
        public string Name { get; set; }
        public decimal Money { get; set; }
        public List<string> BagOfProducts { get; set; }

        public Person(string name, decimal money)
        {
            this.Name = name;
            this.Money = money;
            this.BagOfProducts = new List<string>();
        }

        public void Print()
        {
            System.Console.Write($"{this.Name} - ");

            if (this.BagOfProducts.Count == 0)
            {
                System.Console.WriteLine("Nothing bought");
            }
            else
            {
                System.Console.WriteLine(String.Join(", ", this.BagOfProducts));
            }
        }
    }

    class Product
    {
        public string Name { get; set; }
        public decimal Cost { get; set; }

        public Product(string name, decimal cost)
        {
            this.Name = name;
            this.Cost = cost;
        }
    }
}
