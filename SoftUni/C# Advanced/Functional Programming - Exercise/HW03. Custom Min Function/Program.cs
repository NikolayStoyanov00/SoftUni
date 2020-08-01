using System;
using System.Linq;

namespace HW03._Custom_Min_Function
{
    class Program
    {
        static void Main(string[] args)
        {
            Func<int[], int> minFunc = inputNumbers => inputNumbers.Min();

            int[] numbers = Console.ReadLine()
                .Split()
                .Select(int.Parse)
                .ToArray();

            int smallestNumber = minFunc(numbers);
            Console.WriteLine(smallestNumber);
        }
    }
}
