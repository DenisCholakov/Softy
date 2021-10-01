using System;

namespace CustomDataStructuresExercise
{
    class StartUp
    {
        static void Main(string[] args)
        {
            CustomStack stack = new CustomStack();

            for (int i = 1; i <= 5; i++)
            {
                stack.Push(i);
            }

            while (stack.Count != 0)
            {
                Console.WriteLine(stack.Pop());
            }
        }
    }
}
