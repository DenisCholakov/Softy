using System;
using System.Collections.Generic;
using System.Linq;

namespace ChangeList
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> list = Console.ReadLine().Split().Select(int.Parse).ToList();

            string input = Console.ReadLine();
            while (input != "end")
            {
                string[] command = input.Split();
                if (command[0] == "Delete")
                {
                    int element = int.Parse(command[1]);
                    bool elExists = true;
                    while (elExists)
                    {
                        elExists = list.Remove(element);
                    }
                }
                else
                {
                    int element = int.Parse(command[1]);
                    int position = int.Parse(command[2]);
                    list.Insert(position, element);
                }
                input = Console.ReadLine();
            }

            Console.WriteLine(String.Join(' ', list));
        }
    }
}
