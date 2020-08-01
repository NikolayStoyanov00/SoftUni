using System;
using System.Collections.Generic;

namespace ReverseStrings
{
    class StackDemo
    {
        static void Main(string[] args)
        {
            string input = Console.ReadLine();
            Stack<char> characters = new Stack<char>();

            foreach (var ch in input)
            {
                characters.Push(ch);
            }

            while (characters.Count > 0)
            {
                Console.Write(characters.Pop());
            }

            Console.WriteLine();
        }
    }
}
