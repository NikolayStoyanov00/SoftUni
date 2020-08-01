using System;

namespace _04
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] input = Console.ReadLine()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries);

            string flourType = input[1];
            string bakingTech = input[2];
            double weight = double.Parse(input[3]);

            Dough dough = new Dough(flourType, bakingTech, weight);

            Console.WriteLine($"{dough.GetTotalCalories():f2}");
        }
    }
}
