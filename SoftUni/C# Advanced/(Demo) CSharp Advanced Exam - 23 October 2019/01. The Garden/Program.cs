using System;
using System.Collections.Generic;
using System.Linq;

namespace _01._The_Garden
{
    class Program
    {
        static void Main(string[] args)
        {
            int gardenRows = int.Parse(Console.ReadLine());

            char[][] garden = new char[gardenRows][];

            List<char> vegetablesKinds = new List<char>() { 'L', 'P', 'C' };

            List<char> harmedVegetables = new List<char>();
            List<char> harvestedVegetables = new List<char>();

            for (int row = 0; row < garden.Length; row++)
            {
                char[] vegetables = Console.ReadLine()
                    .Split(' ')
                    .Select(char.Parse)
                    .ToArray();

                garden[row] = new char[vegetables.Length];

                for (int col = 0; col < garden[row].Length; col++)
                {
                    garden[row][col] = vegetables[col];
                }
            }

            while (true)
            {
                string command = Console.ReadLine();

                if (command == "End of Harvest")
                {
                    break;  
                }

                string[] tokens = command
                    .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                    .ToArray();

                if (tokens[0] == "Harvest")
                {
                    int row = int.Parse(tokens[1]);
                    int col = int.Parse(tokens[2]);

                    if (IsInside(row, col, garden))
                    {
                        if (vegetablesKinds.Contains(garden[row][col]))
                        {
                            harvestedVegetables.Add(garden[row][col]);
                            garden[row][col] = ' ';
                        }
                    }
                }

                else if (tokens[0] == "Mole")
                {
                    int row = int.Parse(tokens[1]);
                    int col = int.Parse(tokens[2]);

                    string direction = tokens[3].ToLower();

                    if (IsInside(row, col, garden))
                    {
                        if (IsVegetable(row, col, garden, vegetablesKinds))
                        {
                            harmedVegetables.Add(garden[row][col]);
                            garden[row][col] = ' ';
                        }

                        if (direction == "up")
                        {
                            while (IsInside(row - 2, col, garden) && IsVegetable(row - 2, col, garden, vegetablesKinds))
                            {
                                harmedVegetables.Add(garden[row - 2][col]);
                                garden[row - 2][col] = ' ';
                                row -= 2;
                            }
                        }
                        else if (direction == "down")
                        {
                            while (IsInside(row + 2, col, garden) && IsVegetable(row + 2, col, garden, vegetablesKinds))
                            {
                                harmedVegetables.Add(garden[row + 2][col]);
                                garden[row + 2][col] = ' ';
                                row += 2;
                            }
                        }

                        else if (direction == "left")
                        {
                            while (IsInside(row, col - 2, garden) && IsVegetable(row, col - 2, garden, vegetablesKinds))
                            {
                                harmedVegetables.Add(garden[row][col]);
                                garden[row][col - 2] = ' ';
                                col -= 2;
                            }
                        }

                        else if (direction == "right")
                        {
                            while (IsInside(row, col + 2, garden) && IsVegetable(row, col + 2, garden, vegetablesKinds))
                            {
                                harmedVegetables.Add(garden[row][col]);
                                garden[row][col + 2] = ' ';
                                col += 2;
                            }
                        }
                    }
                }
            }

            for (int row = 0; row < garden.Length; row++)
            {
                for (int col = 0; col < garden[row].Length; col++)
                {
                    Console.Write($"{garden[row][col]} ");
                }
                Console.WriteLine();
            }

            var carrotsCount = harvestedVegetables.Where(x => x == 'C');
            Console.WriteLine($"Carrots: {carrotsCount.Count()}");

            var potatoesCount = harvestedVegetables.Where(x => x == 'P');
            Console.WriteLine($"Potatoes: {potatoesCount.Count()}");

            var lettucesCount = harvestedVegetables.Where(x => x == 'L');
            Console.WriteLine($"Lettuce: {lettucesCount.Count()}");

            Console.WriteLine($"Harmed vegetables: {harmedVegetables.Count()}");
        }

        private static bool IsVegetable(int row, int col, char[][] garden, List<char> vegetablesKinds)
        {
            if (vegetablesKinds.Contains(garden[row][col]))
            {
                return true;
            }
            return false;
        }

        private static bool IsInside(int row, int col, char[][] garden)
        {
            if (row >= 0 && row < garden.Length
                && col >= 0 && col < garden[row].Length)
            {
                return true;
            }
            return false;
        }
    }
}
