using System;
using System.Collections.Generic;
using System.Text;

namespace _09._Collection_Hierarchy
{
    public class MyList <T> : IMyList<T>
    {
        private List<T> collection = new List<T>();

        public int Used => collection.Count;
        public int Add(T item)
        {
            collection.Insert(0, item);
            return 0;
        }

        public T Remove()
        {
            if (collection.Count > 0)
            {
                T element = collection[0];
                collection.RemoveAt(0);
                return element;
            }
            return default(T);
        }
    }
}
