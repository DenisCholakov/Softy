using MilitaryElite.Enumerations;
using MilitaryElite.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MilitaryElite.Models
{
    public class Mission : IMission
    {
        public Mission(string codeName, MissionStateEnum missionStateEnum)
        {
            CodeName = codeName;
            MissionStateEnum = missionStateEnum;
        }

        public string CodeName { get; }

        public MissionStateEnum MissionStateEnum { get; private set; }

        public void CompleteMission()
        {
            this.MissionStateEnum = MissionStateEnum.Finished;
        }

        public override string ToString() => $"  Code Name: {this.CodeName} State: {this.MissionStateEnum}";
    }
}
