using System;
using System.Collections.Generic;
using System.Text;

namespace FootballTeamGenerator
{
    class Player
    {
        private string name;

        public Player(string name, Stats stats)
        {
            this.Name = name;
            this.Stats = stats;
        }

        public string Name
        {
            get { return name; }
            private set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("A name should not be empty.");
                }

                name = value; }
        }

        public Stats Stats { get; set; }

        public double OverallSkill => this.Stats.Average;

        public double SkillLevel()
        {
            return this.Stats.Average;
        }

    }
}
