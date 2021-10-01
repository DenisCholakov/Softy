using System;
using System.Text.RegularExpressions;

namespace SoftuniBarIncome
{
    class Program
    {
        static void Main(string[] args)
        {
            string pattern = @"%(?<customer>[A-Z][a-z]+)%[^|$%.]*?<(?<product>\w+)>[^|$%.]*?\|(?<qty>\d+)\|[^|$%.]*?(?<price>\d+\.?\d*)\$";
            string input = Console.ReadLine();

            double totalIncome = 0;
            while (input != "end of shift")
            {
                Match order = Regex.Match(input, pattern);

                if (order.Success)
                {
                    string customer = order.Groups["customer"].Value;
                    string product = order.Groups["product"].Value;
                    int qty = int.Parse(order.Groups["qty"].Value);
                    double price = double.Parse(order.Groups["price"].Value);
                    totalIncome += (price * qty);

                    System.Console.WriteLine($"{customer}: {product} - {(price * qty):f2}");
                }

                input = Console.ReadLine();
            }

            System.Console.WriteLine($"Total income: {totalIncome:f2}");
        }
    }
}
