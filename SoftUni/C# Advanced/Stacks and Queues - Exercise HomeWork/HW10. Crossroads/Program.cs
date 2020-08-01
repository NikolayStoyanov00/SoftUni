using System;
using System.Collections;
using System.Collections.Generic;

namespace HW10._Crossroads
{
    class Program
    {
        static void Main(string[] args)
        {
            int greenLightSecs = int.Parse(Console.ReadLine());
            int freeWindowInSecs = int.Parse(Console.ReadLine());
            int currentGreenLightSecs = greenLightSecs;
            int totalCarsPassed = 0;
            Queue<string> queue = new Queue<string>();

            while (true)
            {
                string command = Console.ReadLine();

                if (command == "END")
                {
                    break;  
                }

                if (command != "green")
                {
                    queue.Enqueue(command);
                }
                else 
                {
                    for (int i = 0; i < queue.Count; i++)
                    {
                        string currentCar = queue.Peek();

                        if (currentCar.Length <= currentGreenLightSecs)
                        {
                            queue.Dequeue();
                            i--;
                            totalCarsPassed++;
                            currentGreenLightSecs -= currentCar.Length;
                        }
                        
                        else 
                        {
                            if (currentCar.Length - currentGreenLightSecs <= freeWindowInSecs)
                            {
                                queue.Dequeue();
                                totalCarsPassed++;
                            }
                            else
                            {
                                Console.WriteLine($"A crash happened!");
                                Console.WriteLine($"{currentCar} was hit at {currentCar[freeWindowInSecs + currentGreenLightSecs]}.");
                                return;
                            }
                        }
                    }
                    currentGreenLightSecs = greenLightSecs;
                }
            }
            Console.WriteLine("Everyone is safe.");
            Console.WriteLine($"{totalCarsPassed} total cars passed the crossroads.");
        }
    }
}
