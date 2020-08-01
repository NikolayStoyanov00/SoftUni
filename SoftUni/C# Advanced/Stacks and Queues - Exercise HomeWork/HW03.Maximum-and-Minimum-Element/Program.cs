using System;
using System.Collections.Generic;
using System.Linq;

namespace HW03.Maximum_and_Minimum_Element
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());

            Stack<int> stack = new Stack<int>();

            for (int i = 0; i < n; i++)
            {
                int[] numbers = Console.ReadLine()
                    .Split(" ")
                    .Select(int.Parse)
                    .ToArray();

                if (numbers[0] == 1)
                {
                    int number = numbers[1];

                    stack.Push(number);
                }
                else if (numbers[0] == 2)
                {
                    if (stack.Count >= 1)
                    {
                        stack.Pop();
                    }
                }
                else if (numbers[0] == 3)
                {
                    if (stack.Count >= 1)
                    {
                        Console.WriteLine(stack.Max());
                    }
                }
                else if (numbers[0] == 4)
                {
                    if (stack.Count >= 1)
                    {
                        Console.WriteLine(stack.Min());
                    }
                }
            }

            Console.WriteLine(string.Join(", ", stack.ToArray()));
        }
    }
}
