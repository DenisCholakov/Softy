using System;
using System.Collections.Generic;

namespace SoftuniParty
{
    class Program
    {
        static void Main(string[] args)
        {
            HashSet<string> vipQuests = new HashSet<string>();
            HashSet<string> quests = new HashSet<string>();
            string input = Console.ReadLine();

            while (input != "PARTY")
            {
                if (input.Length != 8)
                {
                    input = Console.ReadLine();
                    continue;
                }

                if (char.IsNumber(input[0]))
                {
                    vipQuests.Add(input);
                }
                else
                {
                    quests.Add(input);
                }

                input = Console.ReadLine();
            }

            input = Console.ReadLine();

            while (input != "END")
            {
                if (input.Length != 8)
                {
                    input = Console.ReadLine();
                    continue;
                }

                if (char.IsNumber(input[0]))
                {
                    vipQuests.Remove(input);
                }
                else
                {
                    quests.Remove(input);
                }

                input = Console.ReadLine();
            }

            Console.WriteLine(vipQuests.Count + quests.Count);
            if (vipQuests.Count != 0)
            {
                Console.WriteLine(String.Join(Environment.NewLine, vipQuests));
            }

            if (quests.Count != 0)
            {
                Console.WriteLine(String.Join(Environment.NewLine, quests));
            }
        }
    }
}
