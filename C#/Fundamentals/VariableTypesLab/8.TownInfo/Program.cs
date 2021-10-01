using System;

namespace _8.TownInfo
{
    class Program
    {
        static void Main(string[] args)
        {
            string townName = Console.ReadLine();
            uint townPopulation = uint.Parse(Console.ReadLine());
            int townArea = int.Parse(Console.ReadLine());

            Console.WriteLine($"Town {townName} has population of {townPopulation} and area {townArea} square km.");
        }
    }
}
