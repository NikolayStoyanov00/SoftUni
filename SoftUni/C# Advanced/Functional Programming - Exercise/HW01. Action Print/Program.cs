using System;

namespace HW01._Action_Print
{
    class Program
    {
        static void Main(string[] args)
        {
            Action<string[]> print = items => Console.WriteLine(string.Join(Environment.NewLine, items));

            string[] names = Console.ReadLine()
                .Split();

            print(names);
        }
    }
}
