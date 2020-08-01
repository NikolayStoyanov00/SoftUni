using System;
using System.Collections.Generic;
using System.Linq;

namespace _05._Fashion_Boutique
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] clothes = Console.ReadLine()
                .Split(" ")
                .Select(int.Parse)
                .ToArray();

            Stack<int> stack = new Stack<int>(clothes);
            int rackCapacity = int.Parse(Console.ReadLine());
            int currentRack = 0;
            int racks = 1;

            while (stack.Count > 0)
            {
                if (rackCapacity > currentRack + stack.Peek())
                {
                    currentRack += stack.Pop();
                }
                else if (rackCapacity == currentRack + stack.Peek())
                {
                    stack.Pop();
                    if (stack.Count > 0)
                    {
                        racks++;
                        currentRack = 0;
                    }
                }
                else if (rackCapacity < currentRack + stack.Peek())
                {
                    racks++;
                    currentRack = 0;
                }
            }
            Console.WriteLine(racks);
        }
    }
}
