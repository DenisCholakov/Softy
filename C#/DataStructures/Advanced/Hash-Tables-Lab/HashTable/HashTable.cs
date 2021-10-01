namespace HashTable
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public class HashTable<TKey, TValue> : IEnumerable<KeyValue<TKey, TValue>>
    {
        private const int INITIAL_CAPACITY = 100;
        private List<KeyValue<TKey, TValue>>[] buckets;

        public int Count { get; private set; }

        public int Capacity
        {
            get
            {
                return this.buckets.Length;
            }
        }

        public HashTable() : this(INITIAL_CAPACITY)
        {
        }

        public HashTable(int capacity)
        {
            this.buckets = new List<KeyValue<TKey, TValue>>[capacity];
        }

        public void Add(TKey key, TValue value)
        {

            if (this.ContainsKey(key))
            {
                throw new ArgumentException("The key already exists");
            }

            var item = new KeyValue<TKey, TValue>(key, value);
            this.AddItem(item);
            Count++;

            this.ResizeAndRefresh();
        }

        public bool AddOrReplace(TKey key, TValue value)
        {
            var item = this.Find(key);

            if (item != null)
            {
                item.Value = value;
            }
            else
            {
                this.Add(key, value);
            }

            return true;
        }

        public TValue Get(TKey key)
        {
            return this.GetItemOrReturnException(key);
        }

        public TValue this[TKey key]
        {
            get
            {
                return GetItemOrReturnException(key);
                // Note: throw an exception on missing key
            }
            set
            {
                this.AddOrReplace(key, value);
            }
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            value = default(TValue);
            var item = this.Find(key);

            if (item != null)
            {
                value = item.Value;
            }

            return item != null;
        }

        public KeyValue<TKey, TValue> Find(TKey key)
        {
            var index = GetIndex(key);

            return this.buckets[index]?.FirstOrDefault(item => item.Key.Equals(key));
        }

        public bool ContainsKey(TKey key)
        {
            return this.Find(key) != null;
        }

        public bool Remove(TKey key)
        {
            var item = this.Find(key);

            if (item != null)
            {
                var index = this.GetIndex(key);
                buckets[index].Remove(item);
                buckets[index] = buckets[index].Count == 0 ? null : buckets[index];
                this.Count--;

                return true;
            }

            return false;
        }

        public void Clear()
        {
            this.buckets = new List<KeyValue<TKey, TValue>>[INITIAL_CAPACITY];
            this.Count = 0;
        }

        public IEnumerable<TKey> Keys
        {
            get
            {
                return buckets
                    .Where(item => item != null)
                    .SelectMany(bucket => bucket.Select(i => i.Key));
            }
        }

        public IEnumerable<TValue> Values
        {
            get
            {
                return buckets
                    .Where(item => item != null)
                    .SelectMany(bucket => bucket.Select(i => i.Value));
            }
        }

        public IEnumerator<KeyValue<TKey, TValue>> GetEnumerator()
        {
            for (int i = 0; i < this.buckets.Length; i++)
            {
                if (this.buckets[i] == null)
                {
                    continue;
                }

                foreach (var pair in this.buckets[i])
                {
                    yield return pair;
                }

            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private void AddItem(KeyValue<TKey, TValue> item)
        {
            var index = GetIndex(item.Key);

            if (this.buckets[index] == null)
            {
                buckets[index] = new List<KeyValue<TKey, TValue>>();
            }

            buckets[index].Add(item);
        }

        private int GetIndex(TKey key)
        {
            var hash = key.GetHashCode();
            return Math.Abs(hash % this.buckets.Length);
        }

        private TValue GetItemOrReturnException(TKey key)
        {
            var item = this.Find(key);

            if (item == null)
            {
                throw new KeyNotFoundException("The key was not found" + key);
            }

            return item.Value;
        }

        private void ResizeAndRefresh()
        {
            if (Count / (double)this.Capacity >= 0.75)
            {
                var oldBuckets = this.buckets;
                this.buckets = new List<KeyValue<TKey, TValue>>[this.Capacity * 2];
                foreach (var bucket in oldBuckets)
                {
                    if (bucket == null)
                    {
                        continue;
                    }

                    foreach (var item in bucket)
                    {
                        this.AddItem(item);
                    }
                }
            }
        }
    }
}
