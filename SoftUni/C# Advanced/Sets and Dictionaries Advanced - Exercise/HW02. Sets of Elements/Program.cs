using System;
using System.Collections.Generic;
using System.Linq;

namespace HW02._Sets_of_Elements
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] setParams = Console.ReadLine()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            int firstSetSize = setParams[0];
            int secondSetSize = setParams[1];

            HashSet<int> firstSet = new HashSet<int>();
            HashSet<int> secondSet = new HashSet<int>();

            for (int i = 0; i < firstSetSize; i++)
            {
                int num = int.Parse(Console.ReadLine());

                firstSet.Add(num);
            }

            for (int i = 0; i < secondSetSize; i++)
            {
                int num = int.Parse(Console.ReadLine());

                secondSet.Add(num);
            }

            foreach (var number in firstSet)
            {
                if (secondSet.Contains(number))
                {
                    Console.Write($"{number} ");
                }
            }
        }
    }
}
