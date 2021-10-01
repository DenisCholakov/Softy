using System;
using System.Collections.Generic;
using System.Linq;

namespace OldestFamilyMember
{
    class Program
    {
        static void Main(string[] args)
        {
            var family = new Family();
            int n = int.Parse(Console.ReadLine());

            for (int i = 0; i < n; i++)
            {
                string[] input = Console.ReadLine().Split().ToArray();
                family.AddMember(new Person(input[0], int.Parse(input[1])));
            }

            System.Console.WriteLine(family.GetOldestPerson());
        }
    }

    class Family
    {
        public List<Person> People { get; set; }

        public void AddMember(Person member)
        {
            this.People.Add(new Person(member.Name, member.Age));
        }

        public Person GetOldestPerson()
        {
            var oldestPerson = this.People[0];

            foreach (var person in this.People)
            {
                if (oldestPerson.Age < person.Age)
                {
                    oldestPerson = person;
                }
            }
            return oldestPerson;
        }

        public Family()
        {
            this.People = new List<Person>();
        }
    }

    class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }

        public Person(string name, int age)
        {
            this.Name = name;
            this.Age = age;
        }

        public override string ToString() => $"{this.Name} {this.Age}";
    }
}
