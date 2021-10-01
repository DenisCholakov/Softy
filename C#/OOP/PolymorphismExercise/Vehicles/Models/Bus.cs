using System;
using System.Collections.Generic;
using System.Text;

namespace Vehicles.Models
{
    class Bus : Vehicle
    {
        private const double FUEL_CONSUMPTION_INCR = 1.4;
        public Bus(double fuelQuantity, double fuelConsumption, double tankCapacity) : 
                    base(fuelQuantity, fuelConsumption, tankCapacity)
        {
        }

        public override double FuelConsumption => base.FuelConsumption + FUEL_CONSUMPTION_INCR;

        public string DriveEmpty(double distance)
        {
            return base.Drive(distance, base.FuelConsumption);
        }
    }
}
