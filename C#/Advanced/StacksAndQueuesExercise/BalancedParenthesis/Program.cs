using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BalancedParenthesis
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = Console.ReadLine();
            Stack<char> stack = new Stack<char>();

            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == '(' || input[i] == '{' || input[i] == '[')
                {
                    stack.Push(input[i]);
                }
                else if (input[i] == ')' || input[i] == '}' || input[i] == ']')
                {

                    if (stack.Count == 0)
                    {
                        Console.WriteLine("NO");
                        return;
                    }

                    char openBracket = stack.Pop();

                    if (BracketsBalansed(openBracket, input[i]))
                    {
                        continue;
                    }

                    Console.WriteLine("NO");
                    return;
                }
            }

            Console.WriteLine("YES");
        }

        private static bool BracketsBalansed(char open, char closed)
        {
            return (open == '(' && closed == ')') || (open == '{' && closed == '}') || (open == '[' && closed == ']');
        }
    }
}
