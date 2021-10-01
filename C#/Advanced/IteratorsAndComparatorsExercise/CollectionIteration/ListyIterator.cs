using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CollectionIteration
{
    public class ListyIterator<T> : IEnumerable<T>
    {
        private List<T> _elements;
        private int index;

        public ListyIterator(params T[] elements)
        {
            this._elements = elements.ToList();
            this.index = 0;
        }

        public bool Move()
        {
            if (this.HasNext())
            {
                index++;
                return true;
            }

            return false;
        }

        public void Print()
        {
            this.CheckIfEmpty();

            Console.WriteLine(this._elements[this.index]);
        }

        public bool HasNext()
        {
            return this.index < this._elements.Count - 1;
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < this._elements.Count; i++)
            {
                yield return this._elements[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private void CheckIfEmpty()
        {
            if (this._elements.Count == 0)
            {
                throw new InvalidOperationException("Invalid Operation!");
            }
        }
    }
}
