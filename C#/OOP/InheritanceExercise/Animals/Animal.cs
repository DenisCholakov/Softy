using System;
using System.Collections.Generic;
using System.Text;

namespace Animals
{
    abstract class Animal
    {
        private string name;
        private int age;
        private string gender;
        protected Animal(string name, int age, string gender)
        {
            this.Name = name;
            this.Age = age;
            this.Gender = gender;
        }

        public string Name { get; set; }
        public int Age 
        {
            get { return this.age; }
            set 
            {
                if (value < 0 )
                {
                    throw new ArgumentException("Invalid input!");
                }

                age = value;
            } 
        }
        public string Gender 
        {
            get { return this.gender; }
            set
            {
                if (value != "Male" && value != "Female")
                {
                    throw new ArgumentException("Invalid input!");
                }

                gender = value;
            } 
        }

        public abstract string ProduceSound();

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"{this.GetType().Name}");
            sb.AppendLine($"{this.Name} {this.Age} {this.Gender}");
            sb.AppendLine(this.ProduceSound());
            return sb.ToString().TrimEnd();
        }

    }
}
