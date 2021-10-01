using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace PizzaCalories
{
    class Pizza
    {
        private string name;
        private List<Topping> toppings;

        private Pizza()
        {
            this.toppings = new List<Topping>();
        }

        public Pizza(string name) : this()
        {
            this.Name = name;
        }

        public int ToppingCount => this.toppings.Count;

        public double TotalCalories => this.GetCalories();

        public string Name 
        {
            get { return this.name; }
            set
            {
                if (value.Length > 15 || value.Length < 1)
                {
                    throw new ArgumentException("Pizza name should be between 1 and 15 symbols.");
                }

                this.name = value;
            } 
        }

        public Dough Dough { get; set; }

        public void AddTopping(Topping topping)
        {
            this.ValidateToppingsCount();
            this.toppings.Add(topping);
        }

        public double GetCalories()
        {
            double result = this.Dough.GetCalories();

            foreach (var topping in this.toppings)
            {
                result += topping.Calories;
            }

            return result;
        }

        public override string ToString() => $"{this.Name} - {this.GetCalories():f2} Calories.";

        private void ValidateToppingsCount()
        {
            if (this.toppings.Count >= 10)
            {
                throw new InvalidOperationException("Number of toppings should be in range [0..10].");
            }
        }
    }
}
