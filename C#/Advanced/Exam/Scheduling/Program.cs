using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Scheduling
{
    class Program
    {
        static void Main(string[] args)
        {
            Stack<int> tasks = new Stack<int>(Console.ReadLine().Split(", ").Select(int.Parse).ToArray());
            Queue<int> threads = new Queue<int>(Console.ReadLine().Split().Select(int.Parse).ToArray());
            int taskToBeKilled = int.Parse(Console.ReadLine());

            while (tasks.Count > 0 && threads.Count > 0)
            {
                int currTask = tasks.Peek();
                int currThread = threads.Peek();

                if (currTask == taskToBeKilled)
                {
                    Console.WriteLine($"Thread with value {currThread} killed task {currTask}");
                    Console.WriteLine(String.Join(' ', threads));
                    break;
                }

                if (currThread >= currTask)
                {
                    tasks.Pop();
                }

                threads.Dequeue();
            }
        }
    }
}
