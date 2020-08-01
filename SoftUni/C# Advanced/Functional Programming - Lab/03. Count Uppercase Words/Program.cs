using System;
using System.Collections.Generic;
using System.Linq;

namespace _03._Count_Uppercase_Words
{
    class Program
    {
        static void Main(string[] args)
        {
            Func<string, bool> checker = n => n[0] == n.ToUpper()[0];

            string[] words = Console.ReadLine()
               .Split(" ", StringSplitOptions.RemoveEmptyEntries)
               .Where(checker)
               .ToArray();

            foreach (var word in words)
            {
                Console.WriteLine(word);
            }
        }

    }
}
