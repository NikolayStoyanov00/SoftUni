using System;
using System.Collections.Generic;
using System.Text;

namespace _09._Collection_Hierarchy
{
    public interface IMyList<T> : IAddRemoveCollection<T>
    {
        public int Used { get;}
    }
}
