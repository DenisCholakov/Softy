using System;
using System.Collections.Generic;

namespace ValidUsernames
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] usernames = Console.ReadLine().Split(", ");
            List<string> validUsernames = new List<string>();

            foreach (var name in usernames)
            {
                if (name.Length >= 3 && name.Length <= 16)
                {
                    bool isValid = true;
                    for (int i = 0; i < name.Length; i++)
                    {
                        if (!(char.IsLetterOrDigit(name[i]) || name[i] == '-' || name[i] == '_'))
                        {
                            isValid = false;
                        }
                    }
                    if (isValid)
                    {
                        validUsernames.Add(name);
                    }
                }
            }

            foreach (var username in validUsernames)
            {
                System.Console.WriteLine(username);
            }
        }
    }
}
