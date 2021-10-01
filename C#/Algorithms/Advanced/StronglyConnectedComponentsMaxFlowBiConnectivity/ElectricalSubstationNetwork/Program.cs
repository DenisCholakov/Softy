using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ElectricalSubstationNetwork
{
    class Program
    {
        private static List<int>[] graph;
        private static List<int>[] reversedGraph;
        private static Stack<int> sorted;

        static void Main(string[] args)
        {
            int nodesCount = int.Parse(Console.ReadLine());
            int linesCount = int.Parse(Console.ReadLine());

            (graph, reversedGraph) = ReadGraph(nodesCount, linesCount);

            sorted = TopologicalSorting();

            var visited = new bool[nodesCount];

            while (sorted.Count > 0)
            {
                var node = sorted.Pop();

                if (visited[node])
                {
                    continue;
                }

                var component = new Stack<int>();

                DFS(reversedGraph, node, visited, component);

                Console.WriteLine(String.Join(", ", component));
            }
        }

        private static Stack<int> TopologicalSorting()
        {
            var stack = new Stack<int>();
            var visited = new bool[graph.Length];

            for (int node = 0; node < graph.Length; node++)
            {
                if (!visited[node])
                {
                    DFS(graph, node, visited, stack);
                }
            }

            return stack;
        }

        private static void DFS(List<int>[] targetGraph, int node, bool[] visited, Stack<int> stack)
        {
            if (visited[node])
            {
                return;
            }

            visited[node] = true;

            foreach (var child in targetGraph[node])
            {
                DFS(targetGraph, child, visited, stack);
            }

            stack.Push(node);
        }

        private static (List<int>[] original, List<int>[] reversed) ReadGraph(int nodesCount, int linesCount)
        {
            var original = new List<int>[nodesCount];
            var reversed = new List<int>[nodesCount];

            for (int i = 0; i < nodesCount; i++)
            {
                original[i] = new List<int>();
                reversed[i] = new List<int>();
            }

            for (int i = 0; i < linesCount; i++)
            {
                var input = Console.ReadLine().Split(", ").Select(int.Parse).ToArray();

                var node = input[0];
               
                for (int j = 1; j < input.Length; j++)
                {
                    var child = input[j];
                    original[node].Add(child);
                    reversed[child].Add(node);
                }
            }

            return (original, reversed);
        }
    }
}
