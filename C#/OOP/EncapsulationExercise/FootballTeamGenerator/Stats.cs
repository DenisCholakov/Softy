using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace FootballTeamGenerator
{
    class Stats
    {
        private const double STATS_COUNT = 5;

        private int endurance;
        private int sprint;
        private int dribble;
        private int passing;
        private int shooting;

        public Stats(int endurance, int sprint, int dribble, int passing, int shooting)
        {
            this.Endurance = endurance;
            this.Sprint = sprint;
            this.Dribble = dribble;
            this.Passing = passing;
            this.Shooting = shooting;
        }

        public int Endurance
        {
            get { return endurance; }
            private set
            {
                this.ValidateStat(value, nameof(Endurance));
                endurance = value;
            }
        }

        public int Sprint
        {
            get { return sprint; }
            private set
            {
                this.ValidateStat(value, nameof(Sprint));
                sprint = value;
            }
        }

        public int Dribble
        {
            get { return dribble; }
            private set
            {
                this.ValidateStat(value, nameof(Dribble));
                dribble = value;
            }
        }

        public int Passing
        {
            get { return this.passing; }
            set
            {
                this.ValidateStat(value, nameof(Passing));
                this.passing = value;
            }
        }

        public int Shooting
        {
            get { return shooting; }
            private set
            {
                this.ValidateStat(value, nameof(Shooting));
                shooting = value;
            }
        }

        public double Average => 
                   (this.Endurance + this.Sprint + this.Dribble + this.Passing + this.Shooting) / STATS_COUNT;

        private void ValidateStat(int value, string stat)
        {
            if (value < 0 || value > 100)
            {
                throw new ArgumentException(stat + " should be between 0 and 100.");
            }
        }
    }
}
