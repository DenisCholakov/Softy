using System;
using System.Text.RegularExpressions;

namespace MatchFullName
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = Console.ReadLine();
            Regex fullNames = new Regex(@"\b[A-Z][a-z]+ [A-Z][a-z]+");
            var result = fullNames.Matches(input);

            System.Console.WriteLine(String.Join(' ', result));
        }
    }
}
