﻿using System;
using System.Collections.Generic;
using System.Text;

using WildFarm.Models.Foods.Interfaces;

namespace WildFarm.Models.Foods
{
    public abstract class Food : IFood
    {
        protected Food(int quantity)
        {
            this.Quantity = quantity;
        }

        public int Quantity { get; }
    }
}
