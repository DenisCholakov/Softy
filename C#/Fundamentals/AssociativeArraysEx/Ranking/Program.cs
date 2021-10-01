using System;
using System.Linq;
using System.Collections.Generic;

namespace Ranking
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, string> contests = new Dictionary<string, string>();
            string input = Console.ReadLine();

            while (input != "end of contests")
            {
                string[] contest = input.Split(':');
                string name = contest[0];
                string password = contest[1];
                contests.Add(name, password);
                input = Console.ReadLine();
            }

            SortedDictionary<string, Dictionary<string, int>> candidates = new SortedDictionary<string, Dictionary<string, int>>();
            input = Console.ReadLine();

            while (input != "end of submissions")
            {
                string[] submission = input.Split("=>");
                string contest = submission[0];
                string password = submission[1];
                if (contests.ContainsKey(contest))
                {
                    if (contests[contest] == password)
                    {
                        string username = submission[2];
                        int points = int.Parse(submission[3]);
                        if (candidates.ContainsKey(username))
                        {
                            if (!candidates[username].ContainsKey(contest))
                            {
                                candidates[username].Add(contest, points);
                            }
                            else if (candidates[username][contest] < points)
                            {
                                candidates[username][contest] = points;
                            }
                        }
                        else
                        {
                            candidates.Add(username, new Dictionary<string, int>());
                            candidates[username].Add(contest, points);
                        }
                    }
                }
                input = Console.ReadLine();
            }

            var scorePoints = new Dictionary<int, string>();

            foreach (var candidate in candidates)
            {
                scorePoints.Add(candidate.Value.Sum(x => x.Value), candidate.Key);
            }

            int bestPoints = scorePoints.Max(x => x.Key);

            System.Console.WriteLine($"Best candidate is {scorePoints[bestPoints]} with total {bestPoints} points.");
            System.Console.WriteLine("Ranking: ");

            foreach (var candidate in candidates)
            {
                System.Console.WriteLine(candidate.Key);
                foreach (var pair in candidate.Value.OrderByDescending(x => x.Value))
                {
                    System.Console.WriteLine($"#  {pair.Key} -> {pair.Value}");
                }
            }
        }
    }
}
