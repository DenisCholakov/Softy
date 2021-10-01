using System;
using System.Linq;

namespace SecretChat
{
    class Program
    {
        static void Main(string[] args)
        {
            string message = Console.ReadLine();

            string[] command = Console.ReadLine().Split(":|:");
            while (command[0] != "Reveal")
            {
                switch (command[0])
                {
                    case "InsertSpace":
                        int index = int.Parse(command[1]);
                        message = message.Insert(index, " ");
                        break;
                    case "Reverse":
                        string substring = command[1];
                        if (message.IndexOf(substring) == -1)
                        {
                            System.Console.WriteLine("error");
                            command = Console.ReadLine().Split(":|:");
                            continue;
                        }
                        message = message.Remove(message.IndexOf(substring), substring.Length);
                        message += String.Concat(substring.Reverse());
                        break;
                    case "ChangeAll":
                        string substringToReplace = command[1];
                        string replacement = command[2];
                        message = message.Replace(substringToReplace, replacement);
                        break;
                }
                System.Console.WriteLine(message);
                command = Console.ReadLine().Split(":|:");
            }

            System.Console.WriteLine($"You have a new text message: {message}");
        }
    }
}
