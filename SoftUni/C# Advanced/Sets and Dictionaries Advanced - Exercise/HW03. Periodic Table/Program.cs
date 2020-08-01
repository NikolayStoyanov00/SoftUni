using System;
using System.Collections.Generic;

namespace HW03._Periodic_Table
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());

            SortedSet<string> set = new SortedSet<string>();

            for (int i = 0; i < n; i++)
            {
                string[] chemicalElements = Console.ReadLine()
                    .Split(" ", StringSplitOptions.RemoveEmptyEntries);

                for (int j = 0; j < chemicalElements.Length; j++)
                {
                    set.Add(chemicalElements[j]);
                }
            }

            foreach (var element in set)
            {
                Console.Write($"{element} ");
            }
        }
    }
}
