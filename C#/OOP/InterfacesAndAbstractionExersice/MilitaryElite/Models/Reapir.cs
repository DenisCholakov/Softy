using MilitaryElite.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MilitaryElite.Models
{
    class Reapir : IRepair
    {
        public Reapir()
        {
        }
        public Reapir(string partName, int hoursWorked)
        {
            PartName = partName;
            HoursWorked = hoursWorked;
        }

        public string PartName { get; private set; }

        public int HoursWorked { get; private set; }

        public override string ToString() => $"  Part Name: {this.PartName} Hours Worked: {this.HoursWorked}";

    }
}
