using System;
using System.Linq;

namespace HW3._Maximal_Sum
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] matrixSize = Console.ReadLine()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            int[,] matrix = new int[matrixSize[0], matrixSize[1]];

            int maxSum = 0;
            int biggestRow = 0;
            int biggestCol = 0;

            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                int[] colNumbers = Console.ReadLine()
                    .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray();

                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    matrix[row, col] = colNumbers[col];
                }
            }

            for (int row = 0; row < matrix.GetLength(0) - 2; row++)
            {
                int currentSum = 0;

                for (int col = 0; col < matrix.GetLength(1) - 2; col++)
                {
                    currentSum = matrix[row, col] + matrix[row, col + 1] + matrix[row, col + 2]
                        + matrix[row + 1, col] + matrix[row + 1, col + 1] + matrix[row + 1, col + 2]
                        + matrix[row + 2, col] + matrix[row + 2, col + 1] + matrix[row + 2, col + 2];

                    if (currentSum > maxSum)
                    {
                        maxSum = currentSum;
                        biggestRow = row;
                        biggestCol = col;
                    }
                }
            }

            Console.WriteLine($"Sum = {maxSum}");

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Console.Write($"{matrix[biggestRow + i, biggestCol + j]} ");
                }
                Console.WriteLine();
            }
        }
    }
}
