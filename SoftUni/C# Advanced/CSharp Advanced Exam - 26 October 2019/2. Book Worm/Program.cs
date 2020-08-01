using System;
using System.Collections.Generic;
using System.Linq;

namespace _2._Book_Worm
{
    class Program
    {
        static void Main(string[] args)
        {
            List<char> input = Console.ReadLine()
                .ToCharArray()
                .ToList();

            int fieldSize = int.Parse(Console.ReadLine());

            char[,] matrix = new char[fieldSize, fieldSize];

            int playerRow = 0;
            int playerCol = 0;

            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                char[] rowElements = Console.ReadLine()
                    .ToCharArray();

                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    matrix[row, col] = rowElements[col];

                    if (matrix[row, col] == 'P')
                    {
                        playerRow = row;
                        playerCol = col;
                    }
                }
            }

            while (true)
            {
                string command = Console.ReadLine();

                if (command == "end")
                {
                    break;
                }

                if (command == "up")
                {
                    if (IsInside(playerRow - 1, playerCol, matrix))
                    {
                        matrix[playerRow, playerCol] = '-';
                        playerRow--;

                        if (char.IsLetter(matrix[playerRow, playerCol]))
                        {
                            input.Add(matrix[playerRow, playerCol]);
                            matrix[playerRow, playerCol] = 'P';
                        }
                    }
                    else
                    {
                        if (input.Count > 0)
                        {
                            input.RemoveAt(input.Count - 1);
                        }
                    }
                }
                else if (command == "down")
                {
                    if (IsInside(playerRow + 1, playerCol, matrix))
                    {
                        matrix[playerRow, playerCol] = '-';
                        playerRow++;
                        if (char.IsLetter(matrix[playerRow, playerCol]))
                        {
                            input.Add(matrix[playerRow, playerCol]);
                            matrix[playerRow, playerCol] = 'P';

                        }
                    }
                    else
                    {
                        if (input.Count > 0)
                        {
                            input.RemoveAt(input.Count - 1);
                        }
                    }
                }
                else if (command == "left")
                {
                    if (IsInside(playerRow, playerCol - 1, matrix))
                    {
                        matrix[playerRow, playerCol] = '-';
                        playerCol--;

                        if (char.IsLetter(matrix[playerRow, playerCol]))
                        {
                            input.Add(matrix[playerRow, playerCol]);
                            matrix[playerRow, playerCol] = 'P';

                        }
                    }
                    else
                    {
                        if (input.Count > 0)
                        {
                            input.RemoveAt(input.Count - 1);
                        }
                    }
                }
                else if (command == "right")
                {
                    if (IsInside(playerRow, playerCol + 1, matrix))
                    {
                        matrix[playerRow, playerCol] = '-';
                        playerCol++;

                        if (char.IsLetter(matrix[playerRow, playerCol]))
                        {
                            input.Add(matrix[playerRow, playerCol]);
                            matrix[playerRow, playerCol] = 'P';

                        }
                    }
                    else
                    {
                        if (input.Count > 0)
                        {
                            input.RemoveAt(input.Count - 1);
                        }
                    }
                }
            }

            foreach (var character in input)
            {
                Console.Write(character);
            }
            Console.WriteLine();

            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    Console.Write(matrix[row, col]);
                }
                Console.WriteLine();
            }
        }

        private static bool IsInside(int row, int col, char[,] matrix)
        {
            if (row >= 0 && row < matrix.GetLength(0) 
                && col >= 0 && col < matrix.GetLength(1))
            {
                return true;
            }
            return false;
        }
    }
}
