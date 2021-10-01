using System;

namespace _5.Login
{
    class Program
    {
        static void Main(string[] args)
        {
            string name = Console.ReadLine();
            string password = string.Empty;
            
            for (int i = name.Length - 1; i >= 0; i--)
            {
                password += name[i];
            }

            string inputpass = Console.ReadLine();

            int counter = 1;
            while (inputpass != password && counter < 4)
            {
                Console.WriteLine("Incorrect password. Try again.");
                inputpass = Console.ReadLine();
                counter++;
            }

            if (inputpass == password)
            {
                Console.WriteLine($"User {name} logged in.");
            }
            else
            {
                Console.WriteLine($"User {name} blocked!");
            }
        }
    }
}
