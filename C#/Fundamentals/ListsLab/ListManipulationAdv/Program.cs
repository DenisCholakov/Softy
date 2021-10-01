using System;
using System.Collections.Generic;
using System.Linq;

namespace ListManipulationAdv
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> nums = Console.ReadLine().Split().Select(int.Parse).ToList();
            string input = Console.ReadLine();
            bool madeChanges = false;

            while (input != "end")
            {
                string[] command = input.Split();
                if (command[0] == "Add")
                {
                    int element = int.Parse(command[1]);
                    nums.Add(element);
                    madeChanges = true;
                }
                else if (command[0] == "Remove")
                {
                    int element = int.Parse(command[1]);
                    nums.Remove(element);
                    madeChanges = true;
                }
                else if (command[0] == "RemoveAt")
                {
                    int index = int.Parse(command[1]);
                    nums.RemoveAt(index);
                    madeChanges = true;
                }
                else if (command[0] == "Insert")
                {
                    int element = int.Parse(command[1]);
                    int index = int.Parse(command[2]);
                    nums.Insert(index, element);
                    madeChanges = true;
                }
                else if (command[0] == "Contains")
                {
                    int element = int.Parse(command[1]);
                    if (nums.Contains(element))
                    {
                        Console.WriteLine("Yes");
                    }
                    else
                    {
                        Console.WriteLine("No such number");
                    }
                }
                else if (command[0] == "PrintEven")
                {
                    Console.WriteLine(String.Join(' ', nums.FindAll(x => x % 2 == 0)));
                }
                else if (command[0] == "PrintOdd")
                {
                    Console.WriteLine(String.Join(' ', nums.FindAll(x => x % 2 != 0)));
                }
                else if (command[0] == "GetSum")
                {
                    Console.WriteLine(nums.Sum());
                }
                else
                {
                    int number = int.Parse(command[2]);
                    switch (command[1])
                    {
                        case "<":
                            Console.WriteLine(String.Join(' ', nums.FindAll(x => x < number)));
                            break;
                        case ">":
                            Console.WriteLine(String.Join(' ', nums.FindAll(x => x > number)));
                            break;
                        case ">=":
                            Console.WriteLine(String.Join(' ', nums.FindAll(x => x >= number)));
                            break;
                        case "<=":
                            Console.WriteLine(String.Join(' ', nums.FindAll(x => x <= number)));
                            break;
                    }
                }

                input = Console.ReadLine();
            }

            if (madeChanges)
            {
                Console.WriteLine(String.Join(' ', nums));
            }
        }
    }
}
