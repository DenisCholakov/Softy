using System;
using System.Collections.Generic;
using System.Text;

namespace NeedForSpeed
{
    public class RaceMotorcycle : Motorcycle
    {
        private const double DEFAULT_FUEL_CONSUPTION = 8;

        public RaceMotorcycle(int horsePower, double fuel) : base (horsePower, fuel)
        {
        }

        public override double FuelConsumption
        {
            get { return DEFAULT_FUEL_CONSUPTION; }
        }

        public override void Drive(double kilometers)
        {
            this.Fuel -= kilometers * this.FuelConsumption;
        }
    }
}