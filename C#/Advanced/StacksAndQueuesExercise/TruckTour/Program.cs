using System;
using System.Collections.Generic;
using System.Linq;

namespace TruckTour
{
    class Program
    {
        static void Main(string[] args)
        {
            int numberOfGasStations = int.Parse(Console.ReadLine());
            Queue<int[]> fuelAndDistance = new Queue<int[]>();
            
            int gasStation = 0;
            int totalFuel = 0;

            for (int i = 0; i < numberOfGasStations; i++)
            {
                int[] station = Console.ReadLine().Split().Select(int.Parse).ToArray();
                fuelAndDistance.Enqueue(station);
            }

            Queue<int[]> copy = new Queue<int[]>(fuelAndDistance);

            for (int i = 0; i < numberOfGasStations; i++)
            {
                int[] currentSt = copy.Dequeue();
                totalFuel += currentSt[0];
                int fuelNeeded = currentSt[1];

                if (totalFuel < fuelNeeded )
                {
                    gasStation++;
                    fuelAndDistance.Enqueue(fuelAndDistance.Dequeue());
                    totalFuel = 0;
                    copy = new Queue<int[]>(fuelAndDistance);
                    i = -1;
                }
                else
                {
                    totalFuel -= fuelNeeded;
                }
            }

            Console.WriteLine(gasStation);
        }
    }
}
