using System;

namespace HW2._Knights_of_Honor
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] names = Console.ReadLine()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries);

            Action<string[]> printsSirNames = PrintsNames;
            printsSirNames(names);
        }

        static void PrintsNames(string[] names)
        {
            foreach (var name in names)
            {
                Console.WriteLine($"Sir {name}");
            }
        }
    }
}
