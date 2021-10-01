using System;
using System.Collections.Generic;
using System.Linq;

namespace AnonymousThreat
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> input = Console.ReadLine().Split().ToList();

            string[] command = Console.ReadLine().Split();

            while (command[0] != "3:1")
            {
                if (command[0] == "merge")
                {
                    int startIndex = int.Parse(command[1]);
                    int endIndex = int.Parse(command[2]);
                    Merge(startIndex, endIndex, input);
                }
                else if (command[0] == "divide")
                {
                    int index = int.Parse(command[1]);
                    int partitions = int.Parse(command[2]);
                    if (partitions != 0)
                    {
                        Divide(index, partitions, input);
                    }
                }
                command = Console.ReadLine().Split();
            }

            Console.WriteLine(String.Join(' ', input));
        }

        private static void Divide(int index, int partitions, List<string> input)
        {
            string partitiondata = input[index];
            int length = partitiondata.Length / partitions;
            int reminder = partitiondata.Length % partitions;
            int startIndex = 0;
            List<string> parts = new List<string>();
            input.RemoveAt(index);
            

            for (int i = 0; i < partitions; i++)
            {
                parts.Add(partitiondata.Substring(startIndex, length));
                startIndex += length;
            }

            if (reminder != 0)
            {
                parts[parts.Count - 1] += partitiondata.Substring(startIndex, partitiondata.Length - startIndex);
            }

            input.InsertRange(index, parts);
        }

        private static void Merge(int startIndex, int endIndex, List<string> input)
        {
            if (startIndex < 0)
            {
                startIndex = 0;
            }
            else if (startIndex >= input.Count - 1)
            {
                return;
            }

            if (endIndex > input.Count -1)
            {
                endIndex = input.Count - 1;
            }

            int length = endIndex - startIndex;
            for (int i = 0; i < length; i++)
            {
                input[startIndex] += input[startIndex + 1];
                input.RemoveAt(startIndex + 1);
            }
        }
    }
}
