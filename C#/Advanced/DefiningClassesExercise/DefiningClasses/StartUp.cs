using System;
using System.IO;

namespace DefiningClasses
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            Family people = new Family();

            for (int i = 0; i < n; i++)
            {
                string[] input = Console.ReadLine().Split();
                string name = input[0];
                int age = int.Parse(input[1]);
                people.AddMember(new Person(name, age));
            }

            //var olderPeople = people.GetOlderThan30();
            //olderPeople.ForEach(x => Console.WriteLine($"{x.Name} - {x.Age}"));

            Console.WriteLine(people.GetOldestPerson());
        }
    }
}
