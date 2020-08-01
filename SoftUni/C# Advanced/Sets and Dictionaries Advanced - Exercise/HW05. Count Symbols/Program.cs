using System;
using System.Collections.Generic;

namespace HW05._Count_Symbols
{
    class Program
    {
        static void Main(string[] args)
        {
            var dictionary = new SortedDictionary<char, int>();

            string input = Console.ReadLine();

            for (int i = 0; i < input.Length; i++)
            {
                if (!dictionary.ContainsKey(input[i]))
                {
                    dictionary.Add(input[i], 0);
                }
                dictionary[input[i]]++;
            }

            foreach (var (character, count) in dictionary)
            {
                Console.WriteLine($"{character}: {count} time/s");
            }
        }
    }
}
