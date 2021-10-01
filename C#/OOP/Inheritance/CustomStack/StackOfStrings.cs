using System;
using System.Collections.Generic;
using System.Text;

namespace CustomStack
{
    public class StackOfStrings : Stack<string>
    {
        public bool IsEmpty()
        {
            return this.Count == 0;
        }

        public void AddRange(Stack<string> toAdd)
        {
            while (toAdd.Count > 0)
            {
                this.Push(toAdd.Pop());
            }
        }
    }
}
