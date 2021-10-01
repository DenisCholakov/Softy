using System;
using System.Collections.Generic;
using System.IO;

namespace ComparingObjects
{
    class StartUp
    {
        static void Main(string[] args)
        {
            string input;
            List<Person> people = new List<Person>();

            while ((input = Console.ReadLine()) != "END")
            {
                string[] personInfo = input.Split();
                string name = personInfo[0];
                int age = int.Parse(personInfo[1]);
                string town = personInfo[2];
                people.Add(new Person(name, age, town));
            }

            int searchIndex = int.Parse(Console.ReadLine());
            Person toFind = people[searchIndex - 1];
            int sameCount = 0;

            foreach (var person in people)
            {
                if (toFind.CompareTo(person) == 0)
                {
                    sameCount++;
                }
            }

            if (sameCount == 1)
            {
                Console.WriteLine("No matches");
            }
            else
            {
                Console.WriteLine($"{sameCount} {people.Count - sameCount} {people.Count}");
            }
        }
    }
}
