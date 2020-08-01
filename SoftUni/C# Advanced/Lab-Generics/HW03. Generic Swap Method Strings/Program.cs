using System;
using System.Collections.Generic;

namespace Generic_Swap_Method_Strings
{
    public class Program
    {
        public static void Main(string[] args)
        {
            int lines = int.Parse(Console.ReadLine());
            List<string> list = new List<string>();
            for (int i = 0; i < lines; i++)
            {
                string line = Console.ReadLine();
                list.Add(line);
            }

            string[] indexes = Console.ReadLine()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries);
            SwapsElements(list, indexes);
            foreach (var item in list)
            {
                Box<string> box = new Box<string>();
                box.Value = item;
                Console.WriteLine(box.ToString());
            }
        }

        public static void SwapsElements<T>(List<T> list, string[] indexes)
        {
            var firstElement = list[int.Parse(indexes[0])];
            var secondElement = list[int.Parse(indexes[1])];

            list[int.Parse(indexes[0])] = secondElement;
            list[int.Parse(indexes[1])] = firstElement;
        }
    }
}
