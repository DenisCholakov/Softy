using System;
using System.Collections.Generic;
using System.IO;

namespace EqualityLogic
{
    class Program
    {
        static void Main(string[] args)
        {
            SortedSet<Person> sortedSetPeople = new SortedSet<Person>();
            HashSet<Person> hashSetPople = new HashSet<Person>();
            int n = int.Parse(Console.ReadLine());

            for (int i = 0; i < n; i++)
            {
                string[] personInfo = Console.ReadLine().Split();
                string name = personInfo[0];
                int age = int.Parse(personInfo[1]);
                Person person = new Person(name, age);
                sortedSetPeople.Add(person);
                hashSetPople.Add(person);
            }

            Console.WriteLine(sortedSetPeople.Count);
            Console.WriteLine(hashSetPople.Count);
        }
    }
}
