using System;
using System.Collections.Generic;
using System.Linq;

namespace AppendArrays
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] arrays = Console.ReadLine().Split('|');

            List<int> arr = new List<int>();
            for (int i = arrays.Length - 1; i >= 0; i--)
            {
                arr.AddRange(arrays[i].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList());
            }

            Console.WriteLine(String.Join(' ', arr));
        }
    }
}
