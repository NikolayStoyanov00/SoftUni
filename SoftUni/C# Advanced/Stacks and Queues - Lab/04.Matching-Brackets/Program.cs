using System;
using System.Collections.Generic;
using System.Text;

namespace _4.Matching_Brackets
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = Console.ReadLine();
            Stack<int> stack = new Stack<int>();
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == '(')
                {
                    stack.Push(i);
                }
                if (input[i] == ')')
                {
                    int index = stack.Pop();
                    var expression = input.Substring(index, i - index + 1);
                    Console.WriteLine(expression);
                }
            }
        }
    }
}
