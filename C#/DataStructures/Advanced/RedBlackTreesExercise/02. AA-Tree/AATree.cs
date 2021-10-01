namespace _02._AA_Tree
{
    using System;

    public class AATree<T> : IBinarySearchTree<T>
        where T : IComparable<T>
    {
        private Node<T> root;

        public int CountNodes() => this.GetCount(this.root);

        public bool IsEmpty()
        {
            return this.root == null;
        }

        public void Clear()
        {
            this.root = null;
        }

        public void Insert(T element)
        {
            this.root = this.Insert(element, this.root);
        }

        public bool Search(T element)
        {
            var node = this.FindElement(element);
            return node != null;
        }

        public void InOrder(Action<T> action)
        {
            this.VisitInOrder(action, this.root);
        }

        public void PreOrder(Action<T> action)
        {
            this.VisitPreOrder(action, this.root);
        }

        public void PostOrder(Action<T> action)
        {
            this.VisitPostOrder(action, this.root);
        }

        private Node<T> Insert(T element, Node<T> node)
        {
            if (node == null)
            {
                return new Node<T>(element);
            }

            var comp = element.CompareTo(node.Element);

            if (comp < 0)
            {
                node.Left = this.Insert(element, node.Left);
            }
            else if (comp > 0)
            {
                node.Right = this.Insert(element, node.Right);
            }

            node = this.Skew(node);
            node = this.Split(node);

            this.UpdateCount(node);

            return node;
        }

        private Node<T> Skew(Node<T> node)
        {
            if (node.Level == node.Left?.Level)
            {
                var temp = node.Left;
                node.Left = temp.Right;
                temp.Right = node;

                this.UpdateCount(node);

                return temp;
            }

            return node;
        }

        private Node<T> Split(Node<T> node)
        {
            if (node.Level == node.Right?.Right?.Level)
            {
                var temp = node.Right;
                node.Right = temp.Left;
                temp.Left = node;

                this.UpdateCount(node);
                temp.Level = this.GetLevel(temp.Right) + 1;

                return temp;
            }

            return node;
        }

        private int GetCount(Node<T> node)
        {
            if (node == null)
            {
                return 0;
            }

            return node.Count;
        }

        private void UpdateCount(Node<T> node)
        {
            node.Count = 1 + this.GetCount(node.Left) + this.GetCount(node.Right);
        }

        private int GetLevel(Node<T> node)
        {
            if (node == null)
            {
                return 0;
            }

            return node.Level;
        }

        private void VisitInOrder(Action<T> action, Node<T> node)
        {
            if (node == null)
            {
                return;
            }

            this.VisitInOrder(action, node.Left);
            action(node.Element);
            this.VisitInOrder(action, node.Right);
        }

        private void VisitPreOrder(Action<T> action, Node<T> node)
        {
            if (node == null)
            {
                return;
            }

            action(node.Element);
            this.VisitPreOrder(action, node.Left);
            this.VisitPreOrder(action, node.Right);
        }

        private void VisitPostOrder(Action<T> action, Node<T> node)
        {
            if (node == null)
            {
                return;
            }

            this.VisitPostOrder(action, node.Left);
            this.VisitPostOrder(action, node.Right);
            action(node.Element);
        }

        private Node<T> FindElement(T element)
        {
            var current = this.root;

            while (current != null)
            {
                var comp = element.CompareTo(current.Element);

                if (comp > 0)
                {
                    current = current.Right;
                }
                else if (comp < 0)
                {
                    current = current.Left;
                }
                else
                {
                    break;
                }
            }

            return current;
        }
    }
}