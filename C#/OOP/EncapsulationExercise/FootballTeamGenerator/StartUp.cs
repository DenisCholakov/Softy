using System;
using System.Collections.Generic;
using System.Linq;

namespace FootballTeamGenerator
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            var teams = new List<Team>();
            string command;

            while ((command = Console.ReadLine()) != "END")
            {
                try
                {
                    string[] commandArgs = command.Split(";", StringSplitOptions.RemoveEmptyEntries);
                    string commandType = commandArgs[0];
                    string teamName = commandArgs[1];

                    if (commandType == "Team")
                    {
                        teams.Add(new Team(teamName));
                    }
                    else if (commandType == "Add")
                    {
                        var team = teams.FirstOrDefault(t => t.Name == teamName);
                        ValidateTeam(teamName, team);
                        string playerName = commandArgs[2];
                        int[] stats = commandArgs.Skip(3).Select(int.Parse).ToArray();
                        int endurance = stats[0];
                        int sprint = stats[1];
                        int dribble = stats[2];
                        int passing = stats[3];
                        int shooting = stats[4];
                        var playerStats = new Stats(endurance, sprint, dribble, passing, shooting);
                        team.AddPlayer(new Player(playerName, playerStats));
                    }
                    else if (commandType == "Remove")
                    {
                        var team = teams.FirstOrDefault(t => t.Name == teamName);
                        ValidateTeam(teamName, team);
                        string playerName = commandArgs[2];
                        team.RemovePlayer(playerName);
                    }
                    else if (commandType == "Rating")
                    {
                        var team = teams.FirstOrDefault(t => t.Name == teamName);
                        ValidateTeam(teamName, team);
                        Console.WriteLine(team);
                    }
                }
                catch (ArgumentException ae)
                {
                    Console.WriteLine(ae.Message);
                }
                catch (InvalidOperationException ioe)
                {
                    Console.WriteLine(ioe.Message);
                }
            }
        }

        private static void ValidateTeam(string teamName, Team team)
        {
            if (team == null)
            {
                throw new InvalidOperationException($"Team {teamName} does not exist.");
            }
        }
    }
}
