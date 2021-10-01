using System;
using System.IO;
using System.Linq;

namespace TriFunction
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            string[] names = Console.ReadLine().Split();

            Func<string, int, bool> isLessOrEqualASCII = (x, y) => x.Select(p => (int)p).Sum() >= y;

            Func<string[], int, Func<string, int, bool>, string> firstName = (x, y, func) =>
            {
                for (int i = 0; i < x.Length; i++)
                {
                    if (func(x[i], y))
                    {
                        return x[i];
                    }
                }

                return null;
            };

            Console.WriteLine(firstName(names, n, isLessOrEqualASCII));
        }
    }
}
