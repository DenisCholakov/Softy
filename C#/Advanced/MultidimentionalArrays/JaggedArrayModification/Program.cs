using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace JaggedArrayModification
{
    class Program
    {
        static void Main(string[] args)
        {
            int[][] jaggedArray = new int[int.Parse(Console.ReadLine())][];

            for (int i = 0; i < jaggedArray.Length; i++)
            {
                jaggedArray[i] = Console.ReadLine().Split().Select(int.Parse).ToArray();
            }

            string input = Console.ReadLine();

            while (input != "END")
            {
                string[] command = input.Split();

                if (command[0] == "Add")
                {
                    int row = int.Parse(command[1]);
                    int col = int.Parse(command[2]);
                    int value = int.Parse(command[3]);

                    if (!IsValidIndex(row, col, jaggedArray))
                    {
                        Console.WriteLine("Invalid coordinates");
                        input = Console.ReadLine();
                        continue;
                    }

                    jaggedArray[row][col] += value;
                }
                else if (command[0] == "Subtract")
                {
                    int row = int.Parse(command[1]);
                    int col = int.Parse(command[2]);
                    int value = int.Parse(command[3]);

                    if (!IsValidIndex(row, col, jaggedArray))
                    {
                        Console.WriteLine("Invalid coordinates");
                        input = Console.ReadLine();
                        continue;
                    }

                    jaggedArray[row][col] -= value;
                }

                input = Console.ReadLine();
            }

            foreach (var line in jaggedArray)
            {
                Console.WriteLine(String.Join(' ', line));
            }
        }

        private static bool IsValidIndex(int row, int col, int[][] jaggedArray)
        {
            if (row >= jaggedArray.Length || row < 0)
            {
                return false;
            }
            else if (col >= jaggedArray[row].Length || col < 0)
            {
                return false;
            }

            return true;
        }
    }
}
