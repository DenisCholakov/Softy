using System;
using System.Collections.Generic;

namespace SongQueue
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] initialSongs = Console.ReadLine().Split(", ");
            Queue<string> queue = new Queue<string>(initialSongs);

            string command = Console.ReadLine();

            while (queue.Count > 0)
            {
                if (command.Contains("Play"))
                {
                    queue.Dequeue();
                }
                else if (command.Contains("Add"))
                {
                    string song = command.Substring(4);

                    if (!queue.Contains(song))
                    {
                        queue.Enqueue(song);
                    }
                    else
                    {
                        Console.WriteLine($"{song} is already contained!");
                    }

                }
                else if(command.Contains("Show"))
                {
                    Console.WriteLine(String.Join(", ", queue));
                }

                command = Console.ReadLine();
            }

            Console.WriteLine("No more songs!");
        }
    }
}
