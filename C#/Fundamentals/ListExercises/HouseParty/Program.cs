using System;
using System.Collections.Generic;

namespace HouseParty
{
    class Program
    {
        static void Main(string[] args)
        {
            int numOfCommands = int.Parse(Console.ReadLine());

            List<string> guests = new List<string>();
            for (int i = 0; i < numOfCommands; i++)
            {
                string[] command = Console.ReadLine().Split();
                if (command.Length == 3)
                {
                    string name = command[0];

                    if (guests.Contains(name))
                    {
                        Console.WriteLine($"{name} is already in the list!");
                    }
                    else
                    {
                        guests.Add(name);
                    }
                }
                else
                {
                    string name = command[0];

                    if (guests.Contains(name))
                    {
                        guests.Remove(name);
                    }
                    else
                    {
                        Console.WriteLine($"{name} is not in the list!");
                    }
                }
            }

            Console.WriteLine(String.Join(Environment.NewLine, guests));
        }
    }
}
