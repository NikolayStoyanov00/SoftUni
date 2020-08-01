using System;
using System.Collections.Generic;
using System.Text;

namespace BoxOfT
{
    public class Box<T>
    {
        public int Count { get; set;}
        public List<T> Data { get; set; }
        public void Add(T element)
        {
            if (element != null)
            {
                Data = new List<T>();
                Data.Add(element);
                Count++;
            }
        }

        public T Remove()
        {
            int elementIndex = Data.Count - 1;
            var lastElement = Data[Data.Count - 1];
            Data.RemoveAt(elementIndex);
            Count--;
            return lastElement;
        }
    }
}
