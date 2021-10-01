namespace Tree
{
    using System;
    using System.Collections.Generic;
    using System.Net.WebSockets;

    public class Tree<T> : IAbstractTree<T>
    {
        private readonly List<Tree<T>> _children;

        public Tree(T value)
        {
            this.Value = value;
            this.Parent = null;
            this._children = new List<Tree<T>>();
        }

        public Tree(T value, params Tree<T>[] children)
            : this(value)
        {
            foreach (var child in children)
            {
                child.Parent = this;
                this._children.Add(child);
            }
        }

        public T Value { get; private set; }
        public Tree<T> Parent { get; private set; }
        public IReadOnlyCollection<Tree<T>> Children => this._children.AsReadOnly();

        public bool RootDeleted { get; private set; }

        public ICollection<T> OrderBfs()
        {
            var result = new List<T>();
            var queue = new Queue<Tree<T>>();

            if (this.RootDeleted == true)
            {
                return result;
            }

            queue.Enqueue(this);

            while (queue.Count != 0)
            {
                Tree<T> subtree = queue.Dequeue();

                foreach (var child in subtree.Children)
                {
                    queue.Enqueue(child);
                }

                result.Add(subtree.Value);
            }

            return result;
        }

        public ICollection<T> OrderDfs()
        {
            List<T> result = new List<T>();

            if (this.RootDeleted == true)
            {
                return result;
            }

            this.Dfs(this, result);

            return result;

        }

        public void AddChild(T parentKey, Tree<T> child)
        {
            Tree<T> parentNode = FindBfs(parentKey);
            CheckEmptyNode(parentNode);

            child.Parent = parentNode;
            parentNode._children.Add(child);
        }

        public void RemoveNode(T nodeKey)
        {
            Tree<T> NodeToRemove = FindBfs(nodeKey);
            this.CheckEmptyNode(NodeToRemove);

            var parentNode = NodeToRemove.Parent;

            if (parentNode == null)
            {
                this.RootDeleted = true;
            }
            else
            {
                NodeToRemove.Parent = null;
                parentNode._children.Remove(NodeToRemove);
            }
        }

        public void Swap(T firstKey, T secondKey)
        {
            var firstNode = this.FindBfs(firstKey);
            var secondNode = this.FindBfs(secondKey);

            this.CheckEmptyNode(firstNode);
            this.CheckEmptyNode(secondNode);

            var firstParent = firstNode.Parent;
            var secondParent = secondNode.Parent;

            if (firstParent == null)
            {
                SwapRoot(secondNode);
                return;
            }

            if (secondParent == null)
            {
                SwapRoot(firstNode);
                return;
            }

            var firstIndex = firstParent._children.IndexOf(firstNode);
            var secondIndex = secondParent._children.IndexOf(secondNode);

            firstNode.Parent = secondParent;
            secondNode.Parent = firstParent;

            firstParent._children[firstIndex] = secondNode;
            secondParent._children[secondIndex] = firstNode;
        }

        private void SwapRoot(Tree<T> node)
        {
            this.Value = node.Value;
            this._children.Clear();

            foreach (var child in node._children)
            {
                this._children.Add(child);
            }
        }

        private void Dfs(Tree<T> subtree, List<T> result)
        {
            foreach (var child in subtree.Children)
            {
                Dfs(child, result);
            }

            result.Add(subtree.Value);
        }

        private ICollection<T> DfsStack()
        {
            var stack = new Stack<Tree<T>>();
            var nodes = new Stack<T>();

            stack.Push(this);

            while (stack.Count != 0)
            {
                Tree<T> subtree = stack.Pop();
                nodes.Push(subtree.Value);

                foreach (var child in subtree.Children)
                {
                    stack.Push(child);
                }
            }

            return new List<T>(nodes);
        }

        private void CheckEmptyNode(Tree<T> parentNode)
        {
            if (parentNode is null)
            {
                throw new ArgumentNullException("The searched node was not found!");
            }
        }

        private Tree<T> FindBfs(T parentKey)
        {
            var queue = new Queue<Tree<T>>();

            queue.Enqueue(this);

            while (queue.Count != 0)
            {
                var subtree = queue.Dequeue();

                if (parentKey.Equals(subtree.Value))
                {
                    return subtree;
                }

                foreach (var child in subtree.Children)
                {
                    queue.Enqueue(child);
                }
            }

            return null;
        }
    }
}
