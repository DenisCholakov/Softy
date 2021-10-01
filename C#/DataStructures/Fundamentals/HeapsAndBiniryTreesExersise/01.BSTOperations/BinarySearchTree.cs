namespace _01.BSTOperations
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Runtime.CompilerServices;
    using System.Transactions;

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

        public Node<T> Root { get; private set; }

        public Node<T> LeftChild { get; private set; }

        public Node<T> RightChild { get; private set; }

        public T Value => this.Root.Value;

        public int Count => this.Root.Count;

        public bool Contains(T element)
        {
            Node<T> current = this.Root;

            while (current != null)
            {
                if (this.AreEqual(element, current.Value))
                {
                    return true;
                }

                if (this.IsLess(element, current.Value))
                {
                    current = current.LeftChild;
                }
                else if (this.IsGreater(element, current.Value))
                {
                    current = current.RightChild;
                }
            }

            return false;
        }

        public void Insert(T element)
        {
            if (element == null)
            {
                throw new NullReferenceException("Can't insert an empty value!");
            }

            Node<T> toInsert = new Node<T>(element, null, null);

            if (this.Root == null)
            {
                this.Root = toInsert;
            }
            else
            {
                this.InsertDfs(this.Root, null, toInsert);
            }
        }

        public IAbstractBinarySearchTree<T> Search(T element)
        {
            Node<T> current = this.Root;

            while (current != null)
            {
                if (this.AreEqual(element, current.Value))
                {
                    break;
                }

                if (this.IsLess(element, current.Value))
                {
                    current = current.LeftChild;
                }
                else
                {
                    current = current.RightChild;
                }
            }

            return new BinarySearchTree<T>(current);

        }

        public void EachInOrder(Action<T> action)
        {
            this.EachInOrderDfs(this.Root, action);
        }

        private void EachInOrderDfs(Node<T> current, Action<T> action)
        {
            if (current == null)
            {
                return;
            }

            this.EachInOrderDfs(current.LeftChild, action);
            action.Invoke(current.Value);
            this.EachInOrderDfs(current.RightChild, action);

        }

        public List<T> Range(T lower, T upper)
        {
            var result = new List<T>();
            Queue<Node<T>> queue = new Queue<Node<T>>();

            queue.Enqueue(this.Root);

            while (queue.Count != 0)
            {
                var current = queue.Dequeue();
                if (current.Value.CompareTo(lower) >= 0 && current.Value.CompareTo(upper) <= 0)
                {
                    result.Add(current.Value);
                }

                if (current.LeftChild != null)
                {
                    queue.Enqueue(current.LeftChild);
                }

                if (current.RightChild != null)
                {
                    queue.Enqueue(current.RightChild);
                }

            }

            return result;
        }

        public void DeleteMin()
        {
            if (this.Root == null)
            {
                throw new InvalidOperationException("The tree is empty!");
            }

            if (this.Root.LeftChild == null)
            {
                this.Root = this.Root.RightChild;
                return;
            }

            Node<T> current = this.Root;
            Node<T> previous = null;

            while (current.LeftChild != null)
            {
                current.Count--;
                previous = current;
                current = current.LeftChild;
            }

            previous.LeftChild = current.RightChild;  
        }

        public void DeleteMax()
        {
            if (this.Root == null)
            {
                throw new InvalidOperationException("The tree is empty!");
            }

            if (this.Root.RightChild == null)
            {
                this.Root = this.Root.LeftChild;
                return;
            }

            Node<T> current = this.Root;
            Node<T> previous = null;

            while (current.RightChild != null)
            {
                current.Count--;
                previous = current;
                current = current.RightChild;
            }

            previous.RightChild = current.LeftChild;
        }

        public int GetRank(T element)
        {
            return this.GetRankDfs(this.Root, element);
        }

        private int GetRankDfs(Node<T> current, T element)
        {
            if (current == null)
            {
                return 0;
            }

            if (this.IsLess(element, current.Value))
            {
                return this.GetRankDfs(current.LeftChild, element);
            }
            else if (this.IsGreater(element, current.Value))
            {
                return this.GetLeftChildRank(current) + 1 + this.GetRankDfs(current.RightChild, element);
            }
            {
                return this.GetLeftChildRank(current);
            }
        }

        private int GetLeftChildRank(Node<T> el)
        {
            return el.LeftChild == null ? 0 : el.LeftChild.Count;
        }
        private void InsertDfs(Node<T> current, Node<T> previous, Node<T> toInsert)
        {
            if (current == null)
            {
                if (this.IsLess(toInsert.Value, previous.Value))
                {
                    previous.LeftChild = toInsert;

                    if (this.LeftChild == null)
                    {
                        this.LeftChild = this.Root.LeftChild;
                    }

                    return;
                }

                previous.RightChild = toInsert;

                if (this.RightChild == null)
                {
                    this.RightChild = this.Root.RightChild;
                }

                return;
            }

            if (this.IsLess(toInsert.Value, current.Value))
            {
                this.InsertDfs(current.LeftChild, current, toInsert);
                current.Count++;
            }
            else if (this.IsGreater(toInsert.Value, current.Value))
            {
                this.InsertDfs(current.RightChild, current, toInsert);
                current.Count++;
            }
        }

        private void Copy(Node<T> current)
        {
            if (current == null)
            {
                return;
            }
            this.Insert(current.Value);
            this.Copy(current.LeftChild);
            this.Copy(current.RightChild);
        }

        private bool IsLess(T el1, T el2)
        {
            return el1.CompareTo(el2) < 0;
        }
        
        private bool IsGreater(T el1, T el2)
        {
            return el1.CompareTo(el2) > 0;
        }

        private bool AreEqual(T el1, T el2)
        {
            return el1.CompareTo(el2) == 0;
        }
    }
}
