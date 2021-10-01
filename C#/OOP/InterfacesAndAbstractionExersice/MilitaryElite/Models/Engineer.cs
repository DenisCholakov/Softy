using MilitaryElite.Enumerations;
using MilitaryElite.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MilitaryElite.Models
{
    class Engineer : SpecialisedSoldier, IEngineer
    {
        public Engineer(int id, 
            string firstName, 
            string lastName, 
            decimal salary, 
            SoldierCorpsEnum soldierCorps,
            ICollection<IRepair> repairs)
            : base(id, firstName, lastName, salary, soldierCorps)
        {
            this.Repairs = repairs;
        }

        public ICollection<IRepair> Repairs { get; }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            result.AppendLine(base.ToString());
            result.AppendLine("Repairs:");

            foreach (var repair in this.Repairs)
            {
                result.AppendLine(repair.ToString());
            }

            return result.ToString().TrimEnd();
        }
    }
}
