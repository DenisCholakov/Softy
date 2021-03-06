using System.Drawing;

namespace Problem02.DoublyLinkedList
{
    public class Node<T>
    {
        public T Item { get; set; }

        public Node<T> Next { get; set; }

        public Node<T> Previous { get; set; }

        public Node()
        {
        }
        
        public Node(T item)
        {
            this.Item = item;
        }

        public Node(T item, Node<T> next, Node<T> previous)
        {
            this.Item = item;
            this.Next = next;
            this.Previous = previous;
        }
    }
}