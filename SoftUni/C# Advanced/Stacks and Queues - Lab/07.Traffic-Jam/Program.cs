using System;
using System.Collections.Generic;

namespace _7.Traffic_Jam
{
    class Program
    {
        static void Main(string[] args)
        {
            int carPassCount = int.Parse(Console.ReadLine());
            int carsPassedCount = 0;
            Queue<string> carsQueue = new Queue<string>();
            while (true)
            {
                string carName = Console.ReadLine();

                if (carName == "green")
                {
                    for (int i = 0; i < carPassCount; i++)
                    {
                        if (carsQueue.Count > 0)
                        {
                            Console.WriteLine($"{carsQueue.Dequeue()} passed!");
                            carsPassedCount++;
                        }
                    }
                }
                else if (carName == "end")
                {
                    break;
                }
                else
                {
                    carsQueue.Enqueue(carName);
                }
            }
            Console.WriteLine($"{carsPassedCount} cars passed the crossroads.");
        }
    }
}
