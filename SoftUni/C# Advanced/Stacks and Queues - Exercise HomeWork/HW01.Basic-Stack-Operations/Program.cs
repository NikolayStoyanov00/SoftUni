using System;
using System.Collections.Generic;
using System.Linq;

namespace HW01.Basic_Stack_Operations
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] numbersCommands = Console.ReadLine()
                .Split(" ")
                .Select(int.Parse)
                .ToArray();

            int n = numbersCommands[0];
            int numberOfElementsToPop = numbersCommands[1];
            int numberToLookFor = numbersCommands[2];

            int[] numbers = Console.ReadLine()
                .Split(" ")
                .Select(int.Parse)
                .ToArray();

            Stack<int> stack = new Stack<int>();

            for (int i = 0; i < n; i++)
            {
                stack.Push(numbers[i]);
            }

            for (int i = 0; i < numberOfElementsToPop; i++)
            {
                stack.Pop();
            }

            if (stack.Contains(numberToLookFor))
            {
                Console.WriteLine("true");
            }
            else
            {
                if (stack.Count >= 1)
                {
                    Console.WriteLine(stack.Min());
                }
                else
                {
                    Console.WriteLine(0);
                }
            }
        }
    }
}
