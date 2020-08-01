using System;
using System.Collections.Generic;
using System.Linq;

namespace _2._Stack_Sum
{
    class Program
    {
        static void Main(string[] args)
        {
            var numbers = Console.ReadLine()
                .Split(" ")
                .Select(x => int.Parse(x));

            var stack = new Stack<int>(numbers);

            while (true)
            {
                var command = Console.ReadLine().ToLower();

                if (command.StartsWith("add "))
                {
                    var parts = command.Split(" ");
                    int firstNumber = int.Parse(parts[1]);
                    int secondNumber = int.Parse(parts[2]);

                    stack.Push(firstNumber);
                    stack.Push(secondNumber);
                }
                else if (command.StartsWith("remove "))
                {
                    var parts = command.Split(" ");
                    int itemsToRemove = int.Parse(parts[1]);

                    if (itemsToRemove > stack.Count)
                    {
                        itemsToRemove = stack.Count;
                    }

                    for (int i = 0; i < itemsToRemove; i++)
                    {
                        stack.Pop();
                    } 
                }
                else if (command == "end")
                {
                    break;
                }
            }

            var result = stack.Sum();
            Console.WriteLine($"Sum: {result}");
        }
    }
}
