using System;
using System.Collections.Generic;
using System.Text;

namespace HW09._Simple_Text_Editor
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            StringBuilder builder = new StringBuilder();
            Stack<string> commands = new Stack<string>();
            commands.Push(builder.ToString());

            for (int i = 0; i < n; i++)
            {
                string[] tokens = Console.ReadLine()
                    .Split(" ");

                int command = int.Parse(tokens[0]);

                if (command == 1)
                {
                    builder.Append(tokens[1]);
                    commands.Push(builder.ToString());
                }
                else if (command == 2)
                {
                    int count = int.Parse(tokens[1]);
                    builder.Remove(builder.Length - count, count);
                    commands.Push(builder.ToString());
                }
                else if (command == 3)
                {
                    int index = int.Parse(tokens[1]);
                    Console.WriteLine(builder[index - 1]);
                }
                else if (command == 4)
                {
                    commands.Pop();
                    builder = new StringBuilder();
                    builder.Append(commands.Peek());
                }
            }
        }
    }
}
