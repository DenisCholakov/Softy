using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] expression = Console.ReadLine().Split().Reverse().ToArray();
            Stack<string> stack = new Stack<string>(expression);

            while (stack.Count > 1)
            {
                //PrintStack(stack);
                int firstNum = int.Parse(stack.Pop());
                string sign = stack.Pop();
                int secondNum = int.Parse(stack.Pop());

                if (sign == "+")
                {
                    int result = firstNum + secondNum;
                    stack.Push(result.ToString());
                }
                else if (sign == "-")
                {
                    int result = firstNum - secondNum;
                    stack.Push(result.ToString());
                }
            }

            Console.WriteLine(stack.Pop());
        }

        static void PrintStack(Stack<string> stack)
        {
            foreach (var item in stack)
            {
                Console.Write(item + " ");
            }

            Console.WriteLine();
        }
    }
}
