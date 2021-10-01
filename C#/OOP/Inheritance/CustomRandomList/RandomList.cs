using System;
using System.Collections.Generic;
using System.Text;

namespace CustomRandomList
{
    class RandomList : List<string>
    {
        Random rand;

        public RandomList()
        {
            rand = new Random();
        }

        public string RandomString()
        {
            int index = rand.Next(this.Count);
            string removed = this[index];
            this.RemoveAt(index);
            return removed;
        }
    }
}
