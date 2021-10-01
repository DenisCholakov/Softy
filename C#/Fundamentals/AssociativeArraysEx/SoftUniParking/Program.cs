using System;
using System.Collections.Generic;

namespace SoftUniParking
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, string> users = new Dictionary<string, string>();
            int n = int.Parse(Console.ReadLine());

            string input = string.Empty;

            for (int i = 0; i < n; i++)
            {
                input = Console.ReadLine();
                string[] command = input.Split();
                string username = command[1];
                if (command[0] == "register")
                {
                    if (users.ContainsKey(username))
                    {
                        System.Console.WriteLine($"ERROR: already registered with plate number {users[username]}");
                    }
                    else
                    {
                        users.Add(username, command[2]);
                        System.Console.WriteLine($"{username} registered {command[2]} successfully");
                    }
                }
                else
                {
                    if (users.ContainsKey(username))
                    {
                        users.Remove(username);
                        System.Console.WriteLine($"{username} unregistered successfully");
                    }
                    else
                    {
                        System.Console.WriteLine($"ERROR: user {username} not found");
                    }
                }
            }

            foreach (var pair in users)
            {
                System.Console.WriteLine($"{pair.Key} => {pair.Value}");
            }
        }
    }
}
