using System;
using System.Collections.Generic;

namespace _5.Supermarket
{
    class Program
    {
        static void Main(string[] args)
        {
            Queue<string> names = new Queue<string>();

            while (true)
            {
                string input = Console.ReadLine();

                if (input == "End")
                {
                    Console.WriteLine($"{names.Count} people remaining.");
                    break;
                }
                else if (input == "Paid")
                {
                    foreach (var item in names.ToArray())
                    {
                        Console.WriteLine(item);
                    }
                    names.Clear();
                }
                else
                {
                    names.Enqueue(input);
                }
            }
        }
    }
}
