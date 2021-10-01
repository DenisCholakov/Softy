using System;
using System.Collections.Generic;
using System.Linq;

namespace ThePartyReservationFilterModule
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, Predicate<string>> filters = new Dictionary<string, Predicate<string>>();
            List<string> invitations = Console.ReadLine().Split().ToList();
            string input = Console.ReadLine();

            while (input != "Print")
            {
                string[] command = input.Split(';');
                string action = command[0];
                string condition = command[1];
                string conditionVal = command[2];
                Predicate<string> predicate = null;

                switch (condition)
                {
                    case "Starts with":
                        predicate = x => x.StartsWith(conditionVal);
                        break;
                    case "Ends with":
                        predicate = x => x.EndsWith(conditionVal);
                        break;
                    case "Length":
                        predicate = x => x.Length == int.Parse(conditionVal);
                        break;
                    case "Contains":
                        predicate = x => x.Contains(conditionVal);
                        break;
                }

                string filter = condition + " " + conditionVal;

                if (action == "Add filter")
                {

                    if (!filters.ContainsKey(filter))
                    {
                        filters.Add(filter, predicate);
                    }
                }
                else
                {
                    filters.Remove(filter);
                }

                input = Console.ReadLine();
            }

            foreach (var filter in filters)
            {
                invitations.RemoveAll(filter.Value);
            }

            Console.WriteLine(String.Join(' ', invitations));
        }
    }
}
