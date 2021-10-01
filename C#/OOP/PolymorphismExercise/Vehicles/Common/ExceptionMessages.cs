using System;
using System.Collections.Generic;
using System.Text;

namespace Vehicles.Common
{
    class ExceptionMessages
    {
        public const string NotEnoughFuel = "{0} needs refueling";
        public const string NegativeFuelAmount = "Fuel must be a positive number";
        public const string IvalidVehicletype = "Invalid vehicle type";
        public const string TooBigFuelAmount = "Cannot fit {0} fuel in the tank";
    }
}
