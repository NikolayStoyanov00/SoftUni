using System;
using System.Linq;

namespace _2._Sum_Matrix_Columns
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] matrixSize = Console.ReadLine()
                .Split(", ")
                .Select(int.Parse)
                .ToArray();

            int[,] matrix = new int[matrixSize[0], matrixSize[1]];

            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                int[] numbers = Console.ReadLine()
                    .Split(" ")
                    .Select(int.Parse)
                    .ToArray();

                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    matrix[row, col] = numbers[col];
                }
            }

            for (int col = 0; col < matrix.GetLength(1); col++)
            {
                var sumOfColumn = 0;

                for (int row = 0; row < matrix.GetLength(0); row++)
                {
                    sumOfColumn += matrix[row, col];
                }

                Console.WriteLine(sumOfColumn);
            }
        }
    }
}
