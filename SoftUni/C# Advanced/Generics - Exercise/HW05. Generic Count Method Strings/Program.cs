using System;
using System.Collections.Generic;

namespace Generic_Count_Method_Strings
{
    public class Program
    {
        public static void Main(string[] args)
        {
            int lines = int.Parse(Console.ReadLine());
            List<string> strings = new List<string>();

            for (int i = 0; i < lines; i++)
            {
                string line = Console.ReadLine();
                strings.Add(line);
            }

            string element = Console.ReadLine();
            int count = ComparesAlements(strings, element);

            Console.WriteLine(count);
        }

        public static int ComparesAlements<T>(List<T> list, T element)
        {
            int count = 0;
            foreach (T item in list)
            {
                if (String.Compare(item.ToString(), element.ToString()) == 1)
                {
                    count++;
                }
            }
            return count;
        }
    }
}
