using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CheapTownTour
{
    public class Edge
    {
        public int First { get; set; }

        public int Second { get; set; }

        public int Weight { get; set; }
    }
    class Program
    {
        private static List<Edge> edges;

        static void Main(string[] args)
        {
            int nodeCount = int.Parse(Console.ReadLine());
            int edgeCount = int.Parse(Console.ReadLine());

            edges = ReadGraph(edgeCount);
            var sortedEdges = edges.OrderBy(e => e.Weight).ToList();

            var root = new int[nodeCount];

            for (int i = 0; i < root.Length; i++)
            {
                root[i] = i;            
            }

            var totalCost = 0;

            foreach (var edge in sortedEdges)
            {
                var firstRoot = GetRoot(edge.First, root);
                var secondRoot = GetRoot(edge.Second, root);

                if (firstRoot != secondRoot)
                {
                    root[firstRoot] = secondRoot;
                    totalCost += edge.Weight;
                }
            }

            Console.WriteLine($"Total cost: {totalCost}");
        }

        private static int GetRoot(int node, int[] root)
        {
            while(node != root[node])
            {
                node = root[node];
            }

            return node;
        }

        private static List<Edge> ReadGraph(int e)
        {
            var result = new List<Edge>();

            for (int i = 0; i < e; i++)
            {
                var edgeInfo = Console.ReadLine().Split(" - ").Select(int.Parse).ToArray();

                var fisrt = edgeInfo[0];
                var second = edgeInfo[1];
                var weight = edgeInfo[2];

                result.Add(new Edge
                {
                    First = fisrt,
                    Second = second,
                    Weight = weight
                });
            }

            return result;
        }
    }
}
