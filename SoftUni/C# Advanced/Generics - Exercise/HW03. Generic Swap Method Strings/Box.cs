using System;
using System.Collections.Generic;
using System.Text;

namespace Generic_Swap_Method_Strings
{
    public class Box<T>
    {
        public T Value { get; set; }

        public override string ToString()
        {
            return $"{Value.GetType()}: {Value}";
        }

    }
}
