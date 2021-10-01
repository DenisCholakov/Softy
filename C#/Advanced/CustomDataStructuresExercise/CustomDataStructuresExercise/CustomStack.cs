using System;
using System.Collections.Generic;
using System.Text;

namespace CustomDataStructuresExercise
{
    class CustomStack
    {
        private const int INITIAL_CAPACITY = 4;

        private int[] items;

        public CustomStack()
        {
            this.items = new int[INITIAL_CAPACITY];
        }

        public int Count { get; private set; }

        public void Push(int item)
        {
            if (this.Count == this.items.Length)
            {
                this.Resize();
            }

            this.items[this.Count++] = item;
        }

        public int Pop()
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException("The stack is emty");
            }

            int last = this.items[this.Count - 1];
            this.items[--this.Count] = default;

            return last;
        }

        public int Peek()
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException("The stack is emty");
            }

            return this.items[this.Count - 1];
        }

        public void ForEach(Action<int> action)
        {
            for (int i = 0; i < this.Count; i++)
            {
                action(this.items[i]);
            }
        }

        private void Resize()
        {
            int[] copy = new int[this.items.Length * 2];

            for (int i = 0; i < this.Count; i++)
            {
                copy[i] = this.items[i];
            }

            this.items = copy;
        }
    }
}
