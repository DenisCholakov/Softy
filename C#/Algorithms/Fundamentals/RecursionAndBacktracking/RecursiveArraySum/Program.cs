using System;
using System.IO;
using System.Linq;

namespace RecursiveArraySum
{
    class Program
    {
        private static int[] arr;
        static void Main(string[] args)
        {
            arr = Console.ReadLine().Split().Select(int.Parse).ToArray();

            int result = RecursiveSum(0);

            Console.WriteLine(result);
        }

        private static int RecursiveSum(int index)
        {
            if (index >= arr.Length - 1)
            {
                return arr[index];
            }

            return arr[index] + RecursiveSum(index + 1);
        }
    }
}
