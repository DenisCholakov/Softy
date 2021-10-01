namespace Problem02.Stack
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class Stack<T> : IAbstractStack<T>
    {
        private Node<T> _top;

        public int Count { get; private set; }

        public bool Contains(T item)
        {
            Node<T> current = this._top;

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

        public T Peek()
        {
            this.CheckIfEmpty();
            return this._top.Value;
        }

        public T Pop()
        {
            this.CheckIfEmpty();
            T valueToReturn = this._top.Value;
            this._top = this._top.Next;
            this.Count--;
            return valueToReturn;
        }

        public void Push(T item)
        {
            Node<T> itemToPush = new Node<T>(item, this._top);
            this._top = itemToPush;
            this.Count++;
        }

        public IEnumerator<T> GetEnumerator()
        {
            Node<T> current = this._top;
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
            if (Count == 0)
            {
                throw new InvalidOperationException("The stack is empty");
            }
        }
    }
}