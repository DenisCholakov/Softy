using System;
using System.Collections.Generic;

namespace SpeedRacing
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Car> cars = new List<Car>();

            int n = int.Parse(Console.ReadLine());

            string input = String.Empty;

            for (int i = 0; i < n; i++)
            {
                input = Console.ReadLine();
                string[] carInfo = input.Split();
                cars.Add(new Car(carInfo[0], double.Parse(carInfo[1]), double.Parse(carInfo[2])));
            }

            input = Console.ReadLine();

            while (input != "End")
            {
                string[] drive = input.Split();
                int index = cars.FindIndex(x => x.Model == drive[1]);
                cars[index].Drive(int.Parse(drive[2]));
                input = Console.ReadLine();
            }

            cars.ForEach(x => System.Console.WriteLine(x));
        }
    }

    class Car
    {
        public string Model { get; set; }
        public double FuelAmount { get; set; }
        public double FuelConsuptionPerKm { get; set; }
        public int TraveledDistance { get; set; }

        public void Drive(int km)
        {
            double consumedFuel = km * this.FuelConsuptionPerKm;
            if (consumedFuel <= this.FuelAmount)
            {
                this.FuelAmount -= consumedFuel;
                this.TraveledDistance += km;
            }
            else
            {
                System.Console.WriteLine("Insufficient fuel for the drive");
            }
        }

        public override string ToString() => $"{this.Model} {this.FuelAmount:f2} {this.TraveledDistance}";

        public Car(string model, double fuelAmount, double fuelcons)
        {
            this.Model = model;
            this.FuelAmount = fuelAmount;
            this.FuelConsuptionPerKm = fuelcons;
            this.TraveledDistance = 0;
        }
    }
}
