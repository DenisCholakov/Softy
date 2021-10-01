using System;
using System.Collections.Generic;
using System.Text;

namespace CustomDataStructuresExercise
{
    class CustomList
    {
        private const int INITIAL_CAPACITY = 2;

        private int[] items;

        public CustomList()
        {
            this.items = new int[INITIAL_CAPACITY];
        }

        public int Count { get; private set; }

        public void Add(int item)
        {
            if (this.Count == this.items.Length)
            {
                this.Resize();
            }

            this.items[this.Count++] = item;
        }

        public int RemoveAt(int index)
        {
            this.ValidateIndex(index);
            int removedItem = this.items[index];
            this.ShiftToLeft(index);
            this.items[this.Count - 1] = default;
            this.Count--;

            if (this.Count <= this.items.Length / 4)
            {
                this.Shrink();
            }

            return removedItem;
        }

        public void Insert(int index, int item)
        {
            this.ValidateIndex(index);

            if (this.Count == this.items.Length)
            {
                this.Resize();
            }

            this.ShiftToRight(index);
            this.items[index] = item;
            this.Count++;
        }

        public bool Contains(int item)
        {
            for (int i = 0; i < this.Count; i++)
            {
                if (this.items[i] == item)
                {
                    return true;
                }
            }

            return false;
        }

        public void Swap (int firstIndex, int secondIndex)
        {
            this.ValidateIndex(firstIndex);
            this.ValidateIndex(secondIndex);

            // var1 -> Additional var
            //var temp = this.items[firstIndex];
            //this.items[firstIndex] = this.items[secondIndex];
            //this.items[secondIndex] = this.items[firstIndex];

            // var2 -> Arithmetics //Overflow
            // var3 -> Bitwise
            this.items[firstIndex] = this.items[firstIndex] ^ this.items[secondIndex];
            this.items[secondIndex] = this.items[firstIndex] ^ this.items[secondIndex];
            this.items[firstIndex] = this.items[firstIndex] ^ this.items[secondIndex];

        }

        public void Reverse()
        {
            // ToCheck

            for (int i = 0; i < this.Count / 2; i++)
            {
                for (int j = this.Count - 1; j >= this.Count / 2; j--)
                {
                    this.Swap(i, i + j);
                }
            }
        }

        public override string ToString() => String.Join(", ", this.items);

        public int this[int index]
        {
            get
            {
                this.ValidateIndex(index);

                return this.items[index];
            }
            set
            {
                this.ValidateIndex(index);

                this.items[index] = value;
            }
        }

        private void ValidateIndex(int index)
        {
            if (index < 0 || index >= this.Count)
            {
                throw new ArgumentOutOfRangeException("The index is invalid!");
            }
        }

        private void Resize()
        {
            int[] copy = new int[this.items.Length * 2];

            for (int i = 0; i < this.items.Length; i++)
            {
                copy[i] = this.items[i];
            }

            this.items = copy;
        }

        private void Shrink()
        {
            int[] copy = new int[this.items.Length / 2];

            for (int i = 0; i < this.Count; i++)
            {
                copy[i] = this.items[i];
            }

            this.items = copy;
        }

        private void ShiftToLeft(int index)
        {
            for (int i = index; i < this.Count - 1; i++)
            {
                this.items[i] = this.items[i + 1];
            }
        }

        private void ShiftToRight(int index)
        {
            for (int i = this.Count; i > index; i--)
            {
                this.items[i] = this.items[i - 1];
            }
        }
    }
}