using System;
using System.Collections.Generic;

using MilitaryElite.Interfaces;
using MilitaryElite.Models;
using MilitaryElite.Enumerations;

namespace MilitaryElite
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            ICollection<ISoldier> soldiers = new List<ISoldier>();
            Dictionary<int, IPrivate> privatesById = new Dictionary<int, IPrivate>();

            string command;

            while ((command = Console.ReadLine()) != "End")
            {
                string[] commandTokens = command.Split();
                string soldierType = commandTokens[0];
                int soldierId = int.Parse(commandTokens[1]);
                string soldierFirstName = commandTokens[2];
                string soldierLastName = commandTokens[3];

                if (soldierType == typeof(Private).Name)
                {
                    decimal soldierSalary = decimal.Parse(commandTokens[4]);
                    var soldier = new Private(soldierId, soldierFirstName, soldierLastName, soldierSalary);
                    soldiers.Add(soldier);
                    privatesById.Add(soldierId, soldier);
                }
                else if (soldierType == typeof(LieutenantGeneral).Name)
                {
                    List<IPrivate> privates = new List<IPrivate>();
                    decimal soldierSalary = decimal.Parse(commandTokens[4]);

                    for (int i = 5; i < commandTokens.Length; i++)
                    {
                        int privateId = int.Parse(commandTokens[i]);
                        privates.Add(privatesById[privateId]);

                    }

                    var soldier = new LieutenantGeneral(soldierId, soldierFirstName, soldierLastName, soldierSalary, privates);
                    soldiers.Add(soldier);
                    privatesById.Add(soldierId, soldier);
                }
                else if (soldierType == typeof(Engineer).Name)
                {
                    decimal soldierSalary = decimal.Parse(commandTokens[4]);
                    object result;

                    if (!Enum.TryParse(typeof(SoldierCorpsEnum), commandTokens[5], out result))
                    {
                        continue;
                    }

                    SoldierCorpsEnum corps = (SoldierCorpsEnum)result;
                    List<IRepair> repairs = new List<IRepair>();

                    for (int i = 6; i < commandTokens.Length; i += 2)
                    {
                        string partName = commandTokens[i];
                        int hoursWorked = int.Parse(commandTokens[i + 1]);
                        repairs.Add(new Reapir(partName, hoursWorked));
                    }

                    var soldier = new Engineer(soldierId, soldierFirstName, soldierLastName, soldierSalary, corps, repairs);
                    soldiers.Add(soldier);
                    privatesById.Add(soldierId, soldier);
                }
                else if (soldierType == typeof(Commando).Name)
                {
                    decimal soldierSalary = decimal.Parse(commandTokens[4]);
                    object result;

                    if (!Enum.TryParse(typeof(SoldierCorpsEnum), commandTokens[5], out result))
                    {
                        continue;
                    }

                    SoldierCorpsEnum corps = (SoldierCorpsEnum)result;
                    List<IMission> missions = new List<IMission>();

                    for (int i = 6; i < commandTokens.Length; i += 2)
                    {
                        string codeName = commandTokens[i];

                        if (!Enum.TryParse(typeof(MissionStateEnum), commandTokens[i + 1], out result))
                        {
                            continue;
                        }

                        MissionStateEnum missionState = (MissionStateEnum)result;
                        missions.Add(new Mission(codeName, missionState));
                    }

                    var soldier = new Commando(soldierId, soldierFirstName, soldierLastName, soldierSalary, corps, missions);
                    soldiers.Add(soldier);
                    privatesById.Add(soldierId, soldier);
                }
                else if (soldierType == typeof(Spy).Name)
                {
                    int codeNumber = int.Parse(commandTokens[4]);
                    var soldier = new Spy(soldierId, soldierFirstName, soldierLastName, codeNumber);
                    soldiers.Add(soldier);
                }
            }

            foreach (var soldier in soldiers)
            {
                Console.WriteLine(soldier);
            }
        }
    }
}
