using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;

namespace PredicateParty
{
    class Program
    {
        static void Main(string[] args)
        {
            Action<List<string>, Predicate<string>> doubleElements = (x, func) =>
            {
                for (int i = 0; i < x.Count; i++)
                {
                    if (func(x[i]))
                    {
                        x.Insert(i, x[i]);
                        i++;
                    }
                }
            };

            List<string> names = Console.ReadLine().Split().ToList();
            string input = Console.ReadLine();

            while (input != "Party!")
            {
                string[] command = input.Split();
                string action = command[0];
                string condition = command[1];
                string conditionVar = command[2];

                Predicate<string> predicate = null;

                switch (condition)
                {
                    case "StartsWith": predicate = x => x.StartsWith(conditionVar);
                        break;
                    case "EndsWith": predicate = x => x.EndsWith(conditionVar);
                        break;
                    case "Length": predicate = x => x.Length == int.Parse(conditionVar);
                        break;
                }

                if (command[0] == "Remove")
                {
                    names.RemoveAll(predicate);
                }
                else
                {
                    doubleElements(names, predicate);
                }

                input = Console.ReadLine();
            }

            if (names.Count != 0)
            {
                Console.WriteLine($"{String.Join(", ", names)} are going to the party!");
            }
            else
            {
                Console.WriteLine("Nobody is going to the party!");
            }
            
        }
    }
}
