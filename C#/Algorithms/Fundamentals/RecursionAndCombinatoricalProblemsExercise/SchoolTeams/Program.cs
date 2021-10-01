using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SchoolTeams
{
    class Program
    {

        private const int girlsCount = 3;
        private const int boysCount = 2;

        static void Main(string[] args)
        {
            var girlsNames = Console.ReadLine().Split(", ");
            var boysNames = Console.ReadLine().Split(", ");

            string[] girlsCombArr = new string[girlsCount];
            string[] boysCombArr = new string[boysCount];

            var girlsList = new List<string[]>();
            var boysList = new List<string[]>();

            Combinations(0, 0, girlsNames, girlsCombArr, girlsCount, girlsList);
            Combinations(0, 0, boysNames, boysCombArr, boysCount, boysList);

            foreach (var girlsComb in girlsList)
            {
                foreach (var boysComb in boysList)
                {
                    Console.WriteLine($"{String.Join(", ", girlsComb)}, {String.Join(", ", boysComb)}");
                }
            }
        }

        private static void Combinations(int arrIndex, int combIndex, string[] names, 
                  string[] combination, int count, List<string[]> listOfComb)
        {
            if (combIndex >= count)
            {
                listOfComb.Add(combination.ToArray());
                return;
            }

            for (int i = arrIndex; i < names.Length; i++)
            {
                combination[combIndex] = names[i];
                Combinations(i + 1, combIndex + 1, names, combination, count, listOfComb);
            }
        }
    }
}
