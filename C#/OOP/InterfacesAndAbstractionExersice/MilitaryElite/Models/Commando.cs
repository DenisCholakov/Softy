using MilitaryElite.Enumerations;
using MilitaryElite.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MilitaryElite.Models
{
    public class Commando : SpecialisedSoldier, ICommando
    {
        public Commando(int id, string firstName, string lastName, decimal salary, SoldierCorpsEnum soldierCorps,
                        ICollection<IMission> missions) 
            : base(id, firstName, lastName, salary, soldierCorps)
        {
            this.Missions = missions;
        }

        public ICollection<IMission> Missions { get; }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            result.AppendLine(base.ToString());
            result.AppendLine("Missions:");

            foreach (var mission in this.Missions)
            {
                result.AppendLine(mission.ToString());
            }

            return result.ToString().TrimEnd();
        }
    }
}
