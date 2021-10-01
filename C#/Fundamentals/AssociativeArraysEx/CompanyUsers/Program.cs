using System;
using System.Linq;
using System.Collections.Generic;

namespace CompanyUsers
{
    class Program
    {
        static void Main(string[] args)
        {
            SortedDictionary<string, List<string>> companies = new SortedDictionary<string, List<string>>();
            string input = Console.ReadLine();

            while (input != "End")
            {
                string companyName = input.Split(" -> ")[0];
                string employeeId = input.Split(" -> ")[1];

                if (companies.ContainsKey(companyName))
                {
                    if (!companies[companyName].Contains(employeeId))
                    {
                        companies[companyName].Add(employeeId);
                    }
                }
                else
                {
                    companies.Add(companyName, new List<string>() { employeeId });
                }
                input = Console.ReadLine();
            }

            foreach (var pair in companies)
            {
                System.Console.WriteLine(pair.Key);
                pair.Value.ForEach(x => System.Console.WriteLine($"-- {x}"));
            }
        }
    }
}
