using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace _01._Trojan_Invasion
{
    class Program
    {
        static void Main(string[] args)
        {
            int waves = int.Parse(Console.ReadLine());

            List<int> plates = Console.ReadLine()
                    .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToList();

            Stack<int> warriors = new Stack<int>();

            for (int i = 1; i <= waves; i++)
            {
                if (plates.Count == 0)
                {
                    break;
                }

                int[] warrior = Console.ReadLine()
                    .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray();

                if (i % 3 == 0)
                {
                    int plate = int.Parse(Console.ReadLine());
                    plates.Add(plate);
                }

                foreach (var war in warrior)
                {
                    warriors.Push(war);
                }

                while (plates.Count > 0 && warriors.Count > 0)
                {
                    int currentWarrior = warriors.Pop();
                    int currentPlates = plates[0];

                    if (currentPlates > currentWarrior)
                    {
                        currentPlates -= currentWarrior;
                        plates[0] = currentPlates;
                    }

                    else if (currentPlates < currentWarrior)
                    {
                        currentWarrior -= currentPlates;
                        warriors.Push(currentWarrior);
                        plates.RemoveAt(0);
                    }

                    else
                    {
                        plates.RemoveAt(0);
                    }
                }
                
            }

            if (warriors.Count > 0)
            {
                Console.WriteLine("The Trojans successfully destroyed the Spartan defense.");

                Console.WriteLine($"Warriors left: {string.Join(", ", warriors)}");
            }
            else
            {
                Console.WriteLine("The Spartans successfully repulsed the Trojan attack.");

                Console.WriteLine($"Plates left: {string.Join(", ", plates)}");
            }
        }

    }
}