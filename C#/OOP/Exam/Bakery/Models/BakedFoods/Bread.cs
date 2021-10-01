using System;
using System.Collections.Generic;
using System.Text;

namespace Bakery.Models.BakedFoods
{
    public class Bread : BakedFood
    {
        private const int initialPortion = 200;

        public Bread(string name, decimal price) 
                    : base(name, initialPortion, price)
        {
        }
    }
}
