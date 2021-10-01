namespace _05.TopView
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class BinaryTree<T> : IAbstractBinaryTree<T>
        where T : IComparable<T>
    {
        public BinaryTree(T value, BinaryTree<T> left, BinaryTree<T> right)
        {
            this.Value = value;
            this.LeftChild = left;
            this.RightChild = right;
        }

        public T Value { get; set; }

        public BinaryTree<T> LeftChild { get; set; }

        public BinaryTree<T> RightChild { get; set; }

        public List<T> TopView()
        {
            SortedDictionary<int, KeyValuePair<T, int>> topViewNodes = new SortedDictionary<int, KeyValuePair<T, int>>();

            this.TopViewPreOrder(topViewNodes, 0, 1, this);

            return topViewNodes.Values.Select(kvp => kvp.Key).ToList();
        }

        private void TopViewPreOrder(SortedDictionary<int, KeyValuePair<T, int>> topViewNodes, int dist, int level, BinaryTree<T> subtree)
        {

            if (subtree == null)
            {
                return;
            }

            if (!topViewNodes.ContainsKey(dist))
            {
                topViewNodes.Add(dist, new KeyValuePair<T, int>(subtree.Value, level));
            }
            else if (level < topViewNodes[dist].Value)
            {
                topViewNodes[dist] = new KeyValuePair<T, int>(subtree.Value, level);
            }

            this.TopViewPreOrder(topViewNodes, dist - 1, level + 1, subtree.LeftChild);
            this.TopViewPreOrder(topViewNodes, dist + 1, level + 1, subtree.RightChild);
           
        }
    }
}
