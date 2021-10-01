namespace Tree
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Cryptography.X509Certificates;
    using System.Text;
    using System.Transactions;

    public class Tree<T> : IAbstractTree<T>
    {
        private readonly List<Tree<T>> _children;

        public Tree(T key, params Tree<T>[] children)
        {
            this.Key = key;
            this._children = new List<Tree<T>>();

            foreach (var child in children)
            {
                this._children.Add(child);
                child.Parent = this;
            }
        }

        public T Key { get; private set; }

        public Tree<T> Parent { get; private set; }


        public IReadOnlyCollection<Tree<T>> Children
            => this._children.AsReadOnly();

        public void AddChild(Tree<T> child)
        {
            this._children.Add(child);
        }

        public void AddParent(Tree<T> parent)
        {
            this.Parent = parent;
        }

        public string GetAsString()
        {
            StringBuilder sb = new StringBuilder();

            int indent = 0;
            this.Dfs(this, sb, indent);

            return sb.ToString().TrimEnd();
        }

        public Tree<T> GetDeepestLeftomostNode()
        {
            Tree<T> deepestNode = this;
            var queue = new Queue<Tree<T>>();

            queue.Enqueue(this);
            int biggestDepth = 0;

            while (queue.Count != 0)
            {
                var subtree = queue.Dequeue();

                foreach (var child in subtree.Children)
                {
                    int depth = this.FindDepth(child);

                    if (depth > biggestDepth)
                    {
                        biggestDepth = depth;
                        deepestNode = child;
                    }

                    queue.Enqueue(child);
                }
            }

            return deepestNode;
        }

        public List<T> GetLeafKeys()
        {
            Func<Tree<T>, bool> leafKeysPredicate = node => this.IsLeaf(node);

            return this.OrderBfs(leafKeysPredicate);
        }

        public List<T> GetMiddleKeys()
        {
            Func<Tree<T>, bool> middleKeysPredicate = node => this.IsMiddle(node);

            return this.OrderBfs(middleKeysPredicate);
        }

        public List<T> GetLongestPath()
        {
            var longestPath = new List<T>();
            Tree<T> deepestNode = this.GetDeepestLeftomostNode();

            this.FindPath(deepestNode, longestPath);

            return longestPath;
        }

        public List<List<T>> PathsWithGivenSum(int sum)
        {
            var paths = new List<List<T>>();

            int currentSum = Convert.ToInt32(this.Key);
            var currentPath = new List<T>();
            currentPath.Add(this.Key);
            this.FindPathsWithSumDfs(paths, currentPath, ref currentSum, sum, this);

            return paths;

        }

        public List<Tree<T>> SubTreesWithGivenSum(int sum)
        {
            var result = new List<Tree<T>>();
            var queue = new Queue<Tree<T>>();

            queue.Enqueue(this);

            while (queue.Count != 0)
            {
                var subtree = queue.Dequeue();

                if (this.IsSumEqual(subtree, sum))
                {
                    result.Add(subtree);
                }

                foreach (var child in subtree.Children)
                {
                    queue.Enqueue(child);
                }
            }

            return result;

        }

        private void Dfs(Tree<T> tree, StringBuilder sb, int indent)
        {
            sb.AppendLine($"{new string(' ', indent)}{tree.Key}");

            foreach (var child in tree._children)
            {
                Dfs(child, sb, indent + 2);
            }
        }

        private List<T> OrderBfs(Func<Tree<T>, bool> predicate)
        {
            var result = new List<T>();
            var queue = new Queue<Tree<T>>();

            queue.Enqueue(this);

            while (queue.Count != 0)
            {
                var subtree = queue.Dequeue();

                if (predicate.Invoke(subtree))
                {
                    result.Add(subtree.Key);
                }

                foreach (var child in subtree.Children)
                {
                    queue.Enqueue(child);
                }
            }

            return result;
        }

        private int FindDepth(Tree<T> node)
        {
            int depth = 0;
            Tree<T> current = node;

            while (current.Parent != null)
            {
                depth++;
                current = current.Parent;
            }

            return depth;
        }

        private void FindPath(Tree<T> node, List<T> path)
        {
            if (node == null)
            {
                return;
            }

            FindPath(node.Parent, path);
            path.Add(node.Key);
        }

        private void FindPathsWithSumDfs(List<List<T>> paths, List<T> currentPath, ref int currentSum, int sum, Tree<T> tree)
        {

            if (currentSum == sum)
            {
                paths.Add(new List<T>());
                foreach (var item in currentPath)
                {
                    paths[paths.Count - 1].Add(item);
                }
                return;
            }

            if (currentSum > sum)
            {
                return;
            }

            foreach (var child in tree.Children)
            {
                currentPath.Add(child.Key);
                currentSum += Convert.ToInt32(child.Key);
                this.FindPathsWithSumDfs(paths, currentPath, ref currentSum, sum, child);
                currentPath.RemoveAt(currentPath.Count - 1);
                currentSum -= Convert.ToInt32(child.Key);
            }
        }

        private bool IsSumEqual(Tree<T> tree, int sum)
        {
            int currentSum = 0;
            var queue = new Queue<Tree<T>>();

            queue.Enqueue(tree);

            while (queue.Count != 0)
            {
                var subtree = queue.Dequeue();
                currentSum += Convert.ToInt32(subtree.Key);

                if (currentSum > sum)
                {
                    return false;
                }

                foreach (var child in subtree.Children)
                {
                    queue.Enqueue(child);
                }
            }

            if (currentSum == sum)
            {
                return true;
            }


            return false;
        }

        private bool IsLeaf(Tree<T> tree)
        {
            return tree.Children.Count == 0;
        }

        private bool IsRoot(Tree<T> tree)
        {
            return tree.Parent == null;
        }

        private bool IsMiddle(Tree<T> tree)
        {
            return !(this.IsLeaf(tree) || this.IsRoot(tree));
        }
    }
}
