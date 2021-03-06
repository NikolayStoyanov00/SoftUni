﻿using System;
using System.Linq;

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
                string colElements = Console.ReadLine();

                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    matrix[row, col] = colElements[col];
                }   
            }

            char symbol = char.Parse(Console.ReadLine());
            bool found = false;
            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    if (matrix[row,col] == symbol)
                    {
                        Console.WriteLine($"({row}, {col})");
                        found = true;
                        return;
                    }
                }
            }

            if (found == false)
            {
                Console.WriteLine($"{symbol} does not occur in the matrix");
            }
        }
    }
}
