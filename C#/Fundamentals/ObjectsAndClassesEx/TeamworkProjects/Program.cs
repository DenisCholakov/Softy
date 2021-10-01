using System;
using System.Collections.Generic;
using System.Linq;

namespace TeamworkProjects
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Team> teams = new List<Team>();
            int n = int.Parse(Console.ReadLine());
            string input = String.Empty;
            for (int i = 0; i < n; i++)
            {
                input = Console.ReadLine();
                string[] teamInfo = input.Trim().Split('-').ToArray();
                if (teams.FindIndex(x => x.Creator == teamInfo[0]) >= 0)
                {
                    System.Console.WriteLine($"{teamInfo[0]} cannot create another team!");
                }
                else if (teams.FindIndex(x => x.Name == teamInfo[1]) >= 0)
                {
                    System.Console.WriteLine($"Team {teamInfo[1]} was already created!");
                }
                else
                {
                    teams.Add(new Team(teamInfo[0], teamInfo[1]));
                    System.Console.WriteLine($"Team {teamInfo[1]} has been created by {teamInfo[0]}!");
                }
            }

            input = Console.ReadLine();
            List<string> assignees = new List<string>();
            while (input != "end of assignment")
            {
                string[] newAssignee = input.Split("->").ToArray();
                int index = teams.FindIndex(x => x.Name == newAssignee[1]);
                if (index >= 0)
                {
                    if (assignees.Contains(newAssignee[0]) || teams.FindIndex(x => x.Creator == newAssignee[0]) >= 0)
                    {
                        System.Console.WriteLine($"Member {newAssignee[0]} cannot join team {newAssignee[1]}!");
                    }
                    else
                    {
                        assignees.Add(newAssignee[0]);
                        teams[index].Members.Add(newAssignee[0]);
                    }

                }
                else
                {
                    System.Console.WriteLine($"Team {newAssignee[1]} does not exist!");
                }
                input = Console.ReadLine();
            }

            List<Team> disbandeddTeams = teams.Where(x => x.Members.Count == 0).OrderBy(x => x.Name).ToList();
            List<Team> validTeams = teams.Where(x => x.Members.Count > 0).OrderByDescending(x => x.Members.Count)
                        .ThenBy(x => x.Name).ToList();

            foreach (var team in validTeams)
            {
                team.Print();
            }

            System.Console.WriteLine("Teams to disband:");
            foreach (var team in disbandeddTeams)
            {
                System.Console.WriteLine(team.Name);
            }
        }
    }

    public class Team
    {
        public string Creator { get; set; }
        public string Name { get; set; }
        public List<string> Members { get; set; }

        public Team()
        {
        }

        public Team(string creator, string name)
        {
            this.Creator = creator;
            this.Name = name;
            this.Members = new List<string>();
        }

        public void Print()
        {
            System.Console.WriteLine(this.Name);
            System.Console.WriteLine($"- {this.Creator}");
            foreach (var member in this.Members)
            {
                System.Console.WriteLine($"-- {member}");
            }
        }
    }
}
