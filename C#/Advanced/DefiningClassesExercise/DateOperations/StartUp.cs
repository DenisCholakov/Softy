using System;
using System.IO;

namespace DateOperations
{
    class StartUp
    {
        static void Main(string[] args)
        {
            string firstDate = Console.ReadLine();
            string secondDate = Console.ReadLine();
            int daysDifference = DateModifier.GetDayDifference(firstDate, secondDate);
            Console.WriteLine(daysDifference);
        }
    }
}
