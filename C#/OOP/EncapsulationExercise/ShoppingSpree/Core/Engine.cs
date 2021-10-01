using ShoppingSpree.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;

namespace ShoppingSpree.Core
{
    class Engine
    {
        private readonly ICollection<Person> people;
        private readonly ICollection<Product> products;
        public Engine()
        {
            this.people = new List<Person>();
            this.products = new List<Product>();
        }

        public void Run()
        {
            try
            {
                this.ParsePeopleInput();
                this.ParseProductsInput();

                string command;
                while ((command = Console.ReadLine()) != "END")
                {
                    string[] buyInfo = command.Split();
                    string personName = buyInfo[0];
                    string productToBuy = buyInfo[1];

                    var buyer = this.people.FirstOrDefault(p => p.Name == personName);
                    var product = this.products.FirstOrDefault(p => p.Name == productToBuy);

                    if (buyer != null && product != null)
                    {
                        Console.WriteLine(buyer.BuyProduct(product));
                    }
                }

                foreach (var person in this.people)
                {
                    Console.WriteLine(person);
                }
            }
            catch (ArgumentException ae)
            {
                Console.WriteLine(ae.Message);
            }
            

        }

        private void ParsePeopleInput()
        {
            string[] peopleArgs = Console.ReadLine()
                                    .Split(';', StringSplitOptions.RemoveEmptyEntries);

            foreach (var personStr in peopleArgs)
            {
                string[] personArgs = personStr.Split('=', StringSplitOptions.RemoveEmptyEntries);
                string name = personArgs[0];
                decimal money = decimal.Parse(personArgs[1]);
                this.people.Add(new Person(name, money));
            }
        }

        private void ParseProductsInput()
        {
            string[] productsArgs = Console.ReadLine()
                                    .Split(';', StringSplitOptions.RemoveEmptyEntries);

            foreach (var productStr in productsArgs)
            {
                string[] productArgs = productStr.Split('=', StringSplitOptions.RemoveEmptyEntries);
                string name = productArgs[0];
                decimal cost = decimal.Parse(productArgs[1]);
                this.products.Add(new Product(name, cost));
            }
        }
    }
}
