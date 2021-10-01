using System;
using System.Collections.Generic;
using System.IO;

namespace MaximumTsksAsignment
{
    class Program
    {
        private static int[,] graph;
        private static int[] parents;
        static void Main(string[] args)
        {
            int peopleCount = int.Parse(Console.ReadLine());
            int tasksCount = int.Parse(Console.ReadLine());

            var nodes = peopleCount + tasksCount + 2;
            graph = new int[nodes, nodes];
            parents = new int[nodes];

            var start = 0;
            var target = nodes - 1;

            for (int person = 1; person <= peopleCount; person++)
            {
                graph[start, person] = 1;
                var personTasks = Console.ReadLine();

                for (int i = 0; i < personTasks.Length; i++)
                {
                    if (personTasks[i] == 'Y')
                    {
                        graph[person, peopleCount + 1 + i] = 1;
                    }
                }
            }

            for (int task = peopleCount + 1; task < nodes - 1; task++)
            {
                graph[task, target] = 1;
            }

            while (BFS(start, target))
            {
                var node = target;

                while (node != start)
                {
                    var parent = parents[node];

                    graph[parent, node] = 0;
                    graph[node, parent] = 1;

                    node = parent;
                }
            }

            for (int person = 1; person <= peopleCount; person++)
            {
                for (int task = peopleCount + 1; task <= peopleCount + tasksCount; task++)
                {
                    if (graph[task, person] > 0)
                    {
                        Console.WriteLine($"{(char)(64 + person)}-{task - peopleCount}");
                    }
                }
            }
        }

        private static bool BFS(int start, int target)
        {
            var visited = new bool[graph.GetLength(0)];
            var queue = new Queue<int>();

            visited[start] = true;
            queue.Enqueue(start);

            while (queue.Count > 0)
            {
                var node = queue.Dequeue();

                if (node == target)
                {
                    return true;
                }

                for (int child = 1; child < graph.GetLength(1); child++)
                {
                    if (!visited[child] && graph[node, child] > 0)
                    {
                        queue.Enqueue(child);
                        parents[child] = node;
                        visited[child] = true;
                    }
                }
            }

            return false;
        }
    }
}
