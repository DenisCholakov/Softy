using System;
using System.IO;
using System.Linq;

namespace CreateCustomStack
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            string input;
            CustomStack<int> stack = new CustomStack<int>();

            while ((input = Console.ReadLine()) != "END")
            {
                if (input.Contains("Push"))
                {
                    int[] numsToPush = input.Substring(4).Split(", ", StringSplitOptions.RemoveEmptyEntries)
                                .Select(int.Parse)
                                .ToArray();

                    for (int i = 0; i < numsToPush.Length; i++)
                    {
                        stack.Push(numsToPush[i]);
                    }
                }
                else if(input.Contains("Pop"))
                {
                    try
                    {
                        stack.Pop();
                    }
                    catch (InvalidOperationException e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            }

            for (int i = 0; i < 2; i++)
            {
                foreach (var num in stack)
                {
                    Console.WriteLine(num);
                }
            }
        }
    }
}
