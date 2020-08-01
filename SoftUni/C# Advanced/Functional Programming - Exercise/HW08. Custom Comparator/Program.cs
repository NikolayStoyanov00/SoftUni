using System;
using System.Collections.Generic;
using System.Linq;

namespace HW08._Custom_Comparator
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] numbers = Console.ReadLine()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            List<int> evenNumbers = new List<int>();
            List<int> oddNumbers = new List<int>();

            for (int i = 0; i < numbers.Length; i++)
            {
                if (IsEven(numbers[i]))
                {
                    evenNumbers.Add(numbers[i]);
                }
                else
                {
                    oddNumbers.Add(numbers[i]);
                }
            }

            evenNumbers.Sort();
            oddNumbers.Sort();

            PrintsCollections(evenNumbers, oddNumbers);
        }

        private static void PrintsCollections(List<int> evenNumbers, List<int> oddNumbers)
        {
            foreach (var evenNumber in evenNumbers)
            {
                Console.Write(evenNumber + " ");
            }

            foreach (var oddNumber in oddNumbers)
            {
                Console.Write(oddNumber + " ");
            }
        }

        private static bool IsEven(int number)
        {
            if (number % 2 == 0)
            {
                return true;
            }
            return false;
        }
    }
}
