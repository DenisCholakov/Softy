using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TopologicalSorting
{
    class Program
    {
        private static Dictionary<string, List<string>> graph;
        private static Dictionary<string, int> predecessorsCount;

        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            graph = new Dictionary<string, List<string>>();
            predecessorsCount = new Dictionary<string, int>();
            ReadGraph(n);
            GetPredecessors();
            var sorted = TopologicalSort();

            if (sorted.Count == 0 || predecessorsCount.Count != 0)
            {
                Console.WriteLine("Invalid topological sorting");
            }
            else
            {
                Console.WriteLine($"Topological sorting: {String.Join(", ", sorted)}");
            }
        }

        private static void WithDfs()
        {
            // TODO:
        }

        private static List<string> TopologicalSort()
        {
            var result = new List<string>();

            while (predecessorsCount.Count > 0)
            {
                var current = predecessorsCount.FirstOrDefault(p => p.Value == 0);

                if (current.Key == null)
                {
                    break;
                }

                foreach (var child in graph[current.Key])
                {
                    predecessorsCount[child]--;
                }

                predecessorsCount.Remove(current.Key);

                result.Add(current.Key);
            }

            return result;
        }

        private static void GetPredecessors()
        {
            foreach (var kvp in graph)
            {
                var node = kvp.Key;
                var predecessors = kvp.Value;

                if (!predecessorsCount.ContainsKey(node))
                {
                    predecessorsCount.Add(node, 0);
                }

                foreach (var child in predecessors)
                {
                    if (predecessorsCount.ContainsKey(child))
                    {
                        predecessorsCount[child]++;
                    }
                    else
                    {
                        predecessorsCount.Add(child, 1);
                    }
                }
            }
        }

        private static void ReadGraph(int n)
        {
            for (int i = 0; i < n; i++)
            {
                string[] input = Console.ReadLine().Split(" ->");
                string node = input[0];

                if (String.IsNullOrWhiteSpace(input[1]))
                {
                    graph.Add(node, new List<string>());
                }
                else
                {
                    var children = input[1].Trim().Split(", ").ToList();
                    graph.Add(node, children);
                }
            }
        }
    }
}
