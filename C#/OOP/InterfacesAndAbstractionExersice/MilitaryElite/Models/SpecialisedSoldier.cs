using MilitaryElite.Enumerations;
using MilitaryElite.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MilitaryElite.Models
{
    public class SpecialisedSoldier : Private, ISpecialisedSoldier
    {
        public SpecialisedSoldier(int id, 
            string firstName, 
            string lastName, 
            decimal salary,
            SoldierCorpsEnum soldierCorps) 
            : base(id, firstName, lastName, salary)
        {
            this.SoldierCorp = soldierCorps;
        }

        public SoldierCorpsEnum SoldierCorp { get; }

        public override string ToString() => base.ToString() + Environment.NewLine + $"Corps: {this.SoldierCorp}";
    }
}
