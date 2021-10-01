namespace _03.MinHeap
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Runtime.InteropServices;

    public class MinHeap<T> : IAbstractHeap<T>
        where T : IComparable<T>
    {
        private List<T> _elements;

        public MinHeap()
        {
            this._elements = new List<T>();
        }

        public int Size => this._elements.Count;

        public T Dequeue()
        {
            this.EnsureNotEmpty();

            var toReturn = this._elements[0];
            this.Swap(0, this.Size - 1);
            this._elements.RemoveAt(this.Size - 1);

            this.HeapifyDown(0);

            return toReturn;
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
            int parentIndex = (currentIndex - 1) / 2;

            while (currentIndex > 0)
            {
                if (this.IsLesser(currentIndex, parentIndex))
                {
                    this.Swap(currentIndex, parentIndex);
                }
                else
                {
                    break;
                }

                currentIndex = parentIndex;
                parentIndex = (currentIndex - 1) / 2;
            }
        }

        private void HeapifyDown(int currentIndex)
        {

            int leftChildIndex = currentIndex * 2 + 1;
            int rightChildIndex = currentIndex * 2 + 2;
            
            if (rightChildIndex >= this.Size)
            {
                if (leftChildIndex >= this.Size)
                {
                    return;
                }

                if (this.IsGreater(currentIndex, leftChildIndex))
                {
                    this.Swap(currentIndex, leftChildIndex);
                    this.HeapifyDown(leftChildIndex);
                }
            }
            else
            {
                int indexToSwap = this.IsLesser(leftChildIndex, rightChildIndex) ? leftChildIndex : rightChildIndex;

                if (this.IsGreater(currentIndex, indexToSwap))
                {
                    this.Swap(currentIndex, indexToSwap);
                    this.HeapifyDown(indexToSwap);
                }
            }
        }

        private void Swap(int index1, int index2) 
        {
            var temp = this._elements[index1];
            this._elements[index1] = this._elements[index2];
            this._elements[index2] = temp;
        }

        private bool IsLesser(int child, int parent)
        {
            return this._elements[child].CompareTo(this._elements[parent]) < 0;
        }

        private bool IsGreater(int child, int parent)
        {
            return this._elements[child].CompareTo(this._elements[parent]) > 0;
        }


        private void EnsureNotEmpty()
        {
            if (this.Size == 0)
            {
                throw new InvalidOperationException("The heap is empty!");
            }
        }
    }
}
