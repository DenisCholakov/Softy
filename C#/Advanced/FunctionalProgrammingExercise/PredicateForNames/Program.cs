using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PredicateForNames
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            List<string> names = Console.ReadLine().Split().ToList();
            names = names.Where(x => x.Length <= n).ToList();
            Console.WriteLine(String.Join(Environment.NewLine, names));
        }
    }
}
