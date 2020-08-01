using System;
using System.Collections.Generic;
using System.Linq;

namespace HW04.Fast_Food
{
    class Program
    {
        static void Main(string[] args)
        {
            int foodQuantity = int.Parse(Console.ReadLine());
            int[] numbers = Console.ReadLine()
                .Split(" ")
                .Select(int.Parse)
                .ToArray();
            Queue<int> queue = new Queue<int>(numbers);
            int biggestOrder = 0;

            while (foodQuantity > 0 && queue.Count > 0)
            {
                int number = queue.Peek();

                if (number > biggestOrder)
                {
                    biggestOrder = number;
                }

                if (foodQuantity - number > 0)
                {
                    foodQuantity -= queue.Dequeue();
                }
                else
                {
                    foodQuantity -= number;
                }
            }
            Console.WriteLine(biggestOrder);
            if (foodQuantity >= 0)
            {
                Console.WriteLine("Orders complete");
            }
            else
            {
                Console.Write($"Orders left: ");
                Console.WriteLine(string.Join(" ", queue));
            }
        }
    }
}
