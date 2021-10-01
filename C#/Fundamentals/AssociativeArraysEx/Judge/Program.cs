using System;
using System.Linq;
using System.Collections.Generic;

namespace Judge
{
    class Program
    {
        static void Main(string[] args)
        {
            var subjects = new Dictionary<string, Dictionary<string, int>>();
            string input = Console.ReadLine();

            while (input != "no more time")
            {
                string[] publication = input.Split(" -> ");
                string participant = publication[0];
                string subject = publication[1];
                int points = int.Parse(publication[2]);
                if (subjects.ContainsKey(subject))
                {
                    if (!subjects[subject].ContainsKey(participant))
                    {
                        subjects[subject].Add(participant, points);
                    }
                    else
                    {
                        if (subjects[subject][participant] < points)
                        {
                            subjects[subject][participant] = points;
                        }
                    }
                }
                else
                {
                    subjects.Add(subject, new Dictionary<string, int>());
                    subjects[subject].Add(participant, points);
                }
                input = Console.ReadLine();
            }

            var individualStandings = new Dictionary<string, int>();

            foreach (var subject in subjects)
            {
                System.Console.WriteLine($"{subject.Key}: {subject.Value.Count} participants");
                int counter = 1;
                foreach (var participant in subject.Value.OrderByDescending(x => x.Value).ThenBy(x => x.Key))
                {
                    if (!individualStandings.ContainsKey(participant.Key))
                    {
                        individualStandings.Add(participant.Key, participant.Value);
                    }
                    else
                    {
                        individualStandings[participant.Key] += participant.Value;
                    }
                    System.Console.WriteLine($"{counter}. {participant.Key} <::> {participant.Value}");
                    counter++;
                }
            }

            System.Console.WriteLine("Individual standings:");

            int count = 1;
            foreach (var pair in individualStandings.OrderByDescending(x => x.Value).ThenBy(x => x.Key))
            {
                System.Console.WriteLine($"{count}. {pair.Key} -> {pair.Value}");
                count++;
            }
        }
    }
}
