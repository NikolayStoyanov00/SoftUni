using System;
using System.Collections.Generic;
using System.Text;

namespace Generic_Box_of_String
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
