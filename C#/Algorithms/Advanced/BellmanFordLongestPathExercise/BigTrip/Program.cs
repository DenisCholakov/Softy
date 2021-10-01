using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BigTrip
{
    public class Edge
    {
        public int From { get; set; }

        public int To { get; set; }

        public int Weight { get; set; }
    }
    class Program
    {
        private static List<Edge>[] graph;

        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            int e = int.Parse(Console.ReadLine());

            graph = ReadGraph(n, e);

            var sortedNodes = TopologicalSort();

            int source = int.Parse(Console.ReadLine());
            int destination = int.Parse(Console.ReadLine());

            var distances = new double[n + 1];
            var prev = new int[n + 1];

            for (int i = 0; i <= n; i++)
            {
                distances[i] = double.NegativeInfinity;
                prev[i] = -1;
            }

            distances[source] = 0;

            while (sortedNodes.Count > 0)
            {
                var node = sortedNodes.Pop();

                if (double.IsNegativeInfinity(distances[node]))
                {
                    continue;
                }

                foreach (var edge in graph[node])
                {
                    var newDistance = distances[node] + edge.Weight;

                    if (newDistance > distances[edge.To])
                    {
                        distances[edge.To] = newDistance;
                        prev[edge.To] = node;
                    }
                }
            }

            Console.WriteLine(distances[destination]);
            Console.WriteLine(String.Join(" ", GetPath(destination, prev)));
        }

        private static Stack<int> GetPath(int node, int[] prev)
        {
            var path = new Stack<int>();

            while (node != -1)
            {
                path.Push(node);
                node = prev[node];
            }

            return path;
        }

        private static Stack<int> TopologicalSort()
        {
            var result = new Stack<int>();
            bool[] visited = new bool[graph.Length];

            for (int node = 1; node < graph.Length; node++)
            {
                if (!visited[node])
                {
                    DFS(node, visited, result);
                }
            }

            return result;
        }

        private static void DFS(int node, bool[] visited, Stack<int> result)
        {
            if (visited[node])
            {
                return;
            }

            visited[node] = true;

            foreach (var edge in graph[node])
            {
                DFS(edge.To, visited, result);
            }

            result.Push(node);
        }

        private static List<Edge>[] ReadGraph(int n, int e)
        {
            var result = new List<Edge>[n + 1];

            for (int i = 1; i < result.Length; i++)
            {
                result[i] = new List<Edge>();
            }

            for (int i = 0; i < e; i++)
            {
                var edgeInfo = Console.ReadLine().Split().Select(int.Parse).ToArray();

                var from = edgeInfo[0];
                var to = edgeInfo[1];
                var weight = edgeInfo[2];

                result[from].Add(new Edge
                {
                    From = from,
                    To = to,
                    Weight = weight
                });
            }

            return result;
        }
    }
}
