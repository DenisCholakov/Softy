using System;

namespace _9.PadawanEquipment
{
    class Program
    {
        static void Main(string[] args)
        {
            double budget = double.Parse(Console.ReadLine());
            int studentsCount = int.Parse(Console.ReadLine());
            double lightSaberPrice = double.Parse(Console.ReadLine());
            double robesPrice = double.Parse(Console.ReadLine());
            double beltsPrice = double.Parse(Console.ReadLine());

            int lightSaberCount = (int)Math.Ceiling((double)studentsCount*1.1);
            int beltsCount = studentsCount - (studentsCount / 6);

            double price = lightSaberPrice * lightSaberCount + robesPrice * studentsCount + beltsPrice * beltsCount;

            if (budget >= price)
            {
                Console.WriteLine($"The money is enough - it would cost {price:f2}lv.");
            }
            else
            {
                Console.WriteLine($"Ivan Cho will need {(price - budget):f2}lv more.");
            }
        }
    }
}
