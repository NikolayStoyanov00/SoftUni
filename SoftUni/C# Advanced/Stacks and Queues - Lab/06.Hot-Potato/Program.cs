using System;
using System.Collections.Generic;

namespace _6.Hot_Potato
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] kids = Console.ReadLine()
                .Split(" ");

            Queue<string> kidsQueue = new Queue<string>(kids);

            int n = int.Parse(Console.ReadLine());

            while (kidsQueue.Count > 1)
            {
                for (int i = 1; i < n; i++)
                {
                    kidsQueue.Enqueue(kidsQueue.Dequeue());
                }

                Console.WriteLine($"Removed {kidsQueue.Dequeue()}");
            }

            Console.WriteLine($"Last is {kidsQueue.Dequeue()}");
        }
    }
}
