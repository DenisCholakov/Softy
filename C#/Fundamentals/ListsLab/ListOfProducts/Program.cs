using System;

namespace ListOfProducts
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            string[] products = new string[n];

            for (int i = n - 1; i >= 0; i--)
            {
                products[i] = Console.ReadLine();
            }

            Array.Sort(products);

            for (int i = 0; i < n; i++)
            {
                Console.WriteLine($"{i + 1}.{products[i]}");
            }
        }
    }
}
