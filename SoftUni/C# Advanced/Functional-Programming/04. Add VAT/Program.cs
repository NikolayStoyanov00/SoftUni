using System;
using System.Linq;

namespace _04._Add_VAT
{
    class Program
    {
        static void Main(string[] args)
        {
            double[] numbers = Console.ReadLine()
                .Split(", ", StringSplitOptions.RemoveEmptyEntries)
                .Select(double.Parse)
                .ToArray();

            Func<double, double> result = n => n += n * 0.20;

            foreach (var number in numbers)
            {
                Console.WriteLine($"{result(number):f2}");
            }
        }
    }
}
