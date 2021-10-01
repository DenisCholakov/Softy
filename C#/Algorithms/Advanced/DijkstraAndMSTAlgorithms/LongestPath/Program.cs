using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LongestPath
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
            var sorted = TopologicalSort();

            int source = int.Parse(Console.ReadLine());
            int destination = int.Parse(Console.ReadLine());

            var distances = new double[n + 1];
            Array.Fill(distances, double.NegativeInfinity);
            distances[source] = 0;

            var prev = new int[n + 1];
            Array.Fill(prev, -1);

            while (sorted.Count > 0)
            {
                var node = sorted.Pop();

                foreach (var edge in graph[node])
                {
                    var newDistance = distances[node] + edge.Weight;
                    if (newDistance > distances[edge.To])
                    {
                        distances[edge.To] = newDistance;
                        prev[edge.To] = edge.From;
                    }
                }
            }

            Console.WriteLine(distances[destination]);
        }

        private static Stack<int> TopologicalSort()
        {
            var sorted = new Stack<int>();
            var visited = new bool[graph.Length];

            for (int i = 1; i < graph.Length; i++)
            {
                if (!visited[i])
                {
                    DFS(i, visited, sorted);
                }
            }

            return sorted;
        }

        private static void DFS(int i, bool[] visited, Stack<int> sorted)
        {
            if (visited[i])
            {
                return;
            }

            visited[i] = true;

            foreach (var edge in graph[i])
            {
                DFS(edge.To, visited, sorted);
            }

            sorted.Push(i);
        }

        private static List<Edge>[] ReadGraph(int n, int e)
        {
            var result = new List<Edge>[n + 1];

            for (int i = 1; i <= n; i++)
            {
                result[i] = new List<Edge>();
            }

            for (int i = 0; i < e; i++)
            {
                var edgeInfo = Console.ReadLine().Split().Select(int.Parse).ToArray();

                int from = edgeInfo[0];
                int to = edgeInfo[1];
                int weight = edgeInfo[2];

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
