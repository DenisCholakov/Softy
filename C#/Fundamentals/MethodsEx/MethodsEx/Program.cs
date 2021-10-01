using System;

namespace MethodsEx
{
    class Program
    {
        static void Main(string[] args)
        {
            int num1 = int.Parse(Console.ReadLine());
            int num2 = int.Parse(Console.ReadLine());
            int num3 = int.Parse(Console.ReadLine());

            Console.WriteLine(SmallestNum(num1, num2, num3));
        }

        private static int SmallestNum(int a, int b, int c)
        {
            int num = a < b ? a : b;
            return num < c ? num : c;
        }
    }
}
