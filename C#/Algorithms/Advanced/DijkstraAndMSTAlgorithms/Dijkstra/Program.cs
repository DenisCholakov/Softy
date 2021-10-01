using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Wintellect.PowerCollections;

namespace Dijkstra
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
    class Program
    {
        private static Dictionary<int, List<Edge>> graph;
        static void Main(string[] args)
        {
            int e = int.Parse(Console.ReadLine());
            graph = ReadGraph(e);

            var start = int.Parse(Console.ReadLine());
            var end = int.Parse(Console.ReadLine());

            int[] distances = new int[graph.Keys.Max() + 1];
            // Array.Fill(distances, int.MaxValue);

            for (int i = 0; i < distances.Length; i++)
            {
                distances[i] = int.MaxValue;
            }

            distances[start] = 0;

            var prev = new int[graph.Keys.Max() + 1];
            prev[start] = -1;

            var queue = new OrderedBag<int>(
                    Comparer<int>.Create((f, s) => distances[f] - distances[s]));
            queue.Add(start);

            while (queue.Count > 0)
            {
                var minNode = queue.RemoveFirst();
                var children = graph[minNode];

                if (minNode == end)
                {
                    break;
                }

                foreach (var edge in children)
                {
                    var childNode = edge.First != minNode ? edge.First : edge.Second;
                    var newDistance = edge.Weight + distances[minNode];

                    if (distances[childNode] == int.MaxValue)
                    {
                        queue.Add(childNode);
                    }

                    if (newDistance < distances[childNode])
                    {
                        distances[childNode] = newDistance;
                        prev[childNode] = minNode;

                        queue = new OrderedBag<int>(queue,
                                Comparer<int>.Create((f, s) => distances[f] - distances[s]));
                    }       
                }
            }

            Console.WriteLine(distances[end]);

            var path = new Stack<int>();

            var node = end;
            while (node != -1)
            {
                path.Push(node);
                node = prev[node];
            }

            Console.WriteLine(String.Join(" ", path));
        }

        private static Dictionary<int, List<Edge>> ReadGraph(int e)
        {
            var result = new Dictionary<int, List<Edge>>();

            for (int i = 0; i < e; i++)
            {
                var edgeInfo = Console.ReadLine().Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();

                int firstVertex = edgeInfo[0];
                int secondVertex = edgeInfo[1];
                int weight = edgeInfo[2];

                if (!result.ContainsKey(firstVertex))
                {
                    result.Add(firstVertex, new List<Edge>());
                }

                if (!result.ContainsKey(secondVertex))
                {
                    result.Add(secondVertex, new List<Edge>());
                }

                var edge = new Edge(firstVertex, secondVertex, weight);

                result[firstVertex].Add(edge);
                result[secondVertex].Add(edge);
            }

            return result;
        }
    }
}
