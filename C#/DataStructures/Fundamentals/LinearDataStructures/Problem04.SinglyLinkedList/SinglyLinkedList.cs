namespace Problem04.SinglyLinkedList
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class SinglyLinkedList<T> : IAbstractLinkedList<T>
    {
        private Node<T> _head;

        public int Count { get; private set; }

        public void AddFirst(T item)
        {
            this._head = new Node<T>(item, this._head);
            this.Count++;
        }

        public void AddLast(T item)
        {

            if (this.Count == 0)
            {
                this._head = new Node<T>(item);
            }
            else
            {
                Node<T> current = this._head;

                while (current.Next != null)
                {
                    current = current.Next;
                }

                current.Next = new Node<T>(item);
            }

            Count++;
        }

        public T GetFirst()
        {
            this.CheckIfEmpty();
            return this._head.Value;
        }

        public T GetLast()
        {
            this.CheckIfEmpty();
            Node<T> current = this._head;

            while (current.Next != null)
            {
                current = current.Next;
            }

            return current.Value;
        }

        public T RemoveFirst()
        {
            this.CheckIfEmpty();
            Node<T> nodeToReturn = this._head;
            this._head = this._head.Next;
            this.Count--;
            return nodeToReturn.Value;
        }

        public T RemoveLast()
        {
            this.CheckIfEmpty();
            var lastNode = this._head;

            if (Count == 1)
            {
                this._head = null;
            }
            else
            {
                var current = this._head;

                while (current.Next.Next != null)
                {
                    current = current.Next;
                }

                lastNode = current.Next;
                current.Next = null;
            }

            Count--;
            return lastNode.Value;
        }

        public IEnumerator<T> GetEnumerator()
        {
            Node<T> current = this._head;

            while (current != null)
            {
                yield return current.Value;
                current = current.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
            => this.GetEnumerator();

        private void CheckIfEmpty()
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException("The list is empty!");
            }
        }
    }
}