using System;

namespace _4._Symbol_in_Matrix
{
    class Program
    {
        static void Main(string[] args)
        {
            int matrixSize = int.Parse(Console.ReadLine());

            char[,] matrix = new char[matrixSize, matrixSize];

            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                string characters = Console.ReadLine();

                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    matrix[row, col] = characters[col];
                }
            }

            char symbol = char.Parse(Console.ReadLine());
            int firstRow = 0;
            int firstCol = 0;
            bool isFound = false;

            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    if (matrix[row, col] == symbol)
                    {
                        firstRow = row;
                        firstCol = col;
                        isFound = true;
                        Console.WriteLine($"({firstRow}, {firstCol})");
                        return;  
                    }
                }
            }
            if (isFound == false)
            {
                Console.WriteLine($"{symbol} does not occur in the matrix");
            }
        }
    }
}
