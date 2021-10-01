namespace _01.Hierarchy
{
    using System;
    using System.Collections.Generic;
    using System.Collections;
    using System.Linq;

    public class Hierarchy<T> : IHierarchy<T>
    {
        private Dictionary<T, Node<T>> _elements;
        private Node<T> root;

        public Hierarchy(T root)
        {
            this.root = new Node<T>(root);
            this._elements = new Dictionary<T, Node<T>>();
            this._elements.Add(root, this.root);
        }

        public int Count => this._elements.Count;

        public void Add(T element, T child)
        {
            this.HasElement(element);

            if (this._elements.ContainsKey(child))
            {
                throw new ArgumentException("This value already exists!");
            }

            Node<T> toAdd = new Node<T>(child, this._elements[element]);
            this._elements[element].Children.Add(toAdd);
            this._elements.Add(child, toAdd);
        }

        public void Remove(T element)
        {
            this.HasElement(element);

            if (this.root.Value.Equals(element))
            {
                throw new InvalidOperationException("Can't remove the root!");
            }

            Node<T> toRemove = this._elements[element];
            this._elements.Remove(element);
            toRemove.Parent.Children.Remove(toRemove);

            foreach (var child in toRemove.Children)
            {
                child.Parent = toRemove.Parent;
                toRemove.Parent.Children.Add(child);
            }
        }

        public IEnumerable<T> GetChildren(T element)
        {
            this.HasElement(element);

            return this._elements[element].Children.Select(v => v.Value);
        }

        public T GetParent(T element)
        {
            this.HasElement(element);
            Node<T> node = this._elements[element];

            return node.Parent != null ? node.Parent.Value : default(T);
        }

        public bool Contains(T element)
        {
            return this._elements.ContainsKey(element);
        }

        public IEnumerable<T> GetCommonElements(Hierarchy<T> other)
        {
            foreach (var el in this._elements)
            {
                if (other.Contains(el.Value.Value))
                {
                    yield return el.Value.Value;
                }
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            Queue<Node<T>> queue = new Queue<Node<T>>();
            queue.Enqueue(this.root);

            while (queue.Count > 0)
            {
                Node<T> current = queue.Dequeue();

                yield return current.Value;

                foreach (var child in current.Children)
                {
                    queue.Enqueue(child);
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private void HasElement(T element)
        {
            if (!this.Contains(element))
            {
                throw new ArgumentException("The elements does not exist in the Hierarchy!");
            }
        }
    }
}