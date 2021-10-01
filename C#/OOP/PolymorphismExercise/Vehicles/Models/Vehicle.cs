using System;
using System.Collections.Generic;
using System.Text;

using Vehicles.Models.Interfaces;
using Vehicles.Common;

namespace Vehicles.Models
{
    public abstract class Vehicle : IDriveable, IRefuelable
    {
        private const string SUCC_DRIVE_MSG = "{0} travelled {1} km";

        private double fuelQuantity;

        protected Vehicle(double fuelQuantity, double fuelConsumption, double tankCapacity)
        {
            this.TankCapacity = tankCapacity;
            this.FuelQuantity = fuelQuantity;
            this.FuelConsumption = fuelConsumption;
        }

        public double FuelQuantity 
        {
            get { return this.fuelQuantity; }
            private set
            {
                if (value > this.TankCapacity)
                {
                    this.fuelQuantity = 0;
                }
                else
                {
                    this.fuelQuantity = value;
                }
            } 
        }
        public virtual double FuelConsumption { get; }
        public double TankCapacity { get; set; }

        public string Drive(double amount)
        {
            return this.Drive(amount, this.FuelConsumption);
        }

        public virtual void Refuel(double amount)
        {
            double capacityLeft = this.TankCapacity - this.FuelQuantity;

            if (amount <= 0)
            {
                throw new InvalidOperationException(ExceptionMessages.NegativeFuelAmount);
            }
            else if (amount > capacityLeft)
            {
                throw new InvalidOperationException(String.Format(ExceptionMessages.TooBigFuelAmount, amount));
            }

            this.FuelQuantity += amount;
        }

        public override string ToString()
        {
            return $"{this.GetType().Name}: {FuelQuantity:f2}";
        }

        protected string Drive(double amount, double fuelConsuption)
        {
            double fuelNeeded = amount * fuelConsuption;

            if (this.FuelQuantity >= fuelNeeded)
            {
                this.FuelQuantity -= fuelNeeded;
                return String.Format(SUCC_DRIVE_MSG, this.GetType().Name, amount);
            }
            else
            {
                throw new InvalidOperationException(String.Format(
                                ExceptionMessages.NotEnoughFuel, this.GetType().Name));
            }
        }
    }
}
