using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaCalories
{
    class Topping
    {
        private const string INVALID_TYPE_EXC_MSG = "Cannot place {0} on top of your pizza.";
        private const string INVALID_WIGHT_EXC_MSG = "{0} weight should be in the range [{1}..{2}].";
        private const int MIN_WEIGHT = 1;
        private const int MAX_WEIGHT = 50;

        private string type;
        private double weight;
        private Dictionary<string, double> calories;

        private Topping()
        {
            this.InitializeCaloriesDict();
        }
        public Topping(string type, double weight) : this()
        {
            this.Type = type;
            this.Weight = weight;
        }

        public double Calories => (this.Weight * 2) * this.calories[this.Type.ToLower()];

        private string Type
        {
            get { return this.type; }
            set
            {
                this.ValidateType(value);
                this.type = value;
            }
        }

        private double Weight
        {
            get { return this.weight; }
            set
            {
                if (value < MIN_WEIGHT || value > MAX_WEIGHT)
                {
                    throw new ArgumentException(String.Format(INVALID_WIGHT_EXC_MSG, this.Type, MIN_WEIGHT, MAX_WEIGHT));
                }

                this.weight = value;
            }
        }

        private void ValidateType(string type)
        {
            if (!this.calories.ContainsKey(type.ToLower()))
            {
                throw new ArgumentException(String.Format(INVALID_TYPE_EXC_MSG, type));
            }
        }

        private void InitializeCaloriesDict()
        {
            this.calories = new Dictionary<string, double>
            {
                { "meat", 1.2 },
                { "veggies", 0.8 },
                { "cheese", 1.1 },
                { "sauce", 0.9 }
            };
        }
    }
}
