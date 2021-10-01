using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Undefined
{
    public class Edge
    {
        public int From { get; set; }

        public int To { get; set; }

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

            var distances = new double[nodeCount + 1];
            var prev = new int[nodeCount + 1];

            int source = int.Parse(Console.ReadLine());
            int destination = int.Parse(Console.ReadLine());

            for (int i = 1; i <= nodeCount; i++)
            {
                distances[i] = double.PositiveInfinity;
                prev[i] = -1;
            }

            distances[source] = 0;

            for (int i = 1; i < nodeCount; i++)
            {
                bool updated = false;

                foreach (var edge in edges)
                {
                    if (double.IsPositiveInfinity(edge.From))
                    {
                        continue;
                    }

                    var newDistance = distances[edge.From] + edge.Weight;

                    if (newDistance < distances[edge.To])
                    {
                        distances[edge.To] = newDistance;
                        prev[edge.To] = edge.From;
                        updated = true;
                    }
                }

                if (!updated)
                {
                    break;
                }
            }

            foreach (var edge in edges)
            {
                if (double.IsPositiveInfinity(edge.From))
                {
                    continue;
                }

                var newDistance = distances[edge.From] + edge.Weight;

                if (newDistance < distances[edge.To])
                {
                    Console.WriteLine("Undefined");
                    return;
                }
            }

            Console.WriteLine(String.Join(" ", GetPath(destination, prev)));
            Console.WriteLine(distances[destination]);
        }

        private static Stack<int> GetPath(int node, int[] prev)
        {
            var path = new Stack<int>();

            while (node != -1)
            {
                path.Push(node);
                node = prev[node];
            }

            return path;
        }

        private static List<Edge> ReadGraph(int e)
        {
            var result = new List<Edge>();

            for (int i = 0; i < e; i++)
            {
                var edgeInfo = Console.ReadLine().Split().Select(int.Parse).ToArray();

                var from = edgeInfo[0];
                var to = edgeInfo[1];
                var weight = edgeInfo[2];

                result.Add(new Edge
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
