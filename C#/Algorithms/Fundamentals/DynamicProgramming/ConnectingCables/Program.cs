using System;
using System.IO;
using System.Linq;

namespace ConnectingCables
{
    class Program
    {
        static void Main(string[] args)
        {
            var nums = Console.ReadLine().Split().Select(int.Parse).ToArray();
            var positions = new int[nums.Length];

            for (int i = 0; i < positions.Length; i++)
            {
                positions[i] = i + 1;
            }

            var table = new int[nums.Length + 1, positions.Length + 1];

            for (int r = 1; r < table.GetLength(0); r++)
            {
                for (int c = 1; c < table.GetLength(1); c++)
                {
                    if (nums[r - 1] == positions[c - 1])
                    {
                        table[r, c] = table[r - 1, c - 1] + 1;
                    }
                    else
                    {
                        table[r, c] = Math.Max(table[r - 1, c], table[r, c - 1]);
                    }
                }
            }

            Console.WriteLine($"Maximum pairs connected: {table[nums.Length, nums.Length]}");
        }
    }
}
