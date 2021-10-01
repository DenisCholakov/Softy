using System;
using System.Linq;
using System.Collections.Generic;

namespace Courses
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, List<string>> courses = new Dictionary<string, List<string>>();

            string input = Console.ReadLine();
            while (input != "end")
            {
                string[] course = input.Split(" : ");
                string courseName = course[0];
                string studentName = course[1];
                if (courses.ContainsKey(courseName))
                {
                    courses[courseName].Add(studentName);
                }
                else
                {
                    courses.Add(courseName, new List<string>());
                    courses[courseName].Add(studentName);
                }
                input = Console.ReadLine();
            }

            courses = courses.OrderByDescending(x => x.Value.Count)
                                .ToDictionary(x => x.Key, x => x.Value);

            foreach (var pair in courses)
            {
                System.Console.WriteLine($"{pair.Key}: {pair.Value.Count}");
                pair.Value.OrderBy(x => x).ToList()
                                .ForEach(x => System.Console.WriteLine($"-- {x}"));
            }
        }
    }
}
