﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FootballTeamGenerator
{
    class Team
    {
        private string name;
        private ICollection<Player> players;

        private Team()
        {
            this.players = new List<Player>();
        }

        public Team(string name) : this()
        {
            this.Name = name;
        }

        public string Name
        {
            get { return name; }
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("A name should not be empty.");
                } 
                
                name = value; 
            }
        }

        public int Rating => this.players.Count > 0 ? (int)Math.Round(this.players.Average(p => p.OverallSkill)) : 0;

        public void AddPlayer(Player player)
        {
            this.players.Add(player);
        }

        public void RemovePlayer(string playerName)
        {
            var playerToRemove = this.players.FirstOrDefault(p => p.Name == playerName);

            if (playerToRemove == null)
            {
                throw new InvalidOperationException($"Player {playerName} is not in {this.Name} team.");
            }

            this.players.Remove(playerToRemove);
        }

        public override string ToString()
        {
            return $"{this.Name} - {this.Rating}";
        }
    }
}
