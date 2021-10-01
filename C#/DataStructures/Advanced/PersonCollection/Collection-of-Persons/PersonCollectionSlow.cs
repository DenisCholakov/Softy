namespace Collection_of_Persons
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    public class PersonCollectionSlow : IPersonCollection
    {
        private List<Person> people = new List<Person>();

        public bool AddPerson(string email, string name, int age, string town)
        {
            var person = this.people.FirstOrDefault(p => p.Email == email);

            if (person == null)
            {
                people.Add(new Person()
                {
                    Email = email,
                    Age = age,
                    Name = name,
                    Town = town
                });
            }

            return person == null;
        }

        public int Count => this.people.Count;

        public Person FindPerson(string email)
        {
            return this.people.FirstOrDefault(p => p.Email == email);
        }

        public bool DeletePerson(string email)
        {
            var person = this.people.FirstOrDefault(p => p.Email == email);

            if (person != null)
            {
                this.people.Remove(person);
            }

            return person != null;
        }

        public IEnumerable<Person> FindPersons(string emailDomain)
        {
            Regex matcher = new Regex($"@({emailDomain})(?!\\S)");
            return people.Where(p => matcher.IsMatch(p.Email))
                .OrderBy(p => p.Email);
        }

        public IEnumerable<Person> FindPersons(string name, string town)
        {
            return people.Where(p => p.Name == name && p.Town == town)
                .OrderBy(p => p.Email);
        }

        public IEnumerable<Person> FindPersons(int startAge, int endAge)
        {
            return people.Where(p => p.Age >= startAge && p.Age <= endAge)
                .OrderBy(p => p.Age).ThenBy(p => p.Email);
        }

        public IEnumerable<Person> FindPersons(int startAge, int endAge, string town)
        {
            return people.Where(p => p.Age >= startAge && p.Age <= endAge && p.Town == town)
                .OrderBy(p => p.Age).ThenBy(p => p.Email);
        }
    }
}
