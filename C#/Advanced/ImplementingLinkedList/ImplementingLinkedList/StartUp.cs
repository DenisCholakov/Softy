using System;

namespace CustomDoublyLinkedList
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            LinkedList<int> list = new LinkedList<int>();

            for (int i = 0; i < 5; i++)
            {
                list.AddHead(new Node<int>(i));
            }

            for (int i = 0; i < 5; i++)
            {
                list.AddLast(new Node<int>(i));
            }

            list.PrintList();
        }
    }
}
