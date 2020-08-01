using System;
using System.Linq;

namespace HW06._Reverse_And_Exclude
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] numbers = Console.ReadLine()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            int divisibleBy = int.Parse(Console.ReadLine());

            var sortedNumbers = numbers
                .Where(x => x % divisibleBy != 0)
                .Reverse();

            Console.WriteLine(string.Join(" ", sortedNumbers));
        }
    }
}
