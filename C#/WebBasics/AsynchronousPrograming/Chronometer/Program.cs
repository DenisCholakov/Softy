using System;

namespace Chronometer
{
    class Program
    {
        static void Main(string[] args)
        {
            var chronometer = new Chronometer();

            while (true)
            {
                var input = Console.ReadLine();

                switch (input)
                {
                    case "start": chronometer.Start(); break;
                    case "stop": chronometer.Stop(); break;
                    case "lap": Console.WriteLine(chronometer.Lap()); break;
                    case "laps":
                        if (chronometer.Laps.Count == 0)
                        {
                            Console.WriteLine("Laps: no laps");
                        }
                        else
                        {
                            Console.WriteLine("Laps:");
                            for (int i = 0; i < chronometer.Laps.Count; i++)
                            {
                                Console.WriteLine($"{i} {chronometer.Laps[i]}");
                            };
                        }
                        
                        break;
                    case "time": Console.WriteLine(chronometer.GetTime); break;
                    case "reset": chronometer.Reset(); break;
                    case "exit": return;
                }
            }
        }
    }
}
