using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BoxOfT
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            Box<int> box = new Box<int>();

            for (int i = 0; i < n; i++)
            {
                box.Values.Add(int.Parse(Console.ReadLine()));
            }

            int[] indexesToSwap = Console.ReadLine().Split().Select(int.Parse).ToArray();

            box.Swap(indexesToSwap[0], indexesToSwap[1]);

            Console.WriteLine(box);
        }
    }
}
