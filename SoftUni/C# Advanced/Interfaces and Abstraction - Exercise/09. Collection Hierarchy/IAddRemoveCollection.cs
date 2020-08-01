using System;
using System.Collections.Generic;
using System.Text;

namespace _09._Collection_Hierarchy
{
    public interface IAddRemoveCollection<T> : IAddCollection<T>
    {
        public T Remove();
    }
}
