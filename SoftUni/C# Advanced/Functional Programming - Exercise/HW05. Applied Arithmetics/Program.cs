using System;
using System.Collections.Generic;
using System.Linq;

namespace HW05._Applied_Arithmetics
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> numbers = Console.ReadLine()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToList();

            while (true)
            {
                string command = Console.ReadLine();

                if (command == "end")
                {
                    break;  
                }

                if (command == "add")
                {
                    Adds1ToTheNumbers(numbers);
                }
                else if (command == "multiply")
                {
                    MultipliesNumbersWith2(numbers);
                }
                else if (command == "subtract")
                {
                    SubtractsNumbersWith1(numbers);
                }
                else if (command == "print")
                {
                    PrintsTheCollection(numbers);
                }
            }
        }

        private static void PrintsTheCollection(List<int> numbers)
        {
            Console.WriteLine(string.Join(" ", numbers));
        }

        private static void SubtractsNumbersWith1(List<int> numbers)
        {
            for (int i = 0; i < numbers.Count; i++)
            {
                numbers[i]--;
            }
        }

        private static void MultipliesNumbersWith2(List<int> numbers)
        {
            for (int i = 0; i < numbers.Count; i++)
            {
                numbers[i] *= 2;
            }
        }

        private static void Adds1ToTheNumbers(List<int> numbers)
        {
            for (int i = 0; i < numbers.Count; i++)
            {
                numbers[i]++;
            }
        }
    }
}
