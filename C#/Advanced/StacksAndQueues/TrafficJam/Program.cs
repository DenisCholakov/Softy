using System;
using System.Collections.Generic;

namespace TrafficJam
{
    class Program
    {
        static void Main(string[] args)
        {
            int greenPass = int.Parse(Console.ReadLine());
            Queue<string> queue = new Queue<string>();
            string input = Console.ReadLine();
            int carsPassed = 0;

            while (input != "end")
            {
                if (input == "green")
                {
                    for (int i = 0; i < greenPass; i++)
                    {

                        if (queue.Count == 0)
                        {
                            break;
                        }

                        carsPassed++;
                        Console.WriteLine($"{queue.Dequeue()} passed!");
                    }
                }
                else
                {
                    queue.Enqueue(input);
                }
              
                input = Console.ReadLine();
            }

            Console.WriteLine($"{carsPassed} cars passed the crossroads.");
        }
    }
}
