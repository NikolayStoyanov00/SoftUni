using System;
using System.Linq;

namespace _02._Sum_Numbers
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] numbers = Console.ReadLine()
                .Split(", ", StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            int sum = numbers.Sum();
            int length = numbers.Length;
            Console.WriteLine(length);
            Console.WriteLine(sum);
        }
    }
}
