using System;
using System.Collections.Generic;
using System.Linq;

namespace _01._Summer_Cocktails
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] ingredients = Console.ReadLine()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            int[] freshness = Console.ReadLine()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            var coctails = new SortedDictionary<string, int>();
            Queue<int> queue = new Queue<int>(ingredients);
            Stack<int> stack = new Stack<int>(freshness);

            while (queue.Count > 0 && stack.Count > 0)
            {
                int ingredientValue = queue.Peek();

                if (ingredientValue <= 0)
                {
                    queue.Dequeue();
                    continue;
                }

                int lastFreshnessValue = stack.Peek();

                int totalFreshness = ingredientValue * lastFreshnessValue;

                if (totalFreshness == 150)
                {
                    queue.Dequeue();
                    stack.Pop();
                    if (!coctails.ContainsKey("Mimosa"))
                    {
                        coctails.Add("Mimosa", 0);
                    }
                    coctails["Mimosa"]++;
                }
                else if (totalFreshness == 250)
                {
                    queue.Dequeue();
                    stack.Pop();

                    if (!coctails.ContainsKey("Daiquiri"))
                    {
                        coctails.Add("Daiquiri", 0);
                    }
                    coctails["Daiquiri"]++;
                }
                else if (totalFreshness == 300)
                {
                    queue.Dequeue();
                    stack.Pop();

                    if (!coctails.ContainsKey("Sunshine"))
                    {
                        coctails.Add("Sunshine", 0);
                    }
                    coctails["Sunshine"]++;
                }
                else if (totalFreshness == 400)
                {
                    queue.Dequeue();
                    stack.Pop();

                    if (!coctails.ContainsKey("Mojito"))
                    {
                        coctails.Add("Mojito", 0);
                    }
                    coctails["Mojito"]++;
                }
                else
                {
                    stack.Pop();

                    ingredientValue += 5;
                    queue.Dequeue();
                    queue.Enqueue(ingredientValue);
                }
            }

            if (coctails.Count >= 4)
            {
                Console.WriteLine("It's party time! The cocktails are ready!");

                if (queue.Count > 0)
                {
                    Console.WriteLine($"Ingredients left: {queue.Sum()}");
                }

                foreach (var (coctailName, coctailCount) in coctails)
                {
                    Console.WriteLine($" # {coctailName} --> {coctailCount}");
                }
            }
            else
            {
                Console.WriteLine("What a pity! You didn't manage to prepare all cocktails.");

                if (queue.Count > 0)
                {
                    Console.WriteLine($"Ingredients left: {queue.Sum()}");
                }

                if (coctails.Count > 0)
                {
                    foreach (var (coctailName, coctailCount) in coctails)
                    {
                        Console.WriteLine($" # {coctailName} --> {coctailCount}");
                    }
                }
            }
        }
    }
}
