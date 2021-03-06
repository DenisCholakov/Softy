using System;
using System.Collections.Generic;
using System.Text;

namespace Person
{
    class Child : Person
    {
        public Child(string name, int age) : base(name, age)
        {
        }

        public override int Age 
        {
            get
            {
                return base.Age;
            }
            set
            {
                if (value > 15)
                {
                    throw new ArgumentException("A child can not be older than 15 years old!");
                }

                base.Age = value;
            }
        }
    }
}
