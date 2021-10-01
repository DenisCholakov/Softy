using System;

namespace _3.Vacation
{
    class Program
    {
        static void Main(string[] args)
        {
            int groupCount = int.Parse(Console.ReadLine());
            string groupType = Console.ReadLine();
            string dayOfWeek = Console.ReadLine();
            double price = 0;

            switch (dayOfWeek)
            {
                case "Friday":
                    if (groupType == "Students")
                    {
                        price = groupCount * 8.45;
                    }
                    else if (groupType == "Business")
                    {
                        price = groupCount * 10.9;
                    }
                    else if (groupType == "Regular")
                    {
                        price = groupCount * 15;
                    }
                    break;

                    case "Saturday":
                    if (groupType == "Students")
                    {
                        price = groupCount * 9.8;
                    }
                    else if (groupType == "Business")
                    {
                        price = groupCount * 15.6;
                    }
                    else if (groupType == "Regular")
                    {
                        price = groupCount * 20;
                    }
                    break;

                    case "Sunday":
                    if (groupType == "Students")
                    {
                        price = groupCount * 10.46;
                    }
                    else if (groupType == "Business")
                    {
                        price = groupCount * 16;
                    }
                    else if (groupType == "Regular")
                    {
                        price = groupCount * 22.50;
                    }
                    break;
            }

            if (groupType == "Students" && groupCount >= 30)
            {
                price *= 0.85;
            }
            else if (groupType == "Business" && groupCount >= 100)
            {
                price /= groupCount;
                price = (groupCount - 10) * price;
            }
            else if (groupType == "Regular" && groupCount >= 10 && groupCount <= 20)
            {
                price *= 0.95;
            }

            Console.WriteLine($"Total price: {price:f2}");
        }
    }
}
