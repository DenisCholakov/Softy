using System;
using System.Linq;
using Microsoft.Win32.SafeHandles;

namespace SortArr
{
    class Program
    {
        // selection method
        static void Main(string[] args)
        {
            int[] arr = Console.ReadLine().Split().Select(int.Parse).ToArray();

            for (int i = 0; i < arr.Length - 1; i++)
            {
                int minInd = i;
                int temp = 0;
                for (int j = i + 1; j < arr.Length; j++)
                {
                    if (arr[j] < arr[minInd])
                    {
                        minInd = j;
                    }
                }

                if (i != minInd)
                {
                    temp = arr[i];
                    arr[i] = arr[minInd];
                    arr[minInd] = temp;
                }
            }

            Console.WriteLine(String.Join(' ', arr));
        }
    }
}
