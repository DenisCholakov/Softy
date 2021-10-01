using System;
using System.Text.RegularExpressions;

namespace MatchPhoneNumbers
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = Console.ReadLine();
            Regex validNumbers = new Regex(@"\+359([ -])2\1\d{3}\1\d{4}\b");
            var result = validNumbers.Matches(input);
            System.Console.WriteLine(String.Join(", ", result));
        }
    }
}
