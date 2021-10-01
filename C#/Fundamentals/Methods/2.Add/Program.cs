using System;

namespace _2.Add
{
    class Program
    {
        static void Main(string[] args)
        {
            double grade = double.Parse(Console.ReadLine());
            Grade(grade);
        }

        static void Grade(double grade)
        {
            if (grade < 3)
            {
                Console.WriteLine("Fail");
            }
            else if (grade < 3.5)
            {
                Console.WriteLine("Poor");
            }
            else if (grade < 4.5)
            {
                Console.WriteLine("Good");
            }
            else if (grade < 5.5)
            {
                Console.WriteLine("Very good");
            }
            else
            {
                Console.WriteLine("Excellent");
            }
        }
    }
}
