using System;
using System.Collections.Generic;
using System.Text;

using Vehicles.Common;

namespace Vehicles.Models
{
    class Truck : Vehicle
    {
        private const double FUEL_CONSUPTION_INCR = 1.6;
        private const double REFUEL_SUCC_COEF = 0.95;

        public Truck(double fuelQuantity, double fuelConsumption, double tankCapacity) : 
                    base(fuelQuantity, fuelConsumption, tankCapacity)
        {
        }

        public override double FuelConsumption => base.FuelConsumption + FUEL_CONSUPTION_INCR;

        public override void Refuel(double amount)
        {
            double capacity = this.TankCapacity - this.FuelQuantity;

            if (amount * REFUEL_SUCC_COEF > capacity)
            {
                throw new InvalidOperationException(String.Format(ExceptionMessages.TooBigFuelAmount, amount));
            }

            base.Refuel(amount * REFUEL_SUCC_COEF);
        }
    }
}
