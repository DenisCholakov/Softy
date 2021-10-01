using System;

namespace WorldTour
{
    class Program
    {
        static void Main(string[] args)
        {
            string stops = Console.ReadLine();

            string[] command = Console.ReadLine().Split(":", StringSplitOptions.RemoveEmptyEntries);
            while (command[0] != "Travel")
            {
                if (command[0] == "Add Stop")
                {
                    int index = int.Parse(command[1]);
                    string insertString = command[2];
                    if (index >= 0 && index < stops.Length)
                    {
                        stops = stops.Insert(index, insertString);
                    }
                }
                else if (command[0] == "Remove Stop")
                {
                    int startIndex = int.Parse(command[1]);
                    int endIndex = int.Parse(command[2]);
                    if ((startIndex >= 0 && startIndex < stops.Length) && (endIndex >= 0 && endIndex < stops.Length)
                                    && startIndex <= endIndex)
                    {
                        stops = stops.Remove(startIndex, endIndex - startIndex + 1);
                    }
                }
                else if (command[0] == "Switch")
                {
                    string oldString = command[1];
                    string newString = command[2];
                    stops = stops.Replace(oldString, newString);
                }

                System.Console.WriteLine(stops);
                command = Console.ReadLine().Split(":", StringSplitOptions.RemoveEmptyEntries);
            }

            System.Console.WriteLine($"Ready for world tour! Planned stops: {stops}");
        }
    }
}
