using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MAxFlow
{
    class Program
    {
        private static int[,] graph;
        private static int[] parents;
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            graph = ReadGraph(n);

            int source = int.Parse(Console.ReadLine());
            int destination = int.Parse(Console.ReadLine());

            parents = new int[n];
            Array.Fill(parents, -1);

            int maxFlow = 0;

            while (BFS(source, destination))
            {
                int currentFlow = GetCurrentFlow(source, destination);
                maxFlow += currentFlow;
                UpdateCapacities(source, destination, currentFlow);
            }

            Console.WriteLine($"Max flow = {maxFlow}");
        }

        private static void UpdateCapacities(int source, int destination, int flow)
        {
            var node = destination;

            while (node != source)
            {
                var parent = parents[node];
                graph[parent, node] -= flow;
                node = parent;
            }
        }

        private static int GetCurrentFlow(int source, int destination)
        {
            int minFlow = int.MaxValue;
            var node = destination;

            while (node != source)
            {
                var parent = parents[node];
                int flow = graph[parent, node];

                if (flow < minFlow)
                {
                    minFlow = flow;
                }

                node = parent;
            }

            return minFlow;
        }

        private static bool BFS(int source, int destination)
        {
            var queue = new Queue<int>();
            var visited = new bool[graph.GetLength(0)];

            queue.Enqueue(source);
            visited[source] = true;

            while (queue.Count > 0)
            {
                var node = queue.Dequeue();

                for (int child = 0; child <= destination; child++)
                {
                    if (!visited[child] && graph[node, child] > 0)
                    {
                        visited[child] = true;
                        queue.Enqueue(child);
                        parents[child] = node;
                    }
                }
            }

            return visited[destination];
        }

        private static int[,] ReadGraph(int n)
        {
            var result = new int[n, n];

            for (int i = 0; i < n; i++)
            {
                var row = Console.ReadLine().Split(", ").Select(int.Parse).ToArray();

                for (int j = 0; j < n; j++)
                {
                    result[i, j] = row[j];
                }
            }

            return result;
        }
    }
}
