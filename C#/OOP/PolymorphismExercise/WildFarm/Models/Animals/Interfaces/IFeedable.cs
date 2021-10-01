using System;
using System.Collections.Generic;
using System.Text;

using WildFarm.Models.Foods.Interfaces;

namespace WildFarm.Models.Animals.Interfaces
{
    public interface IFeedable
    {
        void Feed(IFood food);

        int FoodEaten { get; }

        double WeightMultiplier { get; }

        ICollection<Type> PreferredFoods { get; }
    }
}
