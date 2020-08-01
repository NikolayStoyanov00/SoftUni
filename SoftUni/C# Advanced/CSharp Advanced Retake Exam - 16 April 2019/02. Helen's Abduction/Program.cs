using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Exercise
{
    class Program
    {
        public static void Main(string[] args)
        {
            int parisEnergy = int.Parse(Console.ReadLine());
            int sizeOfMatrix = int.Parse(Console.ReadLine());

            var matrix = new char[sizeOfMatrix][];

            int parisRow = int.MinValue;
            int parisCol = int.MinValue;

            for (int row = 0; row < matrix.Length; row++)
            {
                string currentRow = Console.ReadLine();
                int length = currentRow.Length;
                matrix[row] = new char[length];
                for (int col = 0; col < matrix[row].Length; col++)
                {
                    matrix[row][col] = currentRow[col];

                    if (currentRow[col] == 'P')
                    {
                        parisRow = row;
                        parisCol = col;
                    }
                }
            }

            matrix[parisRow][parisCol] = '-';

            while (true)
            {
                var enemySpawn = Console.ReadLine()
                    .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                    .ToArray();

                string parisDirection = enemySpawn[0];
                int enemyRow = int.Parse(enemySpawn[1]);
                int enemyCol = int.Parse(enemySpawn[2]);

                matrix[enemyRow][enemyCol] = 'S';

                if (parisDirection == "up")
                {
                    if (parisRow - 1 >= 0)
                    {
                        parisRow--;
                        parisEnergy--;

                        if (parisEnergy <= 0)
                        {
                            matrix[parisRow][parisCol] = 'X';
                            Console.WriteLine($"Paris died at {parisRow};{parisCol}.");
                            break;
                        }
                    }
                    else if (parisRow - 1 < 0)
                    {
                        parisEnergy--;
                        if (parisEnergy <= 0)
                        {
                            matrix[parisRow][parisCol] = 'X';
                            Console.WriteLine($"Paris died at {parisRow};{parisCol}.");
                            break;
                        }
                    }
                }
                else if (parisDirection == "down")
                {
                    if (parisRow + 1 < sizeOfMatrix)
                    {
                        parisRow++;
                        parisEnergy--;

                        if (parisEnergy <= 0)
                        {
                            matrix[parisRow][parisCol] = 'X';
                            Console.WriteLine($"Paris died at {parisRow};{parisCol}.");
                            break;
                        }
                    }
                    else if (parisRow + 1 >= sizeOfMatrix)
                    {
                        parisEnergy--;
                        if (parisEnergy <= 0)
                        {
                            matrix[parisRow][parisCol] = 'X';
                            Console.WriteLine($"Paris died at {parisRow};{parisCol}.");
                            break;
                        }
                    }
                }
                else if (parisDirection == "left")
                {
                    if (parisCol - 1 >= 0)
                    {
                        parisCol--;
                        parisEnergy--;

                        if (parisEnergy <= 0)
                        {
                            matrix[parisRow][parisCol] = 'X';
                            Console.WriteLine($"Paris died at {parisRow};{parisCol}.");
                            break;
                        }
                    }
                    else if (parisCol - 1 < 0)
                    {
                        parisEnergy--;
                        if (parisEnergy <= 0)
                        {
                            matrix[parisRow][parisCol] = 'X';
                            Console.WriteLine($"Paris died at {parisRow};{parisCol}.");
                            break;
                        }
                    }
                }
                else if (parisDirection == "right")
                {
                    if (parisCol + 1 < sizeOfMatrix)
                    {
                        parisCol++;
                        parisEnergy--;

                        if (parisEnergy <= 0)
                        {
                            matrix[parisRow][parisCol] = 'X';
                            Console.WriteLine($"Paris died at {parisRow};{parisCol}.");
                            break;
                        }
                    }
                    else if (parisCol + 1 >= sizeOfMatrix)
                    {
                        parisEnergy--;
                        if (parisEnergy <= 0)
                        {
                            matrix[parisRow][parisCol] = 'X';
                            Console.WriteLine($"Paris died at {parisRow};{parisCol}.");
                            break;
                        }
                    }
                }

                if (matrix[parisRow][parisCol] == 'H')
                {
                    //parisEnergy--;
                    matrix[parisRow][parisCol] = '-';
                    Console.WriteLine($"Paris has successfully abducted Helen! Energy left: {parisEnergy}");
                    break;
                }
                if (matrix[parisRow][parisCol] == 'S')
                {
                    parisEnergy -= 2;
                    if (parisEnergy > 0)
                    {
                        matrix[parisRow][parisCol] = '-';
                    }
                    else
                    {
                        matrix[parisRow][parisCol] = 'X';
                        Console.WriteLine($"Paris died at {parisRow};{parisCol}.");
                        break;
                    }
                }
            }

            PrintMatrix(matrix);
        }

        private static void PrintMatrix(char[][] matrix)
        {
            for (int row = 0; row < matrix.Length; row++)
            {
                for (int col = 0; col < matrix[row].Length; col++)
                {
                    Console.Write(matrix[row][col]);
                }

                Console.WriteLine();
            }
        }
    }
}