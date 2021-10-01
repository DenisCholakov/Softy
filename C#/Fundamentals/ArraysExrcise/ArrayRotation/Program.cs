using System;
using System.Linq;
using System.Net.Http.Headers;

namespace ArrayRotation
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] arr = Console.ReadLine().Split().Select(int.Parse).ToArray();
            int rotations = int.Parse(Console.ReadLine());

            int temp = 0;
            for (int i = 0; i < rotations; i++)
            {
                temp = arr[0];
                for (int j = 1; j < arr.Length; j++)
                {
                    arr[j - 1] = arr[j];
                }

                arr[arr.Length - 1] = temp;
            }

            Console.WriteLine(String.Join(' ', arr));
        }
    }
}
