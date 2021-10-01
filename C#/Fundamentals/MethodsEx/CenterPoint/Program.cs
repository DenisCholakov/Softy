using System;

namespace CenterPoint
{
    class Program
    {
        static void Main(string[] args)
        {
            double x1 = double.Parse(Console.ReadLine());
            double y1 = double.Parse(Console.ReadLine());
            double x2 = double.Parse(Console.ReadLine());
            double y2 = double.Parse(Console.ReadLine());

            double dist1 = CalculateDistance(x1, y1);
            double dist2 = CalculateDistance(x2, y2);

            double x = dist1 < dist2 ? x1 : x2;
            double y = dist1 < dist2 ? y1 : y2;

            Console.WriteLine($"({x}, {y})");
        }

        private static double CalculateDistance(double x, double y)
        {
            return Math.Sqrt((x * x + y * y));
        }
    }
}
