namespace _03.PriorityQueue
{
    using System;
    using System.Collections.Generic;

    public class PriorityQueue<T> : IAbstractHeap<T>
        where T : IComparable<T>
    {
        private List<T> _elements;

        public PriorityQueue()
        {
            this._elements = new List<T>();
        }

        public int Size => this._elements.Count;

        public T Dequeue()
        {
            this.EnsureNotEmpty();
            var firstEelemet = this._elements[0];
            this.Swap(0, this.Size - 1);

            this._elements.RemoveAt(this.Size - 1);
            this.HeapifyDown();

            return firstEelemet;
        }

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

        private void HeapifyUp()
        {
            int currentIndex = this.Size - 1;
            int parentIndex = this.GetParentIndex(currentIndex);

            while (currentIndex > 0 && this.IsGreater(currentIndex, parentIndex))
            {
                this.Swap(currentIndex, parentIndex);
                currentIndex = parentIndex;
                parentIndex = this.GetParentIndex(currentIndex);
            }
        }

        private void HeapifyDown()
        {
            int currentIndex = 0;
            int leftChildIndex = this.GetLeftChildIndex(currentIndex);
            int rightChildIndex = this.GetRightChildIndex(currentIndex);
            int indexToSwap = leftChildIndex;
            if (IndexIsValid(rightChildIndex))
            {
                indexToSwap = this.IsGreater(leftChildIndex, rightChildIndex) ? leftChildIndex : rightChildIndex;
            }

            while (leftChildIndex < this.Size && this.IsLesser(currentIndex, indexToSwap))
            {
                this.Swap(currentIndex, indexToSwap);
                currentIndex = indexToSwap;
                leftChildIndex = this.GetLeftChildIndex(currentIndex);
                rightChildIndex = this.GetRightChildIndex(currentIndex);
                indexToSwap = leftChildIndex;
                if (rightChildIndex < this.Size)
                {
                    indexToSwap = this.IsGreater(leftChildIndex, rightChildIndex) ? leftChildIndex : rightChildIndex;
                }
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

        private bool IsLesser(int leftChildIndex, int rightChildIndex)
        {
            return this._elements[leftChildIndex].CompareTo(this._elements[rightChildIndex]) < 0;
        }

        private int GetParentIndex(int currentIndex)
        {
            return (currentIndex - 1) / 2;
        }

        private int GetLeftChildIndex(int index)
        {
            return (index * 2) + 1;
        }

        private int GetRightChildIndex(int index)
        {
            return (index * 2) + 2;
        }

        private void EnsureNotEmpty()
        {
            if (this._elements.Count == 0)
            {
                throw new InvalidOperationException("Max heap is empty");
            }
        }

        private bool IndexIsValid(int index)
        {
            return index >= 0 && index < this.Size;
        }
    }
}
