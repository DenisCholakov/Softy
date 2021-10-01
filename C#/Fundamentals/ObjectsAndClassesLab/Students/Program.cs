using System;
using System.Collections.Generic;
using System.Linq;

namespace Students
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Student> students = new List<Student>();

            string[] input = Console.ReadLine().Split().ToArray();
            while (input[0] != "end")
            {
                Student student = new Student();
                student.FirstName = input[0];
                student.LastName = input[1];
                student.Age = int.Parse(input[2]);
                student.Hometown = input[3];

                int index = students.FindIndex(x => x.FirstName == student.FirstName
                                                    && x.LastName == student.LastName);
                if (index >= 0)
                {
                    students.RemoveAt(index);
                    students.Insert(index, student);
                }
                else
                {
                    students.Add(student);
                }

                input = Console.ReadLine().Split().ToArray();
            }

            string cityName = Console.ReadLine();

            foreach (var student in students)
            {
                if (student.Hometown == cityName)
                {
                    Console.WriteLine($"{student.FirstName} {student.LastName} is {student.Age} years old.");
                }
            }
        }
    }

    public class Student
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Hometown { get; set; }
    }
}
