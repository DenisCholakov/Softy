namespace Problem04.BalancedParentheses
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class BalancedParenthesesSolve : ISolvable
    {
        public bool AreBalanced(string parentheses)
        {
            char[] open = new char[] { '(', '[', '{' };
            Stack<char> openParentheses = new Stack<char>();

            for (int i = 0; i < parentheses.Length; i++)
            {
                if (open.Contains(parentheses[i]))
                {
                    openParentheses.Push(parentheses[i]);
                }
                else
                {
                    if (openParentheses.Count == 0)
                    {
                        return false;
                    }

                    char bracket = openParentheses.Pop();

                    if ((bracket == '(' && parentheses[i] == ')') || (bracket == '[' && parentheses[i] == ']') || (bracket == '{' && parentheses[i] == '}'))
                    {
                        continue;
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
