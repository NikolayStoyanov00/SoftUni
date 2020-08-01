using System;
using System.Linq;

namespace Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] dimensions = Console.ReadLine()
                .Split(", ")
                .Select(int.Parse)
                .ToArray();

            int rows = dimensions[0];
            int cols = dimensions[1];
            int[,] matrix = new int[rows, cols];
            int sum = 0;

            for (int row = 0; row < rows; row++)
            {
                var rowAsString = Console.ReadLine();
                var rowElements = rowAsString.Split(", ");

                for (int col = 0; col < cols; col++)
                {
                    matrix[row, col] = int.Parse(rowElements[col]);
                    sum += int.Parse(rowElements[col]);
                }
            }

            Console.WriteLine(matrix.GetLength(0));
            Console.WriteLine(matrix.GetLength(1));
            Console.WriteLine(sum);
        }
    }
}
