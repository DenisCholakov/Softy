using System;
using System.Collections.Generic;
using System.Linq;

namespace Exam
{
    public class Edge
    {
        public int First { get; set; }
        public int Second { get; set; }
        public int Distance { get; set; }
    }
    class Program
    {
        private static List<Edge> graph;

        static void Main(string[] args)
        {
            int shopsCount = int.Parse(Console.ReadLine());
            int connectionsCount = int.Parse(Console.ReadLine());

            graph = ReadGraph(shopsCount, connectionsCount);

            var minDistance = 0;

            var forest = graph.Select(e => e.First).Union(graph.Select(e => e.Second)).ToHashSet();

            var parents = new int[shopsCount];

            for (int i = 0; i < shopsCount; i++)
            {
                parents[i] = i;
            }

            foreach (var edge in graph)
            {
                var fisrtRoot = GetRoot(parents, edge.First);
                var secondRoot = GetRoot(parents, edge.Second);

                if (fisrtRoot == secondRoot)
                {
                    continue;
                }

                minDistance += edge.Distance;
                parents[fisrtRoot] = secondRoot;
            }


            Console.WriteLine(minDistance);
        }

        private static List<Edge> ReadGraph(int shopsCount, int connectionsCount)
        {
            var result = new List<Edge>();

            for (int i = 0; i < connectionsCount; i++)
            {
                var edgeData = Console.ReadLine().Split().Select(int.Parse).ToArray();

                var first = edgeData[0];
                var second = edgeData[1];
                var distance = edgeData[2];

                var edge = new Edge
                {
                    First = first,
                    Second = second,
                    Distance = distance
                };

                result.Add(edge);
            }

            return result.OrderBy(e => e.Distance).ToList();
        }


        private static int GetRoot(int[] parents, int node)
        {
            while (node != parents[node])
            {
                node = parents[node];
            }

            return node;
        }
    }
}
