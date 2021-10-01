namespace _04.BinarySearchTree
{
    using System;

    public class BinarySearchTree<T> : IAbstractBinarySearchTree<T>
        where T : IComparable<T>
    {
        public BinarySearchTree()
        {
        }

        public BinarySearchTree(Node<T> root)
        {
            this.Copy(root);
        }

        private void Copy(Node<T> root)
        {
            if (root == null)
            {
                return;
            }

            this.Insert(root.Value);
            this.Copy(root.LeftChild);
            this.Copy(root.RightChild);
        }

        public Node<T> Root { get; private set; }

        public Node<T> LeftChild { get; private set; }

        public Node<T> RightChild { get; private set; }

        public T Value => this.Root.Value;

        public bool Contains(T element)
        {
            Node<T> current = this.Root;
            while (current != null)
            {
                if (element.CompareTo(current.Value) < 0)
                {
                    current = current.LeftChild;
                }
                else if (element.CompareTo(current.Value) > 0)
                {
                    current = current.RightChild;
                }
                else
                {
                    return true;
                }
            }

            return false;
        }

        public void Insert(T element)
        {
            Node<T> toInsert = new Node<T>(element);
            if (this.Root == null)
            {
                this.Root = toInsert;
                return;
            }

            this.InsertNode(this.Root, toInsert, element);
        }

        public IAbstractBinarySearchTree<T> Search(T element)
        {
            var current = this.Root;

            while (current != null && element.CompareTo(current.Value) != 0)
            {
                if (element.CompareTo(current.Value) < 0)
                {
                    current = current.LeftChild;
                }
                else if(element.CompareTo(current.Value) > 0)
                {
                    current = current.RightChild;
                }
            }

            return new BinarySearchTree<T>(current);
        }

        private void InsertNode(Node<T> node, Node<T> toInsert, T element)
        {
            if (element.CompareTo(node.Value) < 0)
            {
                if (node.LeftChild == null)
                {
                    node.LeftChild = toInsert;

                    if (this.LeftChild == null)
                    {
                        this.LeftChild = toInsert;
                    }

                    return;
                }

                this.InsertNode(node.LeftChild, toInsert, element);
                return;
            }
            else if (element.CompareTo(node.Value) > 0)
            {
                if (node.RightChild == null)
                {
                    node.RightChild = toInsert;

                    if (this.RightChild == null)
                    {
                        this.RightChild = toInsert;
                    }

                    return;
                }

                this.InsertNode(node.RightChild, toInsert, element);
                return;
            }
            else
            {
                return;
            }
        }
    }
}
