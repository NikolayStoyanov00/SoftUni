using System;
using System.Collections.Generic;
using System.Linq;

namespace _1._Dating_App
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] males = Console.ReadLine()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            int[] females = Console.ReadLine()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            int matched = 0;

            Queue<int> femaleQueue = new Queue<int>(females);
            Stack<int> maleStack = new Stack<int>(males);

            while (femaleQueue.Count > 0 && maleStack.Count > 0)
            {
                int female = femaleQueue.Peek();
                int male = maleStack.Peek();

                if (female <= 0)
                {
                    femaleQueue.Dequeue();
                    continue;
                }

                if (male <= 0)
                {
                    maleStack.Pop();
                    continue;
                }

                if (female % 25 == 0)
                {
                    femaleQueue.Dequeue();
                    femaleQueue.Dequeue();
                    continue;
                }

                if (male % 25 == 0)
                {
                    maleStack.Pop();
                    maleStack.Pop();
                    continue;
                }   

                if (female == male)
                {
                    maleStack.Pop();
                    femaleQueue.Dequeue();
                    matched++;
                }
                else
                {
                    femaleQueue.Dequeue();
                    maleStack.Pop();
                    maleStack.Push(male - 2);
                }
            }

            Console.WriteLine($"Matches: {matched}");

            if (maleStack.Count > 0)
            {
                Console.WriteLine($"Males left: {string.Join(", ", maleStack)}");
            }
            else
            {
                Console.WriteLine("Males left: none");
            }

            if (femaleQueue.Count > 0)
            {
                Console.WriteLine($"Females left: {string.Join(", ", femaleQueue)}");
            }
            else
            {
                Console.WriteLine("Females left: none");
            }
        }
    }
}
