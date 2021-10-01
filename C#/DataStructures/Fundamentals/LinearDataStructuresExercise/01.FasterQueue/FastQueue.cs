namespace Problem01.FasterQueue
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class FastQueue<T> : IAbstractQueue<T>
    {
        private Node<T> _head;
        private Node<T> _tail;
        public int Count { get; private set; }

        public bool Contains(T item)
        {
            var current = this._head;

            while (current != null)
            {

                if (current.Item.Equals(item))
                {
                    return true;
                }

                current = current.Next;
            }

            return false;
        }

        public T Dequeue()
        {
            this.CheckIfEmpty();
            Node<T> toReturn = this._head;
            this._head = this._head.Next;
            this.Count--;
            return toReturn.Item;
        }

        public void Enqueue(T item)
        {
            if (this._head is null)
            {
                this._head = new Node<T>(item);
                this._tail = this._head;
            }
            else
            {
                this._tail.Next = new Node<T>(item);
                this._tail = this._tail.Next;
            }

            Count++;
        }

        public T Peek()
        {
            this.CheckIfEmpty();
            return this._head.Item;
        }

        public IEnumerator<T> GetEnumerator()
        {
            var current = this._head;

            while (current != null)
            {
                yield return current.Item;
                current = current.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private void CheckIfEmpty()
        {
            if (this.Count == 0)
                throw new InvalidOperationException("The queue is rmpty!");
        }
    }
}