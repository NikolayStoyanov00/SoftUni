using System;
using System.Collections.Generic;

namespace Generic_Swap_Method_Integers
{
    public class Program
    {
        public static void Main(string[] args)
        {
            int numbers = int.Parse(Console.ReadLine());
            List<int> list = new List<int>();
            for (int i = 0; i < numbers; i++)
            {
                int number = int.Parse(Console.ReadLine());
                list.Add(number);
            }

            string[] indexes = Console.ReadLine()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries);
            SwapsElements(list, indexes);
            foreach (var number in list)
            {
                Box<int> box = new Box<int>();
                box.Value = number;
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
