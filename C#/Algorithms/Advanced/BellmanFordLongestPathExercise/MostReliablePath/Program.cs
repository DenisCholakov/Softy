using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Wintellect.PowerCollections;

namespace MostReliablePath
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

        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            int e = int.Parse(Console.ReadLine());

            graph = ReadGraph(n, e);

            var source = int.Parse(Console.ReadLine());
            var destination = int.Parse(Console.ReadLine());

            var distances = new double[n];
            var prev = new int[n];

            for (int i = 0; i < n; i++)
            {
                distances[i] = double.NegativeInfinity;
                prev[i] = -1;
            }

            distances[source] = 100;

            var queue = new OrderedBag<int>(Comparer<int>.Create((f, s) => distances[s].CompareTo(distances[f])));
            queue.Add(source);

            while (queue.Count > 0)
            {
                var node = queue.RemoveFirst();

                if (node == destination)
                {
                    break;
                }

                var children = graph[node];
                foreach (var edge in children)
                {
                    var childNode = edge.First == node ? edge.Second : edge.First;

                    if (double.IsNegativeInfinity(distances[childNode]))
                    {
                        queue.Add(childNode);
                    }

                    var newDistance = (distances[node] * edge.Weight) / 100;
                    if (newDistance > distances[childNode])
                    {
                        distances[childNode] = newDistance;
                        prev[childNode] = node;

                        queue = new OrderedBag<int>(queue, Comparer<int>.Create((f, s) => distances[s].CompareTo(distances[f])));
                    }

                }
            }

            Console.WriteLine($"Most reliable path reliability: {distances[destination]:f2}%");
            Console.WriteLine(String.Join(" -> ", GetPath(prev, destination)));
        }

        private static Stack<int> GetPath(int[] prev, int node)
        {
            var path = new Stack<int>();

            while (node != -1)
            {
                path.Push(node);
                node = prev[node];
            }

            return path;
        }

        private static List<Edge>[] ReadGraph(int n, int e)
        {
            var result = new List<Edge>[n];

            for (int i = 0; i < n; i++)
            {
                result[i] = new List<Edge>();
            }

            for (int i = 0; i < e; i++)
            {
                var edgeInfo = Console.ReadLine().Split().Select(int.Parse).ToArray();

                var first = edgeInfo[0];
                var second = edgeInfo[1];
                var weight = edgeInfo[2];

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
