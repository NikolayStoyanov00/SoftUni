using System;
using System.Collections.Generic;
using System.Linq;

namespace HW02.Basic_Queue_Operations
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

            Queue<int> queue = new Queue<int>();

            for (int i = 0; i < n; i++)
            {
                queue.Enqueue(numbers[i]);
            }

            for (int i = 0; i < numberOfElementsToPop; i++)
            {
                queue.Dequeue();
            }

            if (queue.Contains(numberToLookFor))
            {
                Console.WriteLine("true");
            }
            else
            {
                if (queue.Count >= 1)
                {
                    Console.WriteLine(queue.Min());
                }
                else
                {
                    Console.WriteLine(0);
                }
            }
        }
    }
}
