using System;
using System.Linq;
using System.Collections.Generic;

namespace SoftUniExamResults
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, int> participants = new Dictionary<string, int>();
            Dictionary<string, int> subscriptions = new Dictionary<string, int>();
            string input = Console.ReadLine();

            while (input != "exam finished")
            {
                string[] subscription = input.Split("-");
                string user = subscription[0];
                if (subscription[1] == "banned")
                {
                    participants.Remove(user);
                }
                else
                {
                    string languadge = subscription[1];
                    int points = int.Parse(subscription[2]);
                    if (participants.ContainsKey(user))
                    {
                        if (participants[user] < points)
                        {
                            participants[user] = points;
                        }
                    }
                    else
                    {
                        participants.Add(user, points);
                    }
                    if (subscriptions.ContainsKey(languadge))
                    {
                        subscriptions[languadge]++;
                    }
                    else
                    {
                        subscriptions.Add(languadge, 1);
                    }
                }
                input = Console.ReadLine();
            }

            System.Console.WriteLine("Results:");

            foreach (var pair in participants.OrderByDescending(x => x.Value).ThenBy(x => x.Key))
            {
                System.Console.WriteLine($"{pair.Key} | {pair.Value}");
            }

            System.Console.WriteLine("Submissions:");

            foreach (var pair in subscriptions.OrderByDescending(x => x.Value).ThenBy(x => x.Key))
            {
                System.Console.WriteLine($"{pair.Key} - {pair.Value}");
            }
        }
    }
}
