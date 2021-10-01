using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DefiningClasses
{
    public class Family
    {
        private List<Person> people;

        public Family()
        {
            this.people = new List<Person>();
        }

        public List<Person> People
        {
            get { return people; }
        }

        public void AddMember(Person member)
        {
            this.people.Add(member);
        }

        public Person GetOldestPerson()
        {
            return this.people.OrderByDescending(x => x.Age).First();
        }

        public List<Person> GetOlderThan30()
        {
            return this.people.Where(x => x.Age > 30).OrderBy(x => x.Name).ToList();
        }
    }
}
