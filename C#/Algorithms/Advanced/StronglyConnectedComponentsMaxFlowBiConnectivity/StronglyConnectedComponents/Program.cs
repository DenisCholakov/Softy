using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace StronglyConnectedComponents
{
    class Program
    {
        private static Stack<int> sorted;
        private static List<int>[] graph;
        private static List<int>[] reversed;
        static void Main(string[] args)
        {
            int nodesCount = int.Parse(Console.ReadLine());
            int linesCount = int.Parse(Console.ReadLine());

            (graph, reversed) = ReadGraph(nodesCount, linesCount);

            sorted = TopologicalSorting(graph);

            Console.WriteLine("Strongly Connected Components:");

            var visited = new bool[nodesCount];
            while (sorted.Count > 0)
            {
                var node = sorted.Pop();

                if (visited[node])
                {
                    continue;
                }

                var component = new Stack<int>();

                DFS(node, component, visited, reversed);

                Console.WriteLine($"{{{String.Join(", ", component)}}}");
            }
        }

        private static Stack<int> TopologicalSorting(List<int>[] graph)
        {
            var result = new Stack<int>();
            bool[] visited = new bool[graph.Length];

            for (int i = 0; i < graph.Length; i++)
            {
                if (!visited[i])
                {
                    DFS(i, result, visited, graph);
                }
            }

            return result;
        }

        private static void DFS(int node, Stack<int> stack, bool[] visited, List<int>[] graph)
        {
            if (visited[node])
            {
                return;
            }

            visited[node] = true;

            foreach (var child in graph[node])
            {
                DFS(child, stack, visited, graph);
            }

            stack.Push(node);
        }

        private static (List<int>[] original, List<int>[] rev) ReadGraph(int n, int l)
        {
            var original = new List<int>[n];
            var rev = new List<int>[n];

            for (int i = 0; i < n; i++)
            {
                original[i] = new List<int>();
                rev[i] = new List<int>();
            }

            for (int i = 0; i < l; i++)
            {
                var input = Console.ReadLine().Split(", ").Select(int.Parse).ToArray();

                var node = input[0];

                for (int j = 1; j < input.Length; j++)
                {
                    var child = input[j];
                    original[node].Add(child);
                    rev[child].Add(node);
                }
            }

            return (original, rev);
        }
    }
}
