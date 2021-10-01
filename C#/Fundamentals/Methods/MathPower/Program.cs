using System;

namespace MathPower
{
    class Program
    {
        static void Main(string[] args)
        {
            double num = double.Parse(Console.ReadLine());
            int power = int.Parse(Console.ReadLine());

            Console.WriteLine(MathPower(num, power));
        }

        private static double MathPower(double num, int power)
        {
            double res = 1;
            for (int i = 1; i <= power; i++)
            {
                res*= num;
            }

            return res;
        }
    }
}
