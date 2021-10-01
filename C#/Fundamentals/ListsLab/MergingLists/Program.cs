using System;
using System.Collections.Generic;
using System.Linq;

namespace MergingLists
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> list1 = Console.ReadLine().Split().Select(int.Parse).ToList();
            List<int> list2 = Console.ReadLine().Split().Select(int.Parse).ToList();

            int length = list1.Count > list2.Count ? list1.Count : list2.Count;

            List<int> combined = new List<int>();
            for (int i = 0; i < length; i++)
            {
                if (i < list1.Count)
                {
                    combined.Add(list1[i]);
                }

                if (i < list2.Count)
                {
                    combined.Add(list2[i]);
                }
            }

            Console.WriteLine(String.Join(' ', combined));
        }
    }
}
