using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassroomProject
{
    public class Classroom
    {
        private List<Student> students;

        public Classroom(int capacity)
        {
            this.students = new List<Student>();
            this.Capacity = capacity;
        }

        public int Capacity { get; private set; }

        public int Count => this.students.Count;

        public string RegisterStudent(Student student)
        {
            if (this.Count >= this.Capacity)
            {
                return "No seats in the classroom";
            }
            else
            {
                students.Add(student);
                return $"Added student {student.FirstName} {student.LastName}";
            }
        }

        public string DismissStudent(string firstName, string lastName)
        {
            if(this.students.Remove(new Student(firstName, lastName, null)))
            {
                return $"Dismissed student {firstName} {lastName}";
            }
            else
            {
                return "Student not found";
            }
        }

        public string GetSubjectInfo(string subject)
        {
            StringBuilder sb = new StringBuilder();

            List<Student> subjectStudents = this.students.Where(x => x.Subject == subject).ToList();

            if (subjectStudents.Count == 0)
            {
                sb.AppendLine("No students enrolled for the subject");
            }
            else
            {
                sb.AppendLine($"Subject: {subject}");
                sb.AppendLine("Students:");

                foreach (var student in subjectStudents)
                {
                    sb.AppendLine($"{student.FirstName} {student.LastName}");
                }
            }

            return sb.ToString().TrimEnd();
        }

        public int GetStudentsCount() => this.Count;

        public Student GetStudent(string firstName, string lastName)
        {
            return this.students.FirstOrDefault(x => x.Equals(new Student(firstName, lastName, null))) ;
        }
    }
}
