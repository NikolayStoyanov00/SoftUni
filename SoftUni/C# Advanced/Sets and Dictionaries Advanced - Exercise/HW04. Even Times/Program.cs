using System;
using System.Collections.Generic;

namespace HW04._Even_Times
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<int, int> dictionary = new Dictionary<int, int>();

            int n = int.Parse(Console.ReadLine());

            for (int i = 0; i < n; i++)
            {
                int num = int.Parse(Console.ReadLine());

                if (!dictionary.ContainsKey(num))
                {
                    dictionary.Add(num, 0);
                }

                dictionary[num]++;
            }

            foreach (var number in dictionary)
            {
                if (number.Value % 2 == 0)
                {
                    Console.WriteLine(number.Key);
                }
            }
        }
    }
}
