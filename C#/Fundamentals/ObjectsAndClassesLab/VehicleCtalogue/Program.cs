using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;

namespace VehicleCtalogue
{
    class Program
    {
        static void Main(string[] args)
        {
            Catalog catalog = new Catalog();
            catalog.Cars = new List<Car>();
            catalog.Trucks = new List<Truck>();

            string[] input = Console.ReadLine().Split('/').ToArray();

            while (input[0] != "end")
            {
                if (input[0] == "Car")
                {
                    Car car = new Car();
                    car.Brand = input[1];
                    car.Model = input[2];
                    car.HorsePower = int.Parse(input[3]);

                    catalog.Cars.Add(car);
                }
                else
                {
                    Truck truck = new Truck();

                    truck.Brand = input[1];
                    truck.Model = input[2];
                    truck.Weight = int.Parse(input[3]);

                    catalog.Trucks.Add(truck);
                }

                input = Console.ReadLine().Split('/').ToArray();
            }

            catalog.Cars = catalog.Cars.OrderBy(x => x.Brand).ToList();
            catalog.Trucks = catalog.Trucks.OrderBy(x => x.Brand).ToList();

            Console.WriteLine("Cars:");

            foreach (var car in catalog.Cars)
            {
                Console.WriteLine($"{car.Brand}: {car.Model} - {car.HorsePower}hp");
            }

            Console.WriteLine("Trucks:");

            foreach (var truck in catalog.Trucks)
            {
                Console.WriteLine($"{truck.Brand}: {truck.Model} - {truck.Weight}kg");
            }
        }
    }

    public class Truck
    {
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Weight { get; set; }
    }

    public class Car
    {
        public string Brand { get; set; }
        public string Model { get; set; }
        public int HorsePower { get; set; }
    }

    public class Catalog
    {
        public List<Truck> Trucks { get; set; }
        public List<Car> Cars { get; set; }
    }
}
