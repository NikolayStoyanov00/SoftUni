using System;
using System.Collections.Generic;
using System.Linq;

namespace MakeASalad
{
    class Program
    {
        static void Main()
        {
            Dictionary<string, int> vegetableCalories = new Dictionary<string, int>
            {
                { "tomato", 80},
                { "carrot", 136},
                { "lettuce", 109},
                { "potato", 215}
            };

            Queue<string> vegetables = new Queue<string>(Console.ReadLine().Split().Where(v => vegetableCalories.ContainsKey(v)));
            Stack<int> calories = new Stack<int>(Console.ReadLine().Split().Select(int.Parse));

            while (vegetables.Count > 0 && calories.Count > 0)
            {
                int currentSalad = calories.Peek();

                while (currentSalad > 0 && vegetables.Count > 0)
                {
                    currentSalad -= vegetableCalories[vegetables.Dequeue()];
                }

                Console.Write(calories.Pop() + " ");
            }

            Console.WriteLine();

            if (vegetables.Count > 0)
            {
                Console.WriteLine(string.Join(" ", vegetables));
            }

            if (calories.Count > 0)
            {
                Console.WriteLine(string.Join(" ", calories));
            }
        }
    }
}