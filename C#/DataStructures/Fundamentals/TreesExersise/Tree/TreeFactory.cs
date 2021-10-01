namespace Tree
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class TreeFactory
    {
        private Dictionary<int, Tree<int>> nodesBykeys;

        public TreeFactory()
        {
            this.nodesBykeys = new Dictionary<int, Tree<int>>();
        }

        public Tree<int> CreateTreeFromStrings(string[] input)
        {
            foreach (var line in input)
            {
                int[] edge = line.Split().Select(int.Parse).ToArray();
                
                int parentNode = edge[0];
                int childNode = edge[1];

                this.AddEdge(parentNode, childNode);
            }

            return this.GetRoot();

        }

        public Tree<int> CreateNodeByKey(int key)
        {
            if (!this.nodesBykeys.ContainsKey(key))
            {
                this.nodesBykeys.Add(key, new Tree<int>(key));
            }

            return this.nodesBykeys[key];
        }

        public void AddEdge(int parent, int child)
        {
            var parentNode = this.CreateNodeByKey(parent);
            var childNode = this.CreateNodeByKey(child);

            childNode.AddParent(parentNode);
            parentNode.AddChild(childNode);
        }

        private Tree<int> GetRoot()
        {
            foreach (var pair in this.nodesBykeys)
            {
                if (pair.Value.Parent == null)
                {
                    return pair.Value;
                }
            }

            return null;
        }
    }
}
