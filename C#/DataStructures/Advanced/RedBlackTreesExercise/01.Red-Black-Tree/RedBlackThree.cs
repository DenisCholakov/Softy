namespace _01.Red_Black_Tree
{
    using System;
    using System.Collections.Generic;

    public class RedBlackTree<T> 
        : IBinarySearchTree<T> where T : IComparable
    {
        private const bool red = true;
        private const bool black = false;

        private Node root;

        public RedBlackTree()
        {
        }

        public int Count => this.GetCount(this.root);

        public void Insert(T element)
        {
            this.root = this.Insert(element, this.root);
            this.root.Color = black;
        }

        public T Select(int rank)
        {
            var node = this.Select(rank, this.root);

            if (node == null)
            {
                throw new InvalidOperationException($"There is no element with rank {rank}");
            }

            return node.Value;
        }

        public int Rank(T element)
        {
            return this.Rank(element, this.root);
        }

        public bool Contains(T element)
        {
            var node = FindElement(element);
            return node != null;
        }

        public IBinarySearchTree<T> Search(T element)
        {
            // dumb-ass idea
            // da se popravi ref

            var node = this.FindElement(element);
            var tree = new RedBlackTree<T>();
            tree.root = node;
            return tree;

        }

        public void DeleteMin()
        {
            if (this.root == null)
            {
                throw new InvalidOperationException("The tree is empty");
            }

            this.root = this.DeleteMin(this.root);
        }

        public void DeleteMax()
        {
            if (this.root == null)
            {
                throw new InvalidOperationException("The tree is empty");
            }

            this.root = this.DeleteMax(this.root);
        }

        public IEnumerable<T> Range(T startRange, T endRange)
        {
            return null;
        }

        public  void Delete(T element)
        {
            this.root = this.Delete(element, this.root);
        }

        public T Ceiling(T element)
        {
            return Select(this.Rank(element) + 1);
        }

        public T Floor(T element)
        {
            return Select(this.Rank(element) - 1);
        }

        public void EachInOrder(Action<T> action)
        {
            this.EachInOrder(action, this.root);
        }

        private void EachInOrder(Action<T> action, Node node)
        {
            if (node == null)
            {
                return;
            }

            EachInOrder(action, node.Left);
            action(node.Value);
            EachInOrder(action, node.Right);
        }

        private Node Insert(T element, Node node)
        {
            if (node == null)
            {
                return new Node(element);
            }

            var comp = element.CompareTo(node.Value);

            if (comp > 0)
            {
                node.Right = this.Insert(element, node.Right);
            }
            else if (comp < 0)
            {
                node.Left = this.Insert(element, node.Left);
            }

            if (this.IsRed(node.Right) && !this.IsRed(node.Left))
            {
                node = this.RotateLeft(node);
            }
            else if (this.IsRed(node.Left) && this.IsRed(node.Left.Left))
            {
                node = this.RotateRight(node);
            }
            else if (this.IsRed(node.Left) && this.IsRed(node.Right))
            {
                this.FlipColors(node);
            }

            this.UpdateCount(node);

            return node;
        }

        private void FlipColors(Node node)
        {
            node.Color = red;
            node.Left.Color = black;
            node.Right.Color = black;
        }

        private Node RotateRight(Node node)
        {
            var temp = node.Left;
            node.Left = temp.Right;
            temp.Right = node;
            temp.Color = node.Color;
            node.Color = red;

            this.UpdateCount(node);

            return temp;
        }

        private Node RotateLeft(Node node)
        {
            var temp = node.Right;
            node.Right = temp.Left;
            temp.Left = node;
            temp.Color = node.Color;
            node.Color = red;

            this.UpdateCount(node);

            return temp;
        }

        private int GetCount(Node node)
        {
            if (node == null)
            {
                return 0;
            }

            return node.Count;
        }

        private void UpdateCount(Node node)
        {
            node.Count = 1 + this.GetCount(node.Left) + this.GetCount(node.Right);
        }

        private bool IsRed(Node node)
        {
            return node != null && node.Color == red;
        }

        private Node FindElement(T element)
        {
            var current = this.root;

            while (current != null)
            {
                var comp = element.CompareTo(current.Value);

                if (comp < 0)
                {
                    current = current.Left;
                }
                else if (comp > 0)
                {
                    current = current.Right;
                }
                else
                {
                    break;
                }
            }

            return current;
        }

        private Node DeleteMin(Node node)
        {
            if (node.Left == null)
            {
                return node.Right;
            }

            node.Left = DeleteMin(node.Left);
            UpdateCount(node);

            return node;
        }

        private Node DeleteMax(Node node)
        {
            if (node.Right == null)
            {
                return node.Left;
            }

            node.Right = DeleteMax(node.Right);
            UpdateCount(node);

            return node;
        }

        private Node Delete(T element, Node node)
        {
            if (node == null)
            {
                return null;
            }

            var comp = element.CompareTo(node.Value);

            if (comp > 0)
            {
                node.Right = this.Delete(element, node.Right);
            }
            else if (comp < 0)
            {
                node.Left = this.Delete(element, node.Left);
            }
            else
            {
                if (node.Left == null)
                {
                    return node.Right;
                }
                else if (node.Right == null)
                {
                    return node.Left;
                }
                else
                {
                    Node temp = node;
                    node = this.FindMin(node.Right);
                    node.Right = DeleteMin(temp.Right);
                    node.Left = temp.Left;
                }
            }

            this.UpdateCount(node);
            return node;
        }

        private Node FindMin(Node node)
        {
            if (node.Left == null)
            {
                return node;
            }

            return FindMin(node.Left);
        }

        private int Rank(T element, Node node)
        {
            if (node == null)
            {
                return 0;
            }

            var comp = element.CompareTo(node.Value);

            if (comp < 0)
            {
                return Rank(element, node.Left);
            }
            else if (comp > 0)
            {
                return 1 + this.GetCount(node.Left) + Rank(element, node.Right);
            }
            else
            {
                return this.GetCount(node.Left);
            }
        }

        private Node Select(int rank, Node node)
        {
            if (node == null)
            {
                return null;
            }

            var leftCount = this.GetCount(node.Left);

            if (leftCount == rank)
            {
                return node;
            }
            else if (leftCount < rank)
            {
                return this.Select(rank - (leftCount + 1), node.Right);
            }
            else
            {
                return this.Select(rank, node.Left);
            }
        }

        private class Node
        {
            public Node(T value)
            {
                this.Value = value;
                this.Color = red;
                this.Count = 1;
            }

            public T Value { get; }
            public Node Left { get; set; }
            public Node Right { get; set; }
            public int Count { get; set; }
            public bool Color { get; set; }
        }
    }
}