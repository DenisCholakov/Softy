using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DistanceBetweenVertices
{
    class Program
    {
        private static Dictionary<int, List<int>> graph;

        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            int p = int.Parse(Console.ReadLine());

            graph = ReadGraph(n);

            for (int i = 0; i < p; i++)
            {
                var pair = Console.ReadLine().Split('-').Select(int.Parse).ToArray();
                int source = pair[0];
                int destination = pair[1];

                int distnace = FindDistance(source, destination);

                Console.WriteLine($"{{{source}, {destination}}} -> {distnace}");
            }
        }

        private static int FindDistance(int first, int second)
        {
            var queue = new Queue<int>();
            queue.Enqueue(first);
            var steps = new Dictionary<int, int> { { first, 0 } };

            while (queue.Count > 0)
            {
                var node = queue.Dequeue();

                if (node == second)
                {
                    return steps[node];
                }

                foreach (var child in graph[node])
                {
                    if (steps.ContainsKey(child))
                    {
                        continue;
                    }

                    queue.Enqueue(child);
                    steps[child] = steps[node] + 1;
                }
            }

            return -1;
        }

        private static Dictionary<int, List<int>> ReadGraph(int n)
        {
            var result = new Dictionary<int, List<int>>();

            for (int i = 0; i < n; i++)
            {
                var input = Console.ReadLine().Split(':');
                int node = int.Parse(input[0]);
                var children = new List<int>();
                if (!String.IsNullOrWhiteSpace(input[1]))
                {
                    children = input[1].Split().Select(int.Parse).ToList();
                }

                result[node] = children;
            }

            return result;
        }
    }
}
