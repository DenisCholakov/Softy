using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleTextEditor
{
    class Program
    {
        static void Main(string[] args)
        {
            StringBuilder sb = new StringBuilder();
            Stack<string> stack = new Stack<string>();
            int numOfOperations = int.Parse(Console.ReadLine());

            for (int i = 0; i < numOfOperations; i++)
            {
                string[] input = Console.ReadLine().Split();
                int operation = int.Parse(input[0]);

                switch (operation)
                {
                    case 1: stack.Push(sb.ToString());
                        sb.Append(input[1]);
                        break;
                    case 2: stack.Push(sb.ToString());
                        int numOfCharsToRemove = int.Parse(input[1]);
                        sb.Remove(sb.Length - numOfCharsToRemove, numOfCharsToRemove);
                        break;
                    case 3: int index = int.Parse(input[1]);
                        Console.WriteLine(sb[index - 1]);
                        break;
                    case 4: sb = new StringBuilder(stack.Pop());
                        break;
                }
            }
        }
    }
}
