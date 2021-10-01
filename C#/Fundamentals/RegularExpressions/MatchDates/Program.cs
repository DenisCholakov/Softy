using System;
using System.Text.RegularExpressions;

namespace MatchDates
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = Console.ReadLine();
            string pattern = @"\b(?<day>\d{2})(?<separator>.)(?<month>[A-Z][a-z]{2})\k<separator>(?<year>\d{4})\b";
            var dateMatches = Regex.Matches(input, pattern);
            foreach (Match date in dateMatches)
            {
                System.Console.WriteLine($"Day: {date.Groups["day"].Value}, Month: {date.Groups["month"].Value}, Year: {date.Groups["year"].Value}");
            }
        }
    }
}
