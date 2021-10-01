using System;
using System.Collections.Generic;
using System.Linq;

namespace MixedUpLists
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> list1 = Console.ReadLine().Split().Select(int.Parse).ToList();
            List<int> list2 = Console.ReadLine().Split().Select(int.Parse).ToList();

            List<int> merged = new List<int>();
            int length = list1.Count < list2.Count ? list1.Count : list2.Count;
            for (int i = 0; i < length; i++)
            {
                merged.Add(list1[i]);
                merged.Add(list2[list2.Count - i - 1]);
            }

            int maxValue = int.MinValue;
            int minValue = int.MaxValue;

            if (list1.Count > list2.Count)
            {
                maxValue = list1[^1] > list1[^2] ? list1[^1] : list1[^2];
                minValue = list1[^1] < list1[^2] ? list1[^1] : list1[^2];
            }
            else
            {
                maxValue = list2[0] > list2[1] ? list2[0] : list2[1];
                minValue = list2[0] < list2[1] ? list2[0] : list2[1];
            }

            for (int i = 0; i < merged.Count; i++)
            {
                if (merged[i] >= maxValue || merged[i] <= minValue)
                {
                    merged.RemoveAt(i);
                    i--;
                }
            }

            merged.Sort();
            Console.WriteLine(String.Join(' ', merged));
        }
    }
}
