using Bakery.Models.BakedFoods.Contracts;
using Bakery.Models.Drinks.Contracts;
using Bakery.Models.Tables.Contracts;
using Bakery.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bakery.Models.Tables
{
    public abstract class Table : ITable
    {
        private ICollection<IBakedFood> foodOrders;
        private ICollection<IDrink> drinkOrders;

        private int capacity;
        private int numberOfPeople;
        private bool isReserved;

        protected Table(int tableNumber, int capacity, decimal pricePerPerson)
        {
            foodOrders = new List<IBakedFood>();
            drinkOrders = new List<IDrink>();

            this.TableNumber = tableNumber;
            this.Capacity = capacity;
            this.PricePerPerson = pricePerPerson;
            this.isReserved = false;
        }

        // maybe some validation for now nothing in description
        public int TableNumber { get; }

        public int Capacity
        {
            get { return this.capacity; }
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException(ExceptionMessages.InvalidTableCapacity);
                }

                this.capacity = value;
            }
        }

        public int NumberOfPeople
        { 
            get { return this.numberOfPeople; }
            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentException(ExceptionMessages.InvalidNumberOfPeople);
                }

                this.numberOfPeople = value;
            }
        }

        public decimal PricePerPerson { get; }

        public bool IsReserved => this.isReserved;

        // judje might not like
        public decimal Price { get { return this.numberOfPeople * this.PricePerPerson; } }

        public void Clear()
        {
            this.foodOrders = new List<IBakedFood>();
            this.drinkOrders = new List<IDrink>();
            this.isReserved = false;
        }

        public decimal GetBill()
        {
            // if list is empty what does sum return 
            return this.foodOrders.Sum(f => f.Price) + this.drinkOrders.Sum(d => d.Price) + this.Price;
        }

        public string GetFreeTableInfo()
        {
            // why free table when there is no condition
            StringBuilder info = new StringBuilder();

            info.AppendLine($"Table: {this.TableNumber}");
            info.AppendLine($"Type: {this.GetType().Name}");
            info.AppendLine($"Capacity: {this.capacity}");
            info.AppendLine($"Price per Person: {this.PricePerPerson}");

            return info.ToString().TrimEnd();
        }

        public void OrderDrink(IDrink drink)
        {
            this.drinkOrders.Add(drink);
        }

        public void OrderFood(IBakedFood food)
        {
            this.foodOrders.Add(food);
        }

        public void Reserve(int numberOfPeople)
        {
            if (numberOfPeople > capacity)
            {
                throw new InvalidOperationException("Not enough capacity!");
            }

            this.isReserved = true;
            this.NumberOfPeople = numberOfPeople;
        }
    }
}
