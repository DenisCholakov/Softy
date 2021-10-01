using System;

namespace LongerLine
{
    class Program
    {
        static void Main(string[] args)
        {
            double[] firstArrow = ReadArrowCoord();
            double[] secondArrow = ReadArrowCoord();

            double length1 = FindArrowLength(firstArrow);
            double length2 = FindArrowLength(secondArrow);

            if (length1 >= length2)
            {
                Print(firstArrow);
            }
            else
            {
                Print(secondArrow);
            }
        }

        private static void Print(double[] arrow)
        {
            double dist1 = FindArrowLength(new double[] {0, 0, arrow[0], arrow[1]});
            double dist2 = FindArrowLength(new double[] {0, 0, arrow[2], arrow[3]});

            if (dist1 <= dist2)
            {
                Console.WriteLine($"({arrow[0]}, {arrow[1]})({arrow[2]}, {arrow[3]})");
            }
            else
            {
                Console.WriteLine($"({arrow[2]}, {arrow[3]})({arrow[0]}, {arrow[1]})");
            }
        }

        private static double FindArrowLength(double[] arrow)
        {
            double x = arrow[0] - arrow[2];
            double y = arrow[1] - arrow[3];
            return Math.Sqrt(x * x + y * y);
        }

        private static double[] ReadArrowCoord()
        {
            double[] coord = new double[4];
            coord[0] = double.Parse(Console.ReadLine()); // x1
            coord[1] = double.Parse(Console.ReadLine()); // y1
            coord[2] = double.Parse(Console.ReadLine()); // x2
            coord[3] = double.Parse(Console.ReadLine()); // y2

            return coord;
        }
    }
}