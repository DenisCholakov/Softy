using MilitaryElite.Enumerations;

namespace MilitaryElite.Interfaces
{
    public interface IMission
    {
        public string CodeName { get; }
        public MissionStateEnum MissionStateEnum { get; }

        public void CompleteMission();
    }
}