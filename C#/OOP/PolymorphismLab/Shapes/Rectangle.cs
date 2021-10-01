using System;
using System.Collections.Generic;
using System.Text;

namespace Shapes
{
    public class Rectangle : Shape
    {
        private double a;
        private double b;

        public Rectangle(double a, double b)
        {
            this.a = a;
            this.b = b;
        }

        public override double CalculateArea()
        {
            return a * b;
        }

        public override double CalculatePerimeter()
        {
            return 2 * (a + b);
        }

        public override string Draw()
        {
            StringBuilder result = new StringBuilder();

            int height = (int)a;
            int width = (int)b;

            DrawLine(width, '*', '*', result);

            for (int i = 1; i < height - 1; i++)
            {
                DrawLine(width, '*', ' ', result);
            }

            DrawLine(width, '*', '*', result);

            return result.ToString().TrimEnd();
        }

        private void DrawLine(int width, char end, char mid, StringBuilder sb)
        {
            sb.Append(end);

            for (int i = 1; i < width; i++)
            {
                sb.Append(mid);
            }

            sb.AppendLine(end.ToString());
        }
    }
}
