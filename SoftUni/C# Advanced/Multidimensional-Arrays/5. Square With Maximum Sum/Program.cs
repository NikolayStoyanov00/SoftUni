using System;
using System.Linq;

namespace _5._Square_With_Maximum_Sum
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] matrixSize = Console.ReadLine()
                .Split(", ")
                .Select(int.Parse)
                .ToArray();

            int maxSum = int.MinValue;

            int[,] matrix = new int[matrixSize[0], matrixSize[1]];

            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                int[] colNumbers = Console.ReadLine()
                    .Split(", ")
                    .Select(int.Parse)
                    .ToArray();

                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    matrix[row, col] = colNumbers[col];
                }
            }

            int biggestSquareRow = 0;
            int biggestSquareCol = 0;

            for (int row = 0; row < matrix.GetLength(0) - 1; row++)
            {
                int currentSum = 0;

                for (int col = 0; col < matrix.GetLength(1) - 1; col++)
                {
                    currentSum = matrix[row, col]
                        + matrix[row + 1, col]
                        + matrix[row, col + 1]
                        + matrix[row + 1, col + 1];

                    if (currentSum > maxSum)
                    {
                        maxSum = currentSum;
                        biggestSquareRow = row;
                        biggestSquareCol = col;
                    }
                }
            }

            Console.Write($"{matrix[biggestSquareRow, biggestSquareCol]} ");
            Console.WriteLine(matrix[biggestSquareRow, biggestSquareCol + 1]);
            Console.Write($"{matrix[biggestSquareRow + 1, biggestSquareCol]} ");
            Console.WriteLine(matrix[biggestSquareRow + 1, biggestSquareCol + 1]);

            Console.WriteLine(maxSum);
        }
    }
}
