using System;
using System.Collections.Generic;
using System.Linq;

namespace HW06._Wardrobe
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());

            var wardrobe = new Dictionary<string, Dictionary<string, int>>();

            for (int i = 0; i < n; i++)
            {
                string[] line = Console.ReadLine()
                    .Split(" -> ", StringSplitOptions.RemoveEmptyEntries);

                string[] clothing = line[1]
                    .Split(",", StringSplitOptions.RemoveEmptyEntries);

                string clothingColor = line[0];

                if (!wardrobe.ContainsKey(clothingColor))
                {
                    wardrobe.Add(clothingColor, new Dictionary<string, int>());
                }

                for (int j = 0; j < clothing.Length; j++)
                {
                    if (!wardrobe[clothingColor].ContainsKey(clothing[j]))
                    {
                        wardrobe[clothingColor].Add(clothing[j], 0);
                    }
                    wardrobe[clothingColor][clothing[j]]++;
                }
            }

            string[] targetClothing = Console.ReadLine()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries);

            foreach (var (color, clothing) in wardrobe)
            {
                Console.WriteLine($"{color} clothes:");

                foreach (var (item, count) in clothing)
                {
                    if (targetClothing[0] == color && targetClothing[1] == item)
                    {
                        Console.WriteLine($"* {item} - {count} (found!)");
                    }
                    else
                    {
                        Console.WriteLine($"* {item} - {count}");
                    }
                }
            }
        }
    }
}
