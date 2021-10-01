using System;
using System.Collections.Generic;
using System.Linq;

namespace RawData
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
                cars.Add(new Car(carInfo[0], int.Parse(carInfo[1]), int.Parse(carInfo[2]), int.Parse(carInfo[3]), carInfo[4]));
            }

            input = Console.ReadLine();
            var neededCars = new List<Car>();

            if (input == "fragile")
            {
                neededCars = cars.Where(x => x.Cargo.Type == "fragile" && x.Cargo.Weight < 1000).ToList();
            }
            else
            {
                neededCars = cars.Where(x => x.Cargo.Type == "flamable" && x.Engine.Power > 250).ToList();
            }

            neededCars.ForEach(x => System.Console.WriteLine(x.Model));
        }
    }

    class Car
    {
        public string Model { get; set; }
        public Engine Engine { get; set; }
        public Cargo Cargo { get; set; }

        public Car(string model, int engineSpeed, int enginePower, int cargoWeight, string cargoType)
        {
            this.Model = model;
            this.Engine = new Engine(engineSpeed, enginePower);
            this.Cargo = new Cargo(cargoWeight, cargoType);
        }
    }

    class Engine
    {
        public int Speed { get; set; }
        public int Power { get; set; }

        public Engine(int speed, int power)
        {
            this.Speed = speed;
            this.Power = power;
        }
    }

    class Cargo
    {
        public int Weight { get; set; }
        public string Type { get; set; }

        public Cargo(int weight, string type)
        {
            this.Weight = weight;
            this.Type = type;
        }
    }
}
