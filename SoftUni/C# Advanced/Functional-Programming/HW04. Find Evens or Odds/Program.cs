using System;
using System.Collections.Generic;
using System.Linq;

namespace HW4._Find_Evens_or_Odds
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] numbersRange = Console.ReadLine()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            List<int> numbers = new List<int>();

            for (int i = numbersRange[0]; i <= numbersRange[1]; i++)
            {
                numbers.Add(i);
            }

            Predicate<int> predicate = x => x % 2 == 0;

            string evenOrOdd = Console.ReadLine();

            foreach (var number in numbers)
            {
                if (evenOrOdd == "even")
                {
                    if (predicate(number))
                    {
                        Console.Write(number + " ");
                    }
                }
                else if (evenOrOdd == "odd")
                {
                    if (!predicate(number))
                    {
                        Console.Write(number + " ");
                    }
                }
            }
            Console.WriteLine();
        }

    }
}
