using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices.ComTypes;

namespace CarSalesman
{
    class StartUp
    {
        static void Main(string[] args)
        {
            Dictionary<string, Engine> engines = new Dictionary<string, Engine>();
            List<Car> cars = new List<Car>();
            int enginesCount = int.Parse(Console.ReadLine());

            for (int i = 0; i < enginesCount; i++)
            {
                string[] engineData = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);

                if (engineData.Length == 2)
                {
                    string model = engineData[0];
                    int power = int.Parse(engineData[1]);

                    engines.Add(model, new Engine(model, power));
                }
                else if (engineData.Length == 3 && int.TryParse(engineData[2], out int x))
                {
                    string model = engineData[0];
                    int power = int.Parse(engineData[1]);
                    int displacement = int.Parse(engineData[2]);

                    engines.Add(model, new Engine(model, power, displacement));
                }
                else if (engineData.Length == 3)
                {
                    string model = engineData[0];
                    int power = int.Parse(engineData[1]);
                    string efficienty = engineData[2];

                    engines.Add(model, new Engine(model, power, efficienty));
                }
                else
                {
                    string model = engineData[0];
                    int power = int.Parse(engineData[1]);
                    int displacement = int.Parse(engineData[2]);
                    string efficienty = engineData[3];

                    engines.Add(model, new Engine(model, power, displacement, efficienty));
                }
            }

            int carsCount = int.Parse(Console.ReadLine());

            for (int i = 0; i < carsCount; i++)
            {
                string[] carData = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);

                string model = carData[0];
                string engineModel = carData[1];


                if (!engines.ContainsKey(engineModel))
                {
                    throw new ArgumentException("This engine is missing in the engine input!");
                }

                Engine engine = engines[engineModel];

                if (carData.Length == 2)
                {
                    cars.Add(new Car(model, engine));
                }
                else if (carData.Length == 3 && int.TryParse(carData[2], out int x))
                {
                    string weight = carData[2];

                    cars.Add(new Car(model, engine, weight));
                }
                else if (carData.Length == 3)
                {
                    string color = carData[2];

                    cars.Add(new Car(model, engine, "n/a", color));
                }
                else if (carData.Length == 4)
                {
                    string weight = carData[2];
                    string color = carData[3];

                    cars.Add(new Car(model, engine, weight, color));
                }
            }

            foreach (var car in cars)
            {
                Console.WriteLine(car);
            }
        }
    }
}
