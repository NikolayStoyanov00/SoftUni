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
                int[] colElements = Console.ReadLine()
                    .Split(" ")
                    .Select(int.Parse)
                    .ToArray();

                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    matrix[row, col] = colElements[col];
                }
            }

            int firstDiagonal = 0;
            int secondDiagonal = 0;

            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    firstDiagonal += matrix[row, col];
                    row++;
                }
            }

            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                for (int col = matrix.GetLength(1) - 1; col >= 0; col--)
                {
                    secondDiagonal += matrix[row, col];
                    row++;
                }
            }

            Console.WriteLine(Math.Abs(firstDiagonal - secondDiagonal));
        }
    }
}
