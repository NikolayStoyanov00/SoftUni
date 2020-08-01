using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace _01._Spaceship_Crafting
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] chemicalLiquids = Console.ReadLine()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            int[] physicalItems = Console.ReadLine()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            Dictionary<string, int> advancedMaterials = new Dictionary<string, int>();

            advancedMaterials.Add("Glass", 0);
            advancedMaterials.Add("Aluminium", 0);
            advancedMaterials.Add("Lithium", 0);
            advancedMaterials.Add("Carbon fiber", 0);

            Queue liquids = new Queue(chemicalLiquids);

            Stack items = new Stack(physicalItems);

            while (liquids.Count > 0 && items.Count > 0)
            {
                int liquid = (int)liquids.Dequeue();
                int item = (int)items.Pop();
                int sum = liquid + item;

                if (sum == 25)
                {
                    advancedMaterials["Glass"]++;
                }
                else if (sum == 50)
                {
                    advancedMaterials["Aluminium"]++;
                }

                else if (sum == 75)
                {
                    advancedMaterials["Lithium"]++;
                }

                else if (sum == 100)
                {
                    advancedMaterials["Carbon fiber"]++;
                }

                else
                {
                    item += 3;
                    items.Push(item);
                } 
            }

            bool succeeded = false;

            foreach (var advancedMaterial in advancedMaterials)
            {
                if (advancedMaterials["Carbon fiber"] > 0
                && advancedMaterials["Lithium"] > 0
                && advancedMaterials["Aluminium"] > 0
                && advancedMaterials["Glass"] > 0)
                {
                    succeeded = true;
                }
                else
                {
                    succeeded = false;
                }
            }

            if (succeeded == true)
            {
                Console.WriteLine($"Wohoo! You succeeded in building the spaceship!");
            }
            else
            {
                Console.WriteLine($"Ugh, what a pity! You didn't have enough materials to build the spaceship.");
            }

            if (liquids.Count == 0)
            {
                Console.WriteLine("Liquids left: none");
            }
            else
            {
                Console.Write($"Liquids left: ");

                while (liquids.Count > 0)
                {
                    if (liquids.Count > 1)
                    {
                        Console.Write($"{liquids.Dequeue()}, ");
                    }
                    else
                    {
                        Console.WriteLine($"{liquids.Dequeue()} ");
                    }
                }
            }

            if (items.Count == 0)
            {
                Console.WriteLine("Physical items left: none");
            }
            else
            {
                Console.Write($"Physical items left: ");
                while (items.Count > 0)
                {
                    if (items.Count > 1)
                    {
                        Console.Write($"{items.Pop()}, ");
                    }
                    else
                    {
                        Console.WriteLine($"{items.Pop()} ");
                    }
                }
            }

            foreach (var item in advancedMaterials.OrderBy(x => x.Key))
            {
                Console.WriteLine($"{item.Key}: {item.Value}");
            }
        }

    }
}
