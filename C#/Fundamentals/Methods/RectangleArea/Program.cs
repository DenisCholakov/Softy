using System;

namespace RectangleArea
{
    class Program
    {
        static void Main(string[] args)
        {
            double height = double.Parse(Console.ReadLine());
            double width = double.Parse(Console.ReadLine());

            double area = RectangleArea(height, width);
            Console.WriteLine(area);
        }

        private static double RectangleArea(double h, double w)
        {
            return h * w;
        }
    }
}
