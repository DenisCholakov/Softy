using System;

namespace DataTypes
{
    class Program
    {
        static void Main(string[] args)
        {
            string dataType = Console.ReadLine();
            string input = Console.ReadLine();

            if (dataType == "int")
            {
                PrintNumber(int.Parse(input));
            }
            else if (dataType == "real")
            {
                PrintNumber(double.Parse(input));
            }
            else if (dataType == "string")
            {
                Console.WriteLine("$" + input + "$");
            }
            else
            {
                Console.WriteLine("Invalid data type!");
            }
        }

        private static void PrintNumber(double parse)
        {
            Console.WriteLine($"{parse * 1.5:f2}");
        }

        private static void PrintNumber(int parse)
        {
            Console.WriteLine(parse * 2);
        }
    }
}
