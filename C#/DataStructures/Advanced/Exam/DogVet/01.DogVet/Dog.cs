namespace _01.DogVet
{
    using System;

    public class Dog : IComparable<Dog>
    {
        public Dog(string id, string name, Breed breed, int age, int vaccines)
        {
            this.Id = id;
            this.Name = name;
            this.Breed = breed;
            this.Age = age;
            this.Vaccines = vaccines;
        }

        public string Id { get; set; }
        public string Name { get; set; }

        public Breed Breed { get; set; }

        public int Age { get; set; }

        public int Vaccines { get; set; }

        public Owner Owner { get; set; }

        public int CompareTo(Dog other)
        {
            int result = this.Age.CompareTo(other.Age);

            if (result == 0)
            {
                result = this.Name.CompareTo(other.Name);
            }

            if (result == 0)
            {
                result = this.Owner.Name.CompareTo(other.Owner.Name);
            }

            return result;
        }

        public override bool Equals(object obj)
        {
            if (obj is Dog other)
            {
                return this.Id == other.Id;
            }

            return false;
        }
    }
}