using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Wintellect.PowerCollections;

namespace PrimsAlgorithm
{
    public class Edge
    {
        public int First { get; set; }

        public int Second { get; set; }

        public int Weight { get; set; }
    }
    class Program
    {
        private static Dictionary<int, List<Edge>> graph;
        private static HashSet<int> forest;

        static void Main(string[] args)
        {
            int e = int.Parse(Console.ReadLine());
            forest = new HashSet<int>();

            graph = ReadGraph(e);

            foreach (var node in graph.Keys)
            {
                if (!forest.Contains(node))
                {
                    Prim(node);
                }
            }
        }

        private static void Prim(int node)
        {
            forest.Add(node);

            var queue = new OrderedBag<Edge>(
                            graph[node],
                            Comparer<Edge>.Create((f, s) => f.Weight - s.Weight));

            while (queue.Count > 0)
            {
                var edge = queue.RemoveFirst();

                int nonTreeNode = GetNonTreeNode(edge.First, edge.Second);

                if (nonTreeNode == -1)
                {
                    continue;
                }

                Console.WriteLine($"{edge.First} - {edge.Second}");

                forest.Add(nonTreeNode);
                queue.AddMany(graph[nonTreeNode]);
            }
        }

        private static int GetNonTreeNode(int first, int second)
        {
            int nonTreeNode = -1;

            if (!forest.Contains(second))
            {
                nonTreeNode = second;
            }

            if (!forest.Contains(first))
            {
                nonTreeNode = first;
            }

            return nonTreeNode;
        }

        private static Dictionary<int, List<Edge>> ReadGraph(int e)
        {
            var result = new Dictionary<int, List<Edge>>();

            for (int i = 0; i < e; i++)
            {
                var graphInfo = Console.ReadLine().Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries)
                                    .Select(int.Parse).ToArray();
                int first = graphInfo[0];
                int second = graphInfo[1];
                int weight = graphInfo[2];

                if (!result.ContainsKey(first))
                {
                    result.Add(first, new List<Edge>());
                }
                
                if (!result.ContainsKey(second))
                {
                    result.Add(second, new List<Edge>());
                }

                Edge edge = new Edge
                {
                    First = first,
                    Second = second,
                    Weight = weight
                };

                result[first].Add(edge);
                result[second].Add(edge);
            }

            return result;
        }
    }
}
