using System;

namespace _2.Division
{
    class Program
    {
        static void Main(string[] args)
        {
            int num = int.Parse(Console.ReadLine());
            int div = 0;
            if (num%10 == 0)
            {
                div = 10;
            }
            else if (num%7 == 0)
            {
                div = 7;
            }
            else if (num%6 == 0)
            {
                div = 5;
            }
            else if (num%3 == 0)
            {
                div = 3;
            }
            else if (num%2 == 0)
            {
                div = 2;
            }

            if (div == 0)
            {
                Console.WriteLine("Not divisible");
            }
            else
            {
                Console.WriteLine($"The number is divisible by {div}");
            }
        }
    }
}
