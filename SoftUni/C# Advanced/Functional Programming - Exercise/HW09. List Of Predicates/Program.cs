using System;
using System.Collections.Generic;
using System.Linq;

namespace HW09._List_Of_Predicates
{
    class Program
    {
        static void Main(string[] args)
        {
            int endOfRange = int.Parse(Console.ReadLine());

            int[] dividers = Console.ReadLine()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .Distinct()
                .ToArray();

            bool dividableNumber = false;

            for (int i = 1; i <= endOfRange; i++)
            {
                foreach (var divider in dividers)
                {
                    if (DividesByDivider(i, divider))
                    {
                        dividableNumber = true;
                    }
                    else
                    {
                        dividableNumber = false;
                        break;
                    }
                }

                if (dividableNumber == true)
                {
                    Console.Write(i + " ");
                    dividableNumber = false;
                }
            }
        }

        private static bool DividesByDivider(int i, int divider)
        {
            return i % divider == 0;
        }
    }
}
