using System;
using System.IO;

namespace ActionPoint
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] names = Console.ReadLine().Split();

            Action<string[]> printer = x => Console.WriteLine(String.Join(Environment.NewLine, x));

            printer(names);
        }
    }
}
