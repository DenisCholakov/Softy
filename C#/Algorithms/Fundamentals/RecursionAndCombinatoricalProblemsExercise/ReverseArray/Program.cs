using System;
using System.IO;

namespace ReverseArray
{
    class Program
    {
        private static string[] elements; 
        static void Main(string[] args)
        {
            elements = Console.ReadLine().Split();

            Reverse(0);

            Console.WriteLine(String.Join(' ', elements));
        }

        private static void Reverse(int index)
        {
            if (index >= elements.Length / 2)
            {
                return;
            }

            Swap(index, elements.Length - (index + 1));

            Reverse(index + 1);
        }

        private static void Swap(int fisrt, int second)
        {
            var temp = elements[fisrt];
            elements[fisrt] = elements[second];
            elements[second] = temp;
        }
    }
}
