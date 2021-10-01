using System;
using System.Text.RegularExpressions;

namespace ExtractEmails
{
    class Program
    {
        static void Main(string[] args)
        {
            string pattern = @"(?<=\s)([A-Za-z0-9]+[._-]*\w*)@([a-z]+([.-][a-z]+)*\.[a-z]{2,})";
            string input = Console.ReadLine();
            var emails = Regex.Matches(input, pattern);

            foreach (Match email in emails)
            {
                System.Console.WriteLine(email.Value);
            }
        }
    }
}
