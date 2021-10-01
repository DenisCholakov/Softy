using System;
using System.Linq;

namespace CountUpperCaseWords
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] words = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries)
                        .Where(x => x[0] == x.ToUpper()[0]).ToArray();

            Console.WriteLine(String.Join("\n", words));

        }
    }
}
