using System;
using System.Collections.Generic;
using System.Linq;

namespace _3._Primary_Diagonal
{
    class Program
    {
        static void Main(string[] args)
        {
            int squareSize = int.Parse(Console.ReadLine());

            int[,] matrix = new int[squareSize, squareSize];

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

            int sum = 0;

            for (int col = 0; col < matrix.GetLength(1); col++)
            {
                for (int row = 0; row < matrix.GetLength(0); row++)
                {
                    col = row;
                    sum += matrix[row, col];
                }
            }

            Console.WriteLine(sum);
        }
    }
}
