﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant
{
    class Coffee : HotBeverage
    {
        private const double COFFEE_MILLITERS = 50;
        private const decimal COFFEE_PRICE = 3.50m;

        public Coffee(string name, double caffeine) : base(name, COFFEE_PRICE, COFFEE_MILLITERS)
        {
            this.Caffeine = caffeine;
        }

        public double Caffeine { get; set; }
    }
}
