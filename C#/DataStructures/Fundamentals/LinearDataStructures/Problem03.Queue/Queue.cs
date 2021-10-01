namespace Problem03.Queue
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class Queue<T> : IAbstractQueue<T>
    {

        //FIFO

        private Node<T> _head;

        public int Count { get; private set; }

        public bool Contains(T item)
        {
            Node<T> current = this._head;

            while (current != null)
            {
                if (current.Value.Equals(item))
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
            T itemToReturn = this._head.Value;
            this._head = this._head.Next;
            Count--;
            return itemToReturn;
        }

        public void Enqueue(T item)
        {

            if (Count == 0)
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

        public T Peek()
        {
            this.CheckIfEmpty();
            return this._head.Value;
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
                throw new InvalidOperationException("The gueue is empty!");
            }
        }
    }
}