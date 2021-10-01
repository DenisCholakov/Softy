using System;
using System.Collections.Generic;
using System.Text;

namespace ClassroomProject
{
    public class Student
    {
        public Student(string firstName, string lastName, string subject)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Subject = subject;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Subject { get; set; }

        public override string ToString() => $"Student: First Name = {this.FirstName}, Last Name = {this.LastName}, Subject = {this.Subject}";

        public override bool Equals(object obj)
        {
            if (obj != null && obj is Student other)
            {
                return this.FirstName == other.FirstName && this.LastName == other.LastName;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.FirstName, this.LastName);
        }
    }
}
