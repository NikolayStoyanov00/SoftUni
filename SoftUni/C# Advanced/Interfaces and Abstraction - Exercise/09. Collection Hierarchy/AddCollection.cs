using System;
using System.Collections.Generic;
using System.Text;

namespace _09._Collection_Hierarchy
{
    public class AddCollection <T> : IAddCollection<T>
    {
        private List<T> collection = new List<T>();
        public int Add(T item)
        {
            collection.Add(item);
            return collection.Count - 1;
        }
    }
}
