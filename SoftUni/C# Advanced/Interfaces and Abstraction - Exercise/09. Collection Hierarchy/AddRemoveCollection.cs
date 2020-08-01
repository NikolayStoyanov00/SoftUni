using System;
using System.Collections.Generic;
using System.Text;

namespace _09._Collection_Hierarchy
{
    public class AddRemoveCollection <T> : IAddRemoveCollection<T>
    {
        private List<T> collection = new List<T>();

        public int Add(T item)
        {
            collection.Insert(0, item);
            return 0;
        }

        public T Remove()
        {
            if (collection.Count > 0)
            {
                T element = collection[collection.Count - 1];
                collection.RemoveAt(collection.Count - 1);
                return element;
            }
            return default(T);
        }
    }
}
