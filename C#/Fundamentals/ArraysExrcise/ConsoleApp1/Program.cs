using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] arr1 = Console.ReadLine().Split();
            string[] arr2 = Console.ReadLine().Split();

            foreach (var el1 in arr1)
            {
                foreach (var el2 in arr2)
                {
                    if (el1 == el2)
                    {
                        Console.Write(el1 + " ");
                    }
                }
            }
        }
    }
}
