namespace Collection_of_Persons
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class PersonCollection : IPersonCollection
    {
        private Dictionary<string, Person> peopleByEmail = new Dictionary<string, Person>();
        private Dictionary<string, SortedSet<Person>> byEmailDomain = new Dictionary<string, SortedSet<Person>>();
        private Dictionary<string, SortedSet<Person>> byNameAndTown = new Dictionary<string, SortedSet<Person>>();
        private SortedDictionary<int, Dictionary<string, SortedSet<Person>>> byAgeTown =
                   new SortedDictionary<int, Dictionary<string, SortedSet<Person>>>();

        public bool AddPerson(string email, string name, int age, string town)
        {
            var p = FindPerson(email);

            if (p != null)
            {
                return false;
            }

            var person = new Person(email, name, age, town);
            peopleByEmail.Add(email, new Person(email, name, age, town));

            var emailDomain = email.Split('@')[1];
            byEmailDomain.AppendValueToKey(emailDomain, person);

            var nameAndTown = GetNameTown(person);
            byNameAndTown.AppendValueToKey(nameAndTown, person);

            byAgeTown.EnsureKeyExists(age);
            byAgeTown[age].AppendValueToKey(town, person);

            return true;
        }

        public int Count { get; }

        public Person FindPerson(string email)
        {
            if (peopleByEmail.ContainsKey(email))
            {
                return peopleByEmail[email];
            }

            return null;
        }

        public bool DeletePerson(string email)
        {
            var person = FindPerson(email);

            if (person != null)
            {
                peopleByEmail.Remove(email);

                var emailDomain = email.Split('@')[1];
                byEmailDomain.Remove(emailDomain);

                var nameAndTown = GetNameTown(person);
                byNameAndTown.Remove(nameAndTown);

                byAgeTown[person.Age][person.Town].Remove(person);

                return true;
            }

            return false;
        }

        public IEnumerable<Person> FindPersons(string emailDomain)
        {
            return byEmailDomain.GetValuesForKey(emailDomain);
        }

        public IEnumerable<Person> FindPersons(string name, string town)
        {
            var key = GetNameTown(name, town);
            return byNameAndTown.GetValuesForKey(key);
        }

        public IEnumerable<Person> FindPersons(int startAge, int endAge)
        {
            SortedSet<int> ages = new SortedSet<int>(byAgeTown.Keys);
            var resultKeys = ages.GetViewBetween(startAge, endAge);
            return resultKeys.SelectMany(k => byAgeTown[k].Values.SelectMany(v => v));
        }

        public IEnumerable<Person> FindPersons(int startAge, int endAge, string town)
        {
            SortedSet<int> ages = new SortedSet<int>(byAgeTown.Keys);
            var resultKeys = ages.GetViewBetween(startAge, endAge);
            return resultKeys.SelectMany(k => byAgeTown[k].GetValuesForKey(town));
        }

        private string GetNameTown(Person person)
        {
            return $"{person.Name}_{person.Town}";
        }

        private string GetNameTown(string name, string town)
        {
            return $"{name}_{town}";
        }
    }
}
