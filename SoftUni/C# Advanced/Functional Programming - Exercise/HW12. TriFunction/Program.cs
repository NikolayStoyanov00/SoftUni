using System;
using System.Collections.Generic;
using System.Linq;

namespace HW12._TriFunction
{
    class Program
    {
        static void Main(string[] args)
        {
            int totalSum = int.Parse(Console.ReadLine());

            string[] inputNames = Console.ReadLine()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries);

            Func<string, int, bool> isEqualOrLargerFunc = (name, currentSum) => name.Sum(x => x) >= currentSum;

            Func<string[], Func<string, int, bool>, int, string> myFunc = (names, viableNameFunc, targetSum) => names.FirstOrDefault(x => viableNameFunc(x, targetSum));

            string targetName = myFunc(inputNames, isEqualOrLargerFunc, totalSum);

            Console.WriteLine(targetName);
        }
    }
}
