using System;
using System.Linq;

namespace HW7._Knight_Game
{
    class Program
    {
        static void Main(string[] args)
        {
            int rows = int.Parse(Console.ReadLine());

            char[,] matrix = new char[rows, rows];
            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                char[] elements = Console.ReadLine().ToCharArray();

                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    matrix[row, col] = elements[col];
                }
            }

            int maxKnightsKilled = int.MinValue;
            int knightsKillers = 0;
            int killerRow = 0;
            int killerCol = 0;
            int currentKnightsKilled = int.MinValue;

            while (true)
            {
                for (int row = 0; row < matrix.GetLength(0); row++)
                {
                    for (int col = 0; col < matrix.GetLength(1); col++)
                    {
                        if (IsInside(matrix, row, col) && matrix[row, col] == 'K')
                        {
                            if (IsInside(matrix, row - 2, col - 1) && matrix[row - 2, col - 1] == 'K')
                            {
                                currentKnightsKilled++;
                            }
                            if (IsInside(matrix, row - 2, col + 1) && matrix[row - 2, col + 1] == 'K')
                            {
                                currentKnightsKilled++;
                            }
                            if (IsInside(matrix, row + 2, col - 1) && matrix[row + 2, col - 1] == 'K')
                            {
                                currentKnightsKilled++;
                            }
                            if (IsInside(matrix, row + 2, col + 1) && matrix[row + 2, col + 1] == 'K')
                            {
                                currentKnightsKilled++;
                            }

                            if (IsInside(matrix, row - 1, col - 2) && matrix[row - 1, col - 2] == 'K')
                            {
                                currentKnightsKilled++;
                            }
                            if (IsInside(matrix, row - 1, col + 2) && matrix[row - 1, col + 2] == 'K')
                            {
                                currentKnightsKilled++;
                            }
                            if (IsInside(matrix, row + 1, col - 2) && matrix[row + 1, col - 2] == 'K')
                            {
                                currentKnightsKilled++;
                            }
                            if (IsInside(matrix, row + 1, col + 2) && matrix[row + 1, col + 2] == 'K')
                            {
                                currentKnightsKilled++;
                            }
                        }

                        if (currentKnightsKilled > maxKnightsKilled)
                        {
                            killerRow = row;
                            killerCol = col;
                            maxKnightsKilled = currentKnightsKilled;
                        }
                        currentKnightsKilled = 0;
                    }
                }

                if (maxKnightsKilled > 0)
                {
                    matrix[killerRow, killerCol] = '0';
                    knightsKillers++;
                    maxKnightsKilled = 0;
                }
                else
                {
                    Console.WriteLine(knightsKillers);
                    return;
                }
            }
        }

        private static bool IsInside(char[,] matrix, int row, int col)
        {
            if (row >= 0 && row < matrix.GetLength(0) && col >= 0 && col < matrix.GetLength(1))
            {
                return true;
            }
            return false;
        }

    }
}
