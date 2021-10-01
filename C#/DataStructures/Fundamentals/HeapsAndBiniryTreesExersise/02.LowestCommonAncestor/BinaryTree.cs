namespace _02.LowestCommonAncestor
{
    using System;
    using System.Collections.Generic;

    public class BinaryTree<T> : IAbstractBinaryTree<T>
        where T : IComparable<T>
    {
        public BinaryTree(
            T value,
            BinaryTree<T> leftChild,
            BinaryTree<T> rightChild)
        {
            this.Value = value;
            this.LeftChild = leftChild;
            this.RightChild = rightChild;

            if (this.LeftChild != null)
            {
                this.LeftChild.Parent = this;
            }

            if (this.RightChild != null)
            {
                this.RightChild.Parent = this;
            }
        }

        public T Value { get; set; }

        public BinaryTree<T> LeftChild { get; set; }

        public BinaryTree<T> RightChild { get; set; }

        public BinaryTree<T> Parent { get; set; }

        public T FindLowestCommonAncestor(T first, T second)
        {
            List<T> firstAncestors = this.FindAncestors(this, first);
            List<T> secondAncestors = this.FindAncestors(this, second);

            foreach (var parent in firstAncestors)
            {
                if (secondAncestors.Contains(parent))
                {
                    return parent;
                }
            }

            throw new InvalidOperationException("These nodes don't have a mutual parent!");
        }

        private List<T> FindAncestors(BinaryTree<T> tree, T element)
        {
            var result = new List<T>();
            Queue<BinaryTree<T>> queue = new Queue<BinaryTree<T>>();
            queue.Enqueue(tree);

            while (queue.Count != 0)
            {
                var subtree = queue.Dequeue();

                if (subtree.Value.Equals(element))
                {
                    while (subtree.Parent != null)
                    {
                        result.Add(subtree.Parent.Value);
                        subtree = subtree.Parent;
                    }

                    break;
                }

                if (subtree.LeftChild != null)
                {
                    queue.Enqueue(subtree.LeftChild);
                }

                if (subtree.RightChild != null)
                {
                    queue.Enqueue(subtree.RightChild);
                }
            }

            return result;
        }
    }
}
