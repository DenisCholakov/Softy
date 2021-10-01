﻿namespace Problem01.FasterQueue
{
    public class Node<T>
    {
        public T Item { get; set; }

        public Node<T> Next { get; set; }

        public Node(T item, Node<T> next = null)
        {
            this.Item = item;
            this.Next = next;
        }
    }
}