using System;
using System.Linq;
using System.Collections.Generic;

namespace Students
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Student> students = new List<Student>();
            int n = int.Parse(Console.ReadLine());

            for (int i = 0; i < n; i++)
            {
                string[] input = Console.ReadLine().Split().ToArray();
                students.Add(new Student(input[0], input[1], double.Parse(input[2])));
            }

            System.Console.WriteLine(string.Join(Environment.NewLine, students
                .OrderByDescending(x => x.Grade)));
        }
    }

    public class Student
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public double Grade { get; set; }

        public Student(string firstName, string lastName, double grade)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Grade = grade;
        }

        public override string ToString() => $"{this.FirstName} {this.LastName}: {this.Grade:f2}";
    }
}
