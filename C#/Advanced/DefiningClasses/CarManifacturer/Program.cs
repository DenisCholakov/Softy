using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CarManufacturer
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            Func<Tire[], double> tirePressureSum = x =>
            {
                double sum = 0;

                for (int i = 0; i < x.Length; i++)
                {
                    sum += x[i].Pressure;
                }

                return sum;
            };

            List<Tire[]> tires = new List<Tire[]>();
            List<Engine> engines = new List<Engine>();
            List<Car> cars = new List<Car>();
            string input = Console.ReadLine();

            while (input != "No more tires")
            {
                var tireSpec = input.Split();
                Tire[] currentTires = new Tire[4];
                for (int i = 0; i < tireSpec.Length; i += 2)
                {
                    int year = int.Parse(tireSpec[i]);
                    double pressure = double.Parse(tireSpec[i + 1]);
                    currentTires[i / 2] = new Tire(year, pressure);
                }

                tires.Add(currentTires);
                input = Console.ReadLine();
            }

            input = Console.ReadLine();

            while (input != "Engines done")
            {
                var engineSpec = input.Split();
                int horsePower = int.Parse(engineSpec[0]);
                double cubicCapacity = double.Parse(engineSpec[1]);
                engines.Add(new Engine(horsePower, cubicCapacity));
                input = Console.ReadLine();
            }

            input = Console.ReadLine();

            while (input != "Show special")
            {
                var carInfo = input.Split();
                string make = carInfo[0];
                string model = carInfo[1];
                int year = int.Parse(carInfo[2]);
                double fuelQuantity = double.Parse(carInfo[3]);
                double fuelConsumption = double.Parse(carInfo[4]);
                int engineIndex = int.Parse(carInfo[5]);
                int tiresIndex = int.Parse(carInfo[5]);
                cars.Add(new Car(make, model, year, fuelQuantity, fuelConsumption, engines[engineIndex], tires[tiresIndex]));
                input = Console.ReadLine();
            }

            foreach (var car in cars.Where(c => c.Year >= 2017 && c.Engine.HorsePower > 330 && 
                                     tirePressureSum(c.Tires) >= 9 && tirePressureSum(c.Tires) <= 10))
            {
                car.Drive(20);
                Console.WriteLine(car);
            }
        }
    }
}
