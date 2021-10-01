using System;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;

namespace PasswordValidator
{
    class Program
    {
        static void Main(string[] args)
        {
            string password = Console.ReadLine();

            bool isValid = CheckForValidPassword(password);

            if (isValid)
            {
                Console.WriteLine("Password is valid");
            }
        }

        private static bool CheckForValidPassword(string password)
        {
            bool isValid = true;

            if (password.Length > 10 || password.Length < 6)
            {
                Console.WriteLine("Password must be between 6 and 10 characters");
                isValid = false;
            }

            if (!password.All(c => Char.IsLetterOrDigit(c)))
            {
                Console.WriteLine("Password must consist only of letters and digits");
                isValid = false;
            }

            Regex reg = new Regex(@"(.*\d){2}");
            if (!reg.IsMatch(password))
            {
                Console.WriteLine("Password must have at least 2 digits");
                isValid = false;
            }

            return isValid;
        }
    }
}
