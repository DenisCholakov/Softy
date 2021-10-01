using System;
using System.IO;
using System.Linq;

namespace ActionPoint
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] names = Console.ReadLine().Split();

            Action<string[]> printer = x =>
            {
                x = x.Select(y => $"Sir {y}").ToArray();
                Console.WriteLine(String.Join(Environment.NewLine, x));
            };

            printer(names);
        }
    }
}
