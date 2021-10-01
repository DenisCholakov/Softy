using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MST
{
    public class Edge
    {
        public Edge(int first, int second, int weight)
        {
            this.First = first;
            this.Second = second;
            this.Weight = weight;
        }

        public int First { get; set; }
        public int Second { get; set; }
        public int Weight { get; set; }
    }
    public class StartUp
    {
        private static List<Edge> edges;

        static void Main(string[] args)
        {
            int e = int.Parse(Console.ReadLine());
            edges = ReadEdges(e);
            var sortedEdges = edges.OrderBy(e => e.Weight).ToList();

            var forest = edges.Select(e => e.First).Union(edges.Select(e => e.Second)).ToHashSet();

            var parents = new int[forest.Max() + 1];

            foreach (var vertex in forest)
            {
                parents[vertex] = vertex;
            }

            foreach (var edge in sortedEdges)
            {
                var firstNodeRoot = GetRoot(parents, edge.First);
                var secondNodeRoot = GetRoot(parents, edge.Second);

                if (firstNodeRoot == secondNodeRoot)
                {
                    continue;
                }

                Console.WriteLine($"{edge.First} - {edge.Second}");
                parents[firstNodeRoot] = secondNodeRoot;

                //if (edge.First != firstNodeRoot)
                //{
                //    parents[firstNodeRoot] = edge.Second;
                //}
                //else
                //{
                //    parents[secondNodeRoot] = edge.First;
                //}
            }
        }

        private static int GetRoot(int[] parents, int node)
        {
            while (node != parents[node])
            {
                node = parents[node];
            }

            return node;
        }

        private static List<Edge> ReadEdges(int e)
        {
            var result = new List<Edge>();

            for (int i = 0; i < e; i++)
            {
                var edgeData = Console.ReadLine().Split(", ").Select(int.Parse).ToArray();

                var first = edgeData[0];
                var second = edgeData[1];
                var weight = edgeData[2];

                result.Add(new Edge(first, second, weight));
            }

            return result;
        }
    }
}
