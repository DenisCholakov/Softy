using System;
using System.Collections.Generic;
using System.Linq;

namespace OrderByAge
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Person> people = new List<Person>();

            string[] input = Console.ReadLine().Split().ToArray();

            while (input[0] != "End")
            {
                people.Add(new Person(input[0], input[1], int.Parse(input[2])));
                input = Console.ReadLine().Split().ToArray();
            }

            people.OrderBy(x => x.Age).ToList().ForEach(x => System.Console.WriteLine(x));
        }
    }

    public class Person
    {
        public string Name { get; set; }
        public string ID { get; set; }
        public int Age { get; set; }

        public Person(string name, string id, int age)
        {
            this.Name = name;
            this.ID = id;
            this.Age = age;
        }

        public override string ToString() => $"{this.Name} with ID: {this.ID} is {this.Age} years old.";
    }
}
