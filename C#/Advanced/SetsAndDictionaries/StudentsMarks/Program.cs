using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StudentsMarks
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, List<decimal>> grades = new Dictionary<string, List<decimal>>();

            int n = int.Parse(Console.ReadLine());

            for (int i = 0; i < n; i++)
            {
                string[] input = Console.ReadLine().Split();
                string name = input[0];
                decimal grade = decimal.Parse(input[1]);

                if (!grades.ContainsKey(name))
                {
                    grades.Add(name, new List<decimal>());
                }

                grades[name].Add(grade);
            }

            foreach (var pair in grades)
            {
                StringBuilder allGrades = new StringBuilder();

                for (int i = 0; i < pair.Value.Count; i++)
                {
                    allGrades.Append($"{pair.Value[i]:f2} ");
                }

                Console.WriteLine($"{pair.Key} -> {allGrades.ToString().Trim()} (avg: {pair.Value.Average():f2})");
            }
        }
    }
}
