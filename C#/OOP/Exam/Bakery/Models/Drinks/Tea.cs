using System;
using System.Collections.Generic;
using System.Text;

namespace Bakery.Models.Drinks
{
    class Tea : Drink
    {
        private const decimal initialPrice = 2.50m;
        public Tea(string name, int portion, string brand) 
            : base(name, portion, initialPrice, brand)
        {
        }
    }
}
