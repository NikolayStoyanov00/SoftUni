using System;
using System.Collections.Generic;
using System.Linq;

namespace HW12.Cups_and_Bottles
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] cupCapacity = Console.ReadLine()
                .Split(" ")
                .Select(int.Parse)
                .ToArray();

            int[] bottleWater = Console.ReadLine()
                .Split(" ")
                .Select(int.Parse)
                .ToArray();

            Stack<int> stack = new Stack<int>(bottleWater);
            Queue<int> queue = new Queue<int>(cupCapacity);
            int wastedLitersWater = 0;

            while (stack.Count > 0 && queue.Count > 0)
            {
                int water = stack.Pop();
                int cup = queue.Peek();

                if (water - cup >= 0)
                {
                    queue.Dequeue();
                    if (water - cup > 0)
                    {
                        wastedLitersWater += water - cup;
                    }
                }
                else
                {
                    cup -= water;
                    queue.Dequeue();
                    queue.Enqueue(cup);

                    for (int i = 0; i < queue.Count - 1; i++)
                    {
                        int item = queue.Dequeue();
                        queue.Enqueue(item);
                    }
                }
            }

            if (queue.Count == 0)
            {
                Console.Write("Bottles: ");
                Console.WriteLine(string.Join(" ", stack));
                Console.WriteLine($"Wasted litters of water: {wastedLitersWater}");
            }
            else
            {
                Console.Write("Cups: ");
                Console.WriteLine(string.Join(" ", queue));
                Console.WriteLine($"Wasted litters of water: {wastedLitersWater}");
            }
        }
    }
}
