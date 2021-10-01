using System;
using System.Collections.Generic;
using System.Text;

namespace Bakery.Models.Drinks
{
    class Water : Drink
    {
        private const decimal initialPrice = 1.50m;
        public Water(string name, int portion, string brand) 
            : base(name, portion, initialPrice, brand)
        {
        }
    }
}
