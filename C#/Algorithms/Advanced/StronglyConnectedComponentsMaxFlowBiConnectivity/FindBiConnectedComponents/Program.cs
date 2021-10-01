using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FindBiConnectedComponents
{
    class Program
    {
        private static List<int>[] graph;
        private static int[] depths;
        private static int[] lowestPoints;
        private static int[] parents;
        private static bool[] visited;
        private static Stack<int> stack;
        private static List<HashSet<int>> components;

        static void Main(string[] args)
        {
            var nodesCount = int.Parse(Console.ReadLine());
            var edgesCount = int.Parse(Console.ReadLine());

            depths = new int[nodesCount];
            lowestPoints = new int[nodesCount];
            visited = new bool[nodesCount];

            parents = new int[nodesCount];
            Array.Fill(parents, -1);

            stack = new Stack<int>();
            components = new List<HashSet<int>>();

            graph = ReadGraph(nodesCount, edgesCount);

            for (int node = 0; node < graph.Length; node++)
            {
                if (!visited[node])
                {
                    FindArticulationPoints(node, 1);

                    var component = new HashSet<int>();

                    while (stack.Count > 1)
                    {
                        var stackChild = stack.Pop();
                        var stackNode = stack.Pop();

                        component.Add(stackNode);
                        component.Add(stackChild);
                    }

                    components.Add(component);
                }
            }

            Console.WriteLine($"Number of bi-connected components: {components.Count}");

            //foreach (var component in components)
            //{
            //    Console.WriteLine(String.Join(' ', component));
            //}

        }

        private static void FindArticulationPoints(int node, int depth)
        {
            visited[node] = true;
            depths[node] = depth;
            lowestPoints[node] = depth;

            var childCount = 0;

            foreach (var child in graph[node])
            {
                if (!visited[child])
                {
                    stack.Push(node);
                    stack.Push(child);

                    parents[child] = node;
                    childCount++;

                    FindArticulationPoints(child, depth + 1);

                    if ((parents[node] == -1 && childCount > 1) || 
                        (parents[node] != -1 && lowestPoints[child] >= depth))
                    {
                        var component = new HashSet<int>();
                        while (true)
                        {
                            var stackChild = stack.Pop();
                            var stackNode = stack.Pop();

                            component.Add(stackNode);
                            component.Add(stackChild);

                            if (stackNode == node && stackChild == child)
                            {
                                break;
                            }
                        }

                        components.Add(component);
                    }

                    lowestPoints[node] = Math.Min(lowestPoints[node], lowestPoints[child]);
                }
                else if (parents[node] != child && depths[child] < lowestPoints[node])
                {
                    lowestPoints[node] = depths[child];

                    stack.Push(node);
                    stack.Push(child);
                }
            }
        }

        private static List<int>[] ReadGraph(int nodesCount, int edgesCount)
        {
            var result = new List<int>[nodesCount];

            for (int i = 0; i < nodesCount; i++)
            {
                result[i] = new List<int>();
            }

            for (int i = 0; i < edgesCount; i++)
            {
                var edge = Console.ReadLine().Split().Select(int.Parse).ToArray();

                var first = edge[0];
                var second = edge[1];

                result[first].Add(second);
                result[second].Add(first);
            }

            return result;
        }
    }
}
