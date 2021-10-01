using System;
using System.Collections.Generic;
using System.IO;
using Wintellect.PowerCollections;

namespace CableNetwork
{
    public class Edge
    {
        public int First { get; set; }

        public int Second { get; set; }

        public int Weight { get; set; }
    }
    class Program
    {
        private static List<Edge>[] graph;
        private static HashSet<int> spanningTree;
        static void Main(string[] args)
        {
            var budget = int.Parse(Console.ReadLine());
            var nodeCount = int.Parse(Console.ReadLine());
            var edgeCount = int.Parse(Console.ReadLine());

            spanningTree = new HashSet<int>();
            graph = ReadGraph(nodeCount, edgeCount);

            int usedBufget = Prim(budget);

            Console.WriteLine($"Budget used: {usedBufget}");
        }

        private static int Prim(int budget)
        {
            var usedBudget = 0;

            var queue = new OrderedBag<Edge>(
                Comparer<Edge>.Create((f, s) => f.Weight - s.Weight));

            foreach (var node in spanningTree)
            {
                queue.AddMany(graph[node]);
            }

            while (queue.Count > 0)
            {
                var edge = queue.RemoveFirst();

                var nonTreeNode = GetNonTreeNode(edge.First, edge.Second);

                if (nonTreeNode == -1)
                {
                    continue;
                }

                if (budget < edge.Weight)
                {
                    break;
                }

                usedBudget += edge.Weight;
                budget -= edge.Weight;

                queue.AddMany(graph[nonTreeNode]);
                spanningTree.Add(nonTreeNode);
            }

            return usedBudget;
        }

        private static int GetNonTreeNode(int first, int second)
        {
            if (spanningTree.Contains(first) &&
                !spanningTree.Contains(second))
            {
                return second;
            }

            if (spanningTree.Contains(second) &&
                !spanningTree.Contains(first))
            {
                return first;
            }

            return -1;
        }

        private static List<Edge>[] ReadGraph(int n, int e)
        {
            var result = new List<Edge>[n];

            for (int i = 0; i < result.Length; i++)
            {
                result[i] = new List<Edge>();
            }

            for (int i = 0; i < e; i++)
            {
                var edgeInfo = Console.ReadLine().Split();

                var first = int.Parse(edgeInfo[0]);
                var second = int.Parse(edgeInfo[1]);
                var weight = int.Parse(edgeInfo[2]);

                if (edgeInfo.Length == 4)
                {
                    spanningTree.Add(first);
                    spanningTree.Add(second);
                }

                var edge = new Edge
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
