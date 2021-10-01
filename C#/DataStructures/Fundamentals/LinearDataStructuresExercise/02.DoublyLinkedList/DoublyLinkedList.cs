namespace Problem02.DoublyLinkedList
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class DoublyLinkedList<T> : IAbstractLinkedList<T>
    {
        private Node<T> head;
        private Node<T> tail;

        public int Count { get; private set; }

        public void AddFirst(T item)
        {
            if (this.Count == 0)
            {
                this.head = new Node<T>(item);
                this.tail = this.head;
            }
            else
            {
                this.head.Previous = new Node<T>(item, this.head, null);
                this.head = this.head.Previous;
            }

            this.Count++;
        }

        public void AddLast(T item)
        {
            if (this.Count == 0)
            {
                this.tail = new Node<T>(item);
                this.head = this.tail;
            }
            else
            {
                this.tail.Next = new Node<T>(item, null, this.tail);
                this.tail = this.tail.Next;
            }

            this.Count++;
        }

        public T GetFirst()
        {
            this.CheckIfEmpty();
            return this.head.Item;
        }

        public T GetLast()
        {
            this.CheckIfEmpty();
            return this.tail.Item;
        }

        public T RemoveFirst()
        {
            this.CheckIfEmpty();
            T itemToReturn = this.head.Item;
            this.head = this.head.Next;
            if(this.head != null)
                this.head.Previous = null;
            this.Count--;
            return itemToReturn;
        }

        public T RemoveLast()
        {
            this.CheckIfEmpty();
            T itemToReturn = this.tail.Item;
            this.tail = this.tail.Previous;
            if (this.tail != null)
                this.tail.Next = null;
            this.Count--;
            return itemToReturn;
        }

        public IEnumerator<T> GetEnumerator()
        {
            var current = this.head;

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
            {
                throw new InvalidOperationException("The list is empty!");
            }
        }
    }
}