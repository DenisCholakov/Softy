using System;
using System.Collections.Generic;

namespace _01.DogVet
{
    public class Owner
    {
        public Owner(string id, string name)
        {
            this.Id = id;
            this.Name = name;
            dogs = new Dictionary<string, Dog>();
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public Dictionary<string, Dog> dogs { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is Owner other)
            {
                return this.Id == other.Id;
            }

            return false;
        }
    }
}