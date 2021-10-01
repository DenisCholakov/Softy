using System;
using System.Collections.Generic;       
using System.Linq;

namespace ListManupilationBasics
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> nums = Console.ReadLine().Split().Select(int.Parse).ToList();

            string input = Console.ReadLine();

            while (input != "end")
            {
                string[] command = input.Split();
                if (command[0] == "Add")
                {
                    int element = int.Parse(command[1]);
                    nums.Add(element);
                }
                else if (command[0] == "Remove")
                {
                    int element = int.Parse(command[1]);
                    nums.Remove(element);
                }
                else if (command[0] == "RemoveAt")
                {
                    int index = int.Parse(command[1]);
                    nums.RemoveAt(index);
                }
                else
                {
                    int element = int.Parse(command[1]);
                    int index = int.Parse(command[2]);
                    nums.Insert(index, element);
                }

                input = Console.ReadLine();
            }

            Console.WriteLine(String.Join(' ', nums));
        }
    }
}
