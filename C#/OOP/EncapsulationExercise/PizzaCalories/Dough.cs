using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaCalories
{
    class Dough
    {
        private const string INV_DOUGH_EXC_MSG = "Invalid type of dough.";
        private const string INV_WEIGHT_EXC_MSG = "Dough weight should be in the range [{0}..{1}].";
        private const int MIN_WEIGHT = 1;
        private const int MAX_WEIGHT = 200;

        private string flourType;
        private string bakingTechnique;
        private double weight;

        public Dough(string flourType, string bakingTechnique, double weight)
        {
            this.FlourType = flourType;
            this.BackingTechnique = bakingTechnique;
            this.Weight = weight;
        }

        public string FlourType 
        {
            get { return this.flourType; }
            private set
            {
                if (GetFlourCalories(value) < 0)
                {
                    throw new ArgumentException(INV_DOUGH_EXC_MSG);
                }

                this.flourType = value;
            } 
        }

        public string BackingTechnique 
        {
            get { return this.bakingTechnique; }
            private set
            {
                if (GetTechniqueCalories(value) < 0)
                {
                    throw new ArgumentException(INV_DOUGH_EXC_MSG);
                }

                this.bakingTechnique = value;
            }
        }

        public double Weight 
        {
            get { return this.weight; }
            private set
            {
                if (value < MIN_WEIGHT || value > MAX_WEIGHT)
                {
                    throw new ArgumentException(String.Format(INV_WEIGHT_EXC_MSG, MIN_WEIGHT, MAX_WEIGHT));
                }

                this.weight = value;
            } 
        }

        public double GetCalories()
        {
            return (2 * this.Weight) * GetFlourCalories(this.FlourType) * GetTechniqueCalories(this.BackingTechnique);
        }

        private static double GetFlourCalories(string flourType)
        {
            string type = flourType.ToLower();

            if (type == "white")
            {
                return 1.5;
            }
            else if (type == "wholegrain")
            {
                return 1.0;
            }
            else
            {
                return -1;
            }
        }

        private static double GetTechniqueCalories(string bakingTechnique)
        {
            string technique = bakingTechnique.ToLower();

            if (technique == "crispy")
            {
                return 0.9;
            }
            else if (technique == "chewy")
            {
                return 1.1;
            }
            else if (technique == "homemade")
            {
                return 1.0;
            }
            else
            {
                return -1;
            }
        }
    }
}
