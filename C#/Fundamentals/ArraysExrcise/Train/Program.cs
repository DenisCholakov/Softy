using System;
using System.Linq;

namespace Train
{
    class Program
    {
        static void Main(string[] args)
        {
            int wagonsNum = int.Parse(Console.ReadLine());
            int[] wagons = new int[wagonsNum];

            for (int i = 0; i < wagonsNum; i++)
            {
                wagons[i] = int.Parse(Console.ReadLine());
            }

            Console.WriteLine(String.Join(' ', wagons));
            Console.WriteLine(wagons.Sum());
        }
    }
}
