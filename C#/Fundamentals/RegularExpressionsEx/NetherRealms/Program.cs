using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace NetherRealms
{
    class Program
    {
        static void Main(string[] args)
        {
            SortedDictionary<string, double[]> participants = new SortedDictionary<string, double[]>();
            string[] names = Console.ReadLine().Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var name in names.OrderBy(x => x))
            {
                var healthMatch = Regex.Matches(name, @"[^0-9\/*\.+-]");
                string chars = String.Concat(healthMatch);

                int health = 0;
                foreach (var ch in chars)
                {
                    health += ch;
                }

                double attack = Regex.Matches(name, @"-?[0-9]+\.?\d*").Select(x => double.Parse(x.Value)).Sum();

                foreach (var sign in name)
                {
                    if (sign == '*')
                    {
                        attack *= 2;
                    }
                    else if (sign == '/')
                    {
                        attack /= 2;
                    }
                }

                participants.Add(name, new double[] { health, attack });
            }

            foreach (var pair in participants)
            {
                System.Console.WriteLine($"{pair.Key} - {pair.Value[0]} health, {pair.Value[1]:f2} damage");
            }
        }
    }
}