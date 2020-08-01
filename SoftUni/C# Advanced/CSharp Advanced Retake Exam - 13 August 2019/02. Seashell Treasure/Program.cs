using System;
using System.Collections.Generic;
using System.Linq;

namespace _02._Seashell_Treasure
{
    class Program
    {
        static void Main(string[] args)
        {
            int rows = int.Parse(Console.ReadLine());

            char[][] jaggedArray = new char[rows][];
            List<char> seashells = new List<char>();
            int stolenSeashells = 0;

            for (int row = 0; row < jaggedArray.Length; row++)
            {
                char[] rowElements = Console.ReadLine()
                    .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                    .Select(char.Parse)
                    .ToArray();

                jaggedArray[row] = new char[rowElements.Length];
                for (int col = 0; col < rowElements.Length; col++)
                {
                    jaggedArray[row][col] = rowElements[col];
                }
            }

            while (true)
            {
                string command = Console.ReadLine();

                if (command == "Sunset")
                {
                    break;  
                }

                string[] line = command.Split(" ", StringSplitOptions.RemoveEmptyEntries);

                if (line[0] == "Collect")
                {
                    int row = int.Parse(line[1]);
                    int col = int.Parse(line[2]);

                    if (IsInside(row, col, jaggedArray))
                    {
                        if (jaggedArray[row][col] != '-')
                        {
                            seashells.Add(jaggedArray[row][col]);
                            jaggedArray[row][col] = '-';
                        }
                    }
                }

                else if (line[0] == "Steal")
                {
                    int row = int.Parse(line[1]);
                    int col = int.Parse(line[2]);
                    string direction = line[3];

                    if (IsInside(row, col, jaggedArray))
                    {
                        if (jaggedArray[row][col] != '-')
                        {
                            jaggedArray[row][col] = '-';
                            stolenSeashells++;

                            if (direction == "up")
                            {
                                for (int i = 1; i <= 3; i++)
                                {
                                    if (IsInside(row - i, col, jaggedArray))
                                    {
                                        if (jaggedArray[row - i][col] != '-')
                                        {
                                            jaggedArray[row - i][col] = '-';
                                            stolenSeashells++;
                                        }
                                    }
                                }
                            }

                            else if (direction == "down")
                            {
                                for (int i = 1; i <= 3; i++)
                                {
                                    if (IsInside(row + i, col, jaggedArray))
                                    {
                                        if (jaggedArray[row + i][col] != '-')
                                        {
                                            jaggedArray[row + i][col] = '-';
                                            stolenSeashells++;
                                        }
                                    }
                                }
                            }

                            else if (direction == "left")
                            {
                                for (int i = 1; i <= 3; i++)
                                {
                                    if (IsInside(row, col - i, jaggedArray))
                                    {
                                        if (jaggedArray[row][col - i] != '-')
                                        {
                                            jaggedArray[row][col - i] = '-';
                                            stolenSeashells++;
                                        }
                                    }
                                }
                            }

                            else if (direction == "right")
                            {
                                for (int i = 1; i <= 3; i++)
                                {
                                    if (IsInside(row, col + i, jaggedArray))
                                    {
                                        if (jaggedArray[row][col + i] != '-')
                                        {
                                            jaggedArray[row][col + i] = '-';
                                            stolenSeashells++;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            foreach (var array in jaggedArray)
            {
                Console.Write(string.Join(" ", array));
                Console.WriteLine(' ');
            }

            Console.Write($"Collected seashells: {seashells.Count}");

            if (seashells.Count > 0)
            {
                Console.WriteLine($" -> {string.Join(", ", seashells)}");
            }
            Console.WriteLine();

            Console.WriteLine($"Stolen seashells: {stolenSeashells}");
        }

        private static bool IsInside(int row, int col, char[][] jaggedArray)
        {
            if (row >= 0 && row < jaggedArray.Length 
                && col >= 0 && col < jaggedArray[row].Length)
            {
                return true;
            }
            return false;
        }
    }
}
