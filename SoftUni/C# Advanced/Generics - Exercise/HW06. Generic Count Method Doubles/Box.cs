using System;
using System.Collections.Generic;
using System.Text;

namespace __Generic_Count_Method_Doubles
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
