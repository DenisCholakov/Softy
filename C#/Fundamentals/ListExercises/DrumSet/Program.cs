using System;
using System.Collections.Generic;
using System.Linq;

namespace DrumSet
{
    class Program
    {
        static void Main(string[] args)
        {
            decimal savings = decimal.Parse(Console.ReadLine());
            List<int> drumsInit = Console.ReadLine().Split().Select(int.Parse).ToList();

            List<int> drums = new List<int>();
            for (int i = 0; i < drumsInit.Count; i++)
            {
                drums.Add(drumsInit[i]);
            }

            string input = Console.ReadLine();
            while (input != "Hit it again, Gabsy!")
            {
                int hitPower = int.Parse(input);
                for (int i = 0; i < drums.Count; i++)
                {
                    drums[i] -= hitPower;
                    if (drums[i] <= 0)
                    {
                        if (savings >= drumsInit[i] * 3)
                        {
                            drums[i] = drumsInit[i];
                            savings -= drumsInit[i] * 3;
                        }
                        else
                        {
                            drums.RemoveAt(i);
                            drumsInit.RemoveAt(i);
                            i--;
                        }
                    }
                }
                input = Console.ReadLine();
            }

            Console.WriteLine(String.Join(' ', drums));
            Console.WriteLine($"Gabsy has {savings:f2}lv.");
        }
    }
}
