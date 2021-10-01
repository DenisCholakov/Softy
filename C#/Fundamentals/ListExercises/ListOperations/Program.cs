using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace ListOperations
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> nums = Console.ReadLine().Split().Select(int.Parse).ToList();

            string input = Console.ReadLine();

            while (input != "End")
            {
                string[] command = input.Split();
                if (command[0] == "Add")
                {
                    int element = int.Parse(command[1]);
                    nums.Add(element);
                }
                else if (command[0] == "Insert")
                {
                    int element = int.Parse(command[1]);
                    int index = int.Parse(command[2]);
                    if (index >= nums.Count || index < 0)
                    {
                        Console.WriteLine("Invalid index");
                        input = Console.ReadLine();
                        continue;
                    }
                    nums.Insert(index, element);
                }
                else if (command[0] == "Remove")
                {
                    int index = int.Parse(command[1]);
                    if (index >= nums.Count || index < 0)
                    {
                        Console.WriteLine("Invalid index");
                        input = Console.ReadLine();
                        continue;
                    }
                    nums.RemoveAt(index);
                }
                else
                {
                    if (command[1] == "left")
                    {
                        int count = int.Parse(command[2]);
                        for (int i = 0; i < count; i++)
                        {
                            int firstNum = nums[0];
                            nums.Add(firstNum);
                            nums.RemoveAt(0);
                        }
                    }
                    else
                    {
                        int count = int.Parse(command[2]);
                        for (int i = 0; i < count; i++)
                        {
                            int lastItem = nums[^1];
                            nums.RemoveAt(nums.Count - 1);
                            nums.Insert(0, lastItem);
                        }
                    }
                }
                input = Console.ReadLine();
            }

            Console.WriteLine(String.Join(' ', nums));
        }
    }
}
