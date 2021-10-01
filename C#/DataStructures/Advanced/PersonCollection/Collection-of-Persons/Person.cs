using System;

namespace Collection_of_Persons
{
    public class Person : IComparable
    {
        public Person()
        {

        }
        public Person(string email, string name, int age, string town)
        {
            Email = email;
            Name = name;
            Age = age;
            Town = town;
        }

        public string Email { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Town { get; set; }

        public int CompareTo(object obj)
        {
            if (obj is Person person)
            {
                return this.Town.CompareTo(person.Town);
            }

            return 1;
        }
    }
}
