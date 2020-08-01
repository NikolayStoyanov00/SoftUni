using System;
using System.Collections.Generic;
using System.Text;

namespace CustomStack
{
    public class StackOfStrings : Stack<string>
    {
        public StackOfStrings()
        {

        }
        public bool IsEmpty()
        {
            return this.Count == 0;
        }

        public Stack<string> AddRange(Stack<string> collection)
        {
            foreach (var element in collection)
            {
                this.Push(element);
            }
            return this;
        }
    }
}
