using System;
using System.Collections.Generic;
using System.Globalization;

namespace ParkingLot
{
    class Program
    {
        static void Main(string[] args)
        {
            HashSet<string> cars = new HashSet<string>();

            string input = Console.ReadLine();
            while (input != "END")
            {
                string[] command = input.Split(", ");

                if (command[0] == "IN")
                {
                    cars.Add(command[1]);
                }
                else if (command[0] == "OUT")
                {
                    cars.Remove(command[1]);
                }

                input = Console.ReadLine();
            }

            if (cars.Count != 0)
            {
                Console.WriteLine(String.Join(Environment.NewLine, cars));
            }
            else
            {
                Console.WriteLine("Parking Lot is Empty");
            }
            
        }
    }
}
