using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Froggy
{
    class Lake : IEnumerable<int>
    {
        private int[] stones;

        public Lake(params int[] stones)
        {
            this.stones = stones;
        }

        public int Count => this.stones.Length;

        public IEnumerator<int> GetEnumerator()
        {
            for (int i = 0; i < this.Count; i += 2)
            {
                yield return this.stones[i];
            }

            for (int i = this.Count - 1; i >= 0; i--)
            {
                if (i % 2 != 0)
                {
                    yield return this.stones[i];
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
