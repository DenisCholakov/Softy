using System;
using System.Collections.Generic;
using System.IO;

namespace SpeedRacing
{
    class StartUp
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            Dictionary<string, Car> cars = new Dictionary<string, Car>();
      
            for (int i = 0; i < n; i++)
            {
                string[] carInput = Console.ReadLine().Split();
                string model = carInput[0];
                double fuelAmount = double.Parse(carInput[1]);
                double fuelConsumptionPerKm = double.Parse(carInput[2]);
                cars.Add(model, new Car(model, fuelAmount, fuelConsumptionPerKm));
            }

            string input = Console.ReadLine();

            while (input != "End")
            {
                string[] command = input.Split();

                if (command[0] == "Drive")
                {
                    string model = command[1];
                    double distance = double.Parse(command[2]);

                    if (cars.ContainsKey(model))
                    {
                        cars[model].Drive(distance);
                    }
                }

                input = Console.ReadLine();
            }

            foreach (var pair in cars)
            {
                Console.WriteLine($"{pair.Key} {pair.Value.FuelAmount:f2} {pair.Value.TravelledDistance}");
            }
        }
    }
}
