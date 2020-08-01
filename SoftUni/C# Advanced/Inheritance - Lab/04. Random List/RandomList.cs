using System;
using System.Collections.Generic;
using System.Text;

namespace CustomRandomList
{
    public class RandomList : List<string>
    {
        private Random random;
        public RandomList()
        {
            Random random = new Random();
        }

        public string RemoveRandomElement()
        {
            int index = random.Next(0, this.Count);
            string element = this[index];

            this.RemoveAt(index);
            return element;
        }

        public string RandomString()
        {
            int index = random.Next(0, this.Count);
            return this[index];
        }
    }
}
