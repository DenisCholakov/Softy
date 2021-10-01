using System;
using System.Collections.Generic;

namespace CrossRoads
{
    class Program
    {
        static void Main(string[] args)
        {

            int greenLight = int.Parse(Console.ReadLine());
            int freeWindow = int.Parse(Console.ReadLine());
            Queue<string> cars = new Queue<string>();
            string input = Console.ReadLine();
            string currentCar = String.Empty;
            int passedCars = 0;

            while (input != "END")
            {
                if (input == "green")
                {
                    int greenDuration = greenLight;
                    int windowDuration = freeWindow;
                    while (greenDuration > 0 && cars.Count != 0)
                    {
                        currentCar = cars.Dequeue();
                        greenDuration -= currentCar.Length;

                        if (greenDuration >= 0)
                        {
                            passedCars++;
                        }
                        else
                        {
                            windowDuration += greenDuration;

                            if (windowDuration < 0)
                            {
                                Console.WriteLine("A crash happened!");
                                Console.WriteLine($"{currentCar} was hit at {currentCar[currentCar.Length + windowDuration]}.");
                                return;
                            }

                            passedCars++;
                        }
                    }
                }
                else
                {
                    cars.Enqueue(input);
                }

                input = Console.ReadLine();
            }

            Console.WriteLine("Everyone is safe.");
            Console.WriteLine($"{passedCars} total cars passed the crossroads.");
        }
    }
}
