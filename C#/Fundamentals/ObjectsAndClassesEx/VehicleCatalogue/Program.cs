using System;
using System.Collections.Generic;
using System.Linq;

namespace VehicleCatalogue
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Car> cars = new List<Car>();
            List<Truck> trucks = new List<Truck>();

            string input = Console.ReadLine();

            while (input != "End")
            {
                string[] vehicleInfo = input.Split().ToArray();
                if (vehicleInfo[0] == "car")
                {
                    cars.Add(new Car(vehicleInfo[1], vehicleInfo[2], int.Parse(vehicleInfo[3])));
                }
                else if (vehicleInfo[0] == "truck")
                {
                    trucks.Add(new Truck(vehicleInfo[1], vehicleInfo[2], int.Parse(vehicleInfo[3])));
                }

                input = Console.ReadLine();
            }

            input = Console.ReadLine();

            while (input != "Close the Catalogue")
            {
                int index = cars.FindIndex(x => x.Model == input);

                if (index >= 0)
                {
                    cars[index].PrintInfo();
                }

                index = trucks.FindIndex(x => x.Model == input);
                if (index >= 0)
                {
                    trucks[index].PrintInfo();
                }

                input = Console.ReadLine();
            }
            int sum = 0;
            if (cars.Count == 0)
            {
                System.Console.WriteLine($"Cars have average horsepower of: {0.0:f2}.");
            }
            else
            {
                cars.ForEach(x => sum += x.HP);
                System.Console.WriteLine($"Cars have average horsepower of: {((sum * 1.0) / cars.Count):f2}.");
            }
            sum = 0;
            if (trucks.Count == 0)
            {
                System.Console.WriteLine($"Trucks have average horsepower of: {0.0:f2}.");
            }
            else
            {
                trucks.ForEach(x => sum += x.HP);
                System.Console.WriteLine($"Trucks have average horsepower of: {((sum * 1.0) / trucks.Count):f2}.");
            }
        }
    }

    public class Car
    {
        public string Model { get; set; }
        public string Color { get; set; }
        public int HP { get; set; }

        public Car(string model, string color, int hp)
        {
            this.Model = model;
            this.Color = color;
            this.HP = hp;
        }

        public void PrintInfo()
        {
            System.Console.WriteLine("Type: Car");
            System.Console.WriteLine($"Model: {this.Model}");
            System.Console.WriteLine($"Color: {this.Color}");
            System.Console.WriteLine($"Horsepower: {this.HP}");
        }
    }

    public class Truck
    {
        public string Model { get; set; }
        public string Color { get; set; }
        public int HP { get; set; }

        public Truck(string model, string color, int hp)
        {
            this.Model = model;
            this.Color = color;
            this.HP = hp;
        }

        public void PrintInfo()
        {
            System.Console.WriteLine("Type: Truck");
            System.Console.WriteLine($"Model: {this.Model}");
            System.Console.WriteLine($"Color: {this.Color}");
            System.Console.WriteLine($"Horsepower: {this.HP}");
        }
    }
}
