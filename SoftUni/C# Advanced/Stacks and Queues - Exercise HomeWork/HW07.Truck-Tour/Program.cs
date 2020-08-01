using System;
using System.Collections.Generic;
using System.Linq;

namespace HW07.Truck_Tour
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            Queue<int[]> queue = new Queue<int[]>();

            for (int i = 0; i < n; i++)
            {
                queue.Enqueue(Console.ReadLine()
                    .Split(" ")
                    .Select(int.Parse)
                    .ToArray());
            }
            int tankFuel = 0;
            bool foundAnswer = true;
            int index = 0;

            for (int i = 0; i < n; i++)
            {
                for (int j = i; j < n; j++)
                {
                    int[] currPump = queue.Dequeue();

                    tankFuel += currPump[0];

                    if (tankFuel - currPump[1] < 0)
                    {
                        foundAnswer = false;
                        tankFuel = 0;
                    }
                    queue.Enqueue(currPump);
                }

                if (foundAnswer == true)
                {
                    Console.WriteLine(i);
                    break;
                }
                int[] startingPump = queue.Dequeue();
                queue.Enqueue(startingPump);
            }

            
        }
    }
}
