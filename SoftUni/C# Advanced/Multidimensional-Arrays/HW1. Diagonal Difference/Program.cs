using System;
using System.Linq;

namespace HW1._Diagonal_Difference
{
    class Program
    {
        static void Main(string[] args)
        {
            int matrixSize = int.Parse(Console.ReadLine());

            int[,] matrix = new int[matrixSize, matrixSize];

            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                int[] colNumbers = Console.ReadLine()
                    .Split(" ")
                    .Select(int.Parse)
                    .ToArray();

                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    matrix[row, col] = colNumbers[col];
                }
            }

            int primaryDiagonalSum = 0;
            int secondaryDiagonalSum = 0;

            for (int col = 0; col < matrix.GetLength(1); col++)
            {
                for (int row = 0; row < matrix.GetLength(0); row++)
                {
                    col = row;
                    primaryDiagonalSum += matrix[row, col];
                }
            }

            for (int col = matrix.GetLength(1) - 1; col >= 0; col--)
            {
                for (int row = 0; row < matrix.GetLength(0); row++)
                {
                    secondaryDiagonalSum += matrix[row, col];
                    col--;
                }
            }

            int difference = Math.Abs(primaryDiagonalSum - secondaryDiagonalSum);

            Console.WriteLine(difference);
        }
    }
}
