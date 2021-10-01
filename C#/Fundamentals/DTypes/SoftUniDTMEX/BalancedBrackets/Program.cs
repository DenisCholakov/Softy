using System;
using System.Collections;
using System.Collections.Generic;

namespace BalancedBrackets
{
    class Program
    {
        static void Main(string[] args)
        {
            Stack<char> brackets = new Stack<char>();

            int n = int.Parse(Console.ReadLine());

            for (int i = 0; i < n; i++)
            {
                bool isBracket = char.TryParse(Console.ReadLine(), out char bracket);
                if (isBracket && (bracket == '(' || bracket == '{' || bracket == '[' || bracket == '<'))
                {
                    brackets.Push(bracket);
                }
                else if (isBracket && (bracket == ')' || bracket == '}' || bracket == ']' || bracket == '>'))
                {
                    if (brackets.Count != 0)
                    {
                        bool isBalanced = BracketsClosed(brackets.Pop(), bracket);

                        if (!isBalanced)
                        {
                            Console.WriteLine("BALANCED");
                            return;
                        }
                    }
                    else
                    {
                        Console.WriteLine("UNBALANCED");
                        return;
                    }
                }
            }

            if (brackets.Count == 0)
            {
                Console.WriteLine("BALANCED");
            }
        }

        static bool BracketsClosed(char a, char b)
        {
            return a == '(' && b == ')' || a == '{' && b == '}' || a == '[' && b == ']' || a == '<' && b == '>';
        }
    }
}
