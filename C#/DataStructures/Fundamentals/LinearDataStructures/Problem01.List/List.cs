namespace Problem01.List
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Security.Cryptography;
    using System.Xml.Schema;

    public class List<T> : IAbstractList<T>
    {
        private const int DEFAULT_CAPACITY = 4;
        private T[] _items;

        public List()
            : this(DEFAULT_CAPACITY) {
        }

        public List(int capacity)
        {

            if (capacity <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(capacity));
            }

            this._items = new T[capacity];
        }

        public T this[int index]
        {
            get
            {
                this.ValidateIndex(index);
                return this._items[index];
            }
            set
            {
                this.ValidateIndex(index);
                this._items[index] = value;
            }
        }

        public int Count { get; private set; }

        public void Add(T item)
        {
            this.ResizeIfNeeded();
            this._items[Count++] = item;
        }

        public bool Contains(T item)
        {
            return this.IndexOf(item) != -1;
        }


        public int IndexOf(T item)
        {
            for (int i = 0; i < this._items.Length; i++)
            {
                if (this._items[i].Equals(item))
                {
                    return i;
                }
            }

            return -1;
        }

        public void Insert(int index, T item)
        {
            this.ValidateIndex(index);
            this.ResizeIfNeeded();

            for (int i = this.Count - 1; i >= index; i--)
            {
                this._items[i + 1] = this._items[i];
            }

            this._items[index] = item;

            Count++;
        }

        public bool Remove(T item)
        {
            int index = this.IndexOf(item);

            if (index != -1)
            {
                this.RemoveAt(index);
                return true;
            }

            return false;
        }

        public void RemoveAt(int index)
        {
            this.ValidateIndex(index);

            for (int i = index; i < this.Count - 1; i++)
            {
                this._items[i] = this._items[i + 1];
            }

            this._items[--Count] = default(T);
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < this.Count; i++)
            {
                yield return this._items[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
            => this.GetEnumerator();

        private void ResizeIfNeeded()
        {
            if (this.Count == this._items.Length)
            {
                T[] doubleSize = new T[this._items.Length * 2];

                for (int i = 0; i < this._items.Length; i++)
                {
                    doubleSize[i] = this._items[i];
                }

                this._items = doubleSize;
            }
        }

        private void ValidateIndex(int index)
        {

            if (index < 0 || index >= this.Count)
            {
                throw new IndexOutOfRangeException($"The given index {index} is invalid");
            }

        }
    }
}