namespace _02.MaxHeap
{
    using System;
    using System.Collections.Generic;

    public class MaxHeap<T> : IAbstractHeap<T>
        where T : IComparable<T>
    {

        private List<T> _elements;

        public MaxHeap()
        {
            this._elements = new List<T>();
        }
        public int Size { get { return this._elements.Count; } }

        public void Add(T element)
        {
            this._elements.Add(element);

            this.HeapifyUp();
        }

        public T Peek()
        {
            this.EnsureNotEmpty();

            return this._elements[0];
        }

        private void EnsureNotEmpty()
        {
            if (this.Size == 0)
            {
                throw new InvalidOperationException("Max heap is empty");
            }
        }

        private void HeapifyUp()
        {
            int currentIndex = this.Size - 1;
            int parentIndex = (currentIndex - 1) / 2;

            while (currentIndex > 0 && this.IsGreater(currentIndex, parentIndex))
            {
                this.Swap(currentIndex, parentIndex);
                currentIndex = parentIndex;
                parentIndex = (currentIndex - 1) / 2;
            }

        }

        private void Swap(int currentIndex, int parentIndex)
        {
            var temp = this._elements[currentIndex];
            this._elements[currentIndex] = this._elements[parentIndex];
            this._elements[parentIndex] = temp;
        }

        private bool IsGreater(int currentIndex, int parentIndex)
        {
            return this._elements[currentIndex].CompareTo(this._elements[parentIndex]) > 0;
        }
    }
}
