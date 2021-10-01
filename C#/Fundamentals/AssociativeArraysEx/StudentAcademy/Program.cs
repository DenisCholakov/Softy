using System;
using System.Linq;
using System.Collections.Generic;

namespace StudentAcademy
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            Dictionary<string, List<double>> studentGrades = new Dictionary<string, List<double>>();

            for (int i = 0; i < n; i++)
            {
                string name = Console.ReadLine();
                double grade = double.Parse(Console.ReadLine());

                if (studentGrades.ContainsKey(name))
                {
                    studentGrades[name].Add(grade);
                }
                else
                {
                    studentGrades.Add(name, new List<double> { grade });
                }
            }

            var StudentGradesAvg = new Dictionary<string, double>();

            foreach (var pair in studentGrades)
            {
                StudentGradesAvg.Add(pair.Key, pair.Value.Average());
            }

            foreach (var pair in StudentGradesAvg.Where(x => x.Value >= 4.5).OrderByDescending(x => x.Value))
            {
                System.Console.WriteLine($"{pair.Key} -> {pair.Value:f2}");
            }
        }
    }
}
