
namespace _03.AVL
{
    using System;
    using System.Security.Cryptography.X509Certificates;

    public class AVL<T> where T : IComparable<T>
    {
        public Node<T> Root { get; private set; }

        public bool Contains(T item)
        {
            var node = this.Search(this.Root, item);
            return node != null;
        }

        public void Insert(T item)
        {
            this.Root = this.Insert(this.Root, item);
        }

        public void Delete(T item)
        {
            this.Root = this.Remove(this.Root, item);
            UpdateHeight(this.Root);
        }

        public void DeleteMin()
        {
            if (this.Root == null)
            {
                return;
            }

            this.Delete(this.FindMin(this.Root).Value);
        }

        public void EachInOrder(Action<T> action)
        {
            this.EachInOrder(this.Root, action);
        }

        private Node<T> Insert(Node<T> node, T item)
        {
            if (node == null)
            {
                return new Node<T>(item);
            }

            int cmp = item.CompareTo(node.Value);
            if (cmp < 0)
            {
                node.Left = this.Insert(node.Left, item);
            }
            else if (cmp > 0)
            {
                node.Right = this.Insert(node.Right, item);
            }

            node = Balance(node);
            UpdateHeight(node);
            return node;
        }

        private Node<T> Remove(Node<T> node, T item)
        {
            if (node == null)
            {
                return null;
            }

            int cmp = item.CompareTo(node.Value);

            if (cmp < 0)
            {
                node.Left = this.Remove(node.Left, item);
                this.UpdateHeight(node);
            }
            else if (cmp > 0)
            {
                node.Right = this.Remove(node.Right, item);
                this.UpdateHeight(node);
            }
            else
            {
                if (node.Left == null && node.Right == null)
                {
                    return null;
                }
                else if(node.Left == null)
                {
                    return node.Right;
                }
                else if (node.Right == null)
                {
                    return node.Left;
                }
                else
                {
                    if (node.Left.Height > node.Right.Height)
                    {
                        var replacement = this.FindMax(node.Left);
                        node.Left.Value = replacement.Value;
                        node.Left = this.Remove(node.Left, replacement.Value);
                        this.UpdateHeight(node.Left);
                        this.UpdateHeight(node);
                    }
                    else
                    {
                        var replacement = this.FindMin(node.Right);
                        node.Value = replacement.Value;
                        node.Right = this.Remove(node.Right, replacement.Value);
                        this.UpdateHeight(node.Right);
                        this.UpdateHeight(node);
                    }
                }
            }

            return this.Balance(node);
        }

        private Node<T> Balance(Node<T> node)
        {
            var balance = Height(node.Left) - Height(node.Right);
            if (balance > 1)
            {
                var childBalance = Height(node.Left.Left) - Height(node.Left.Right);
                if (childBalance < 0)
                {
                    node.Left = RotateLeft(node.Left);
                }

                node = RotateRight(node);
            }
            else if (balance < -1)
            {
                var childBalance = Height(node.Right.Left) - Height(node.Right.Right);
                if (childBalance > 0)
                {
                    node.Right = RotateRight(node.Right);
                }

                node = RotateLeft(node);
            }

            return node;
        }

        private void UpdateHeight(Node<T> node)
        {
            if (node == null)
            {
                return;
            }
            node.Height = Math.Max(Height(node.Left), Height(node.Right)) + 1;
        }

        private Node<T> Search(Node<T> node, T item)
        {
            if (node == null)
            {
                return null;
            }

            int cmp = item.CompareTo(node.Value);
            if (cmp < 0)
            {
                return Search(node.Left, item);
            }
            else if (cmp > 0)
            {
                return Search(node.Right, item);
            }

            return node;
        }

        private void EachInOrder(Node<T> node, Action<T> action)
        {
            if (node == null)
            {
                return;
            }

            this.EachInOrder(node.Left, action);
            action(node.Value);
            this.EachInOrder(node.Right, action);
        }

        private int Height(Node<T> node)
        {
            if (node == null)
            {
                return 0;
            }

            return node.Height;
        }

        private Node<T> RotateRight(Node<T> node)
        {
            var left = node.Left;
            node.Left = left.Right;
            left.Right = node;

            UpdateHeight(node);

            return left;
        }

        private Node<T> RotateLeft(Node<T> node)
        {
            var right = node.Right;
            node.Right = right.Left;
            right.Left = node;

            UpdateHeight(node);

            return right;
        }

        private Node<T> FindMin(Node<T> node)
        {
            if (node == null)
            {
                return null;
            }

            Node<T> current = node;

            while (current.Left != null)
            {
                current = current.Left;
            }

            return current;
        }

        private Node<T> FindMax(Node<T> node)
        {
            if (node == null)
            {
                return null;
            }

            Node<T> current = node;

            while (current.Right != null)
            {
                current = current.Right;
            }

            return current;
        }
    }
}
