using System;
using System.Collections.Generic;

namespace CustomDoublyLinkedList
{
    public class LinkedList<T>
    {
        public Node<T> Head { get; set; }
        public Node<T> Tail { get; set; }

        public void ForEach(Action<Node<T>> action)
        {
            Node<T> current = Head;

            while (current != null)
            {
                action(current);
                current = current.Next;
            }
        }

        public void AddHead(Node<T> node)
        {
            if (Head == null)
            {
                Head = node;
                Tail = node;
            }
            else
            {
                node.Next = Head;
                Head.Previous = node;
                Head = node;
            }
        }

        public void AddLast(Node<T> node)
        {
            if (Tail == null)
            {
                Head = node;
                Tail = node;
            }
            else
            {
                node.Previous = Tail;
                Tail.Next = node;
                Tail = node;
            }
        }

        public Node<T> RemoveFirst()
        {
            var oldHead = Head;
            this.Head = this.Head.Next;
            this.Head.Previous = null;
            return oldHead;
        }

        public Node<T> RemoveLast()
        {
            var oldTail = Tail;
            this.Tail = this.Tail.Previous;
            this.Tail.Next = null;
            return oldTail;
        }

        public bool Remove(T value)
        {
            Node<T> current = Head;

            while (current != null)
            {
                if (current.Value.Equals(value))
                {
                    current.Previous.Next = current.Next;
                    current.Next.Previous = current.Previous;
                    return true;
                }
            }

            return false;
        }

        public Node<T> Peek() => this.Head;

        public void PrintList()
        {
            this.ForEach(node => Console.WriteLine(node.Value));
        }

        public void ReversePrintList()
        {
            Node<T> current = Tail;
            while (current != null)
            {
                Console.WriteLine(current.Value);
                current = current.Previous;
            }
        }

        public T[] ToArray()
        {
            List<T> list = new List<T>();
            this.ForEach(node => list.Add(node.Value));
            return list.ToArray();
        }
    }
}
