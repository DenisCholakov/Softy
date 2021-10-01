using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace CreateCustomStack
{
    public class CustomStack<T> : IEnumerable<T>
    {
        private Node<T> _top;

        public int Count { get; private set; }

        public void Push(T value)
        {
            Node<T> itemToAdd = new Node<T>(value, this._top);
            this._top = itemToAdd;
            this.Count++;
        }

        public T Pop()
        {
            this.CheckIfEmpty();
            Node<T> toReturn = this._top;
            this._top = this._top.Next;
            this.Count--;
            return toReturn.Value;
        }

        private void CheckIfEmpty()
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException("No elements");
            }
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
        {
            return this.GetEnumerator();
        }
    }
}
