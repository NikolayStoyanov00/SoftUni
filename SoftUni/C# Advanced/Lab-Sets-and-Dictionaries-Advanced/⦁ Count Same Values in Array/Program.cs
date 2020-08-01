using System;
using System.Collections.Generic;
using System.Linq;

namespace __Count_Same_Values_in_Array
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<double, int> dictionary = new Dictionary<double, int>();

            double[] numbers = Console.ReadLine()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Select(double.Parse)
                .ToArray();

            for (int i = 0; i < numbers.Length; i++)
            {
                if (dictionary.ContainsKey(numbers[i]))
                {
                    dictionary[numbers[i]]++;
                }
                else
                {
                    dictionary.Add(numbers[i], 1);
                }
            }

            foreach (var number in dictionary)
            {
                Console.WriteLine($"{number.Key} - {number.Value} times");
            }
        }

    }
}
