using System;
using System.Linq;

namespace HW8._Bombs
{
    class Program
    {
        static void Main(string[] args)
        {
            int size = int.Parse(Console.ReadLine());

            int[,] matrix = new int[size, size];

            InitializeMatrix(matrix);

            string[] bombs = Console.ReadLine()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < bombs.Length; i++)
            {
                int[] currentBomb = bombs[i]
                    .Split(",", StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray();

                int bombRow = currentBomb[0];
                int bombCol = currentBomb[1];

                if (IsInside(matrix, bombRow, bombCol) && matrix[bombRow, bombCol] > 0)
                {
                    int value = matrix[bombRow, bombCol];
                    matrix[bombRow, bombCol] = 0;

                    if (IsInside(matrix, bombRow, bombCol + 1) && matrix[bombRow, bombCol + 1] > 0)
                    {
                        matrix[bombRow, bombCol + 1] -= value;
                    }
                    if (IsInside(matrix, bombRow, bombCol - 1) && matrix[bombRow, bombCol - 1] > 0)
                    {
                        matrix[bombRow, bombCol - 1] -= value;
                    }
                    if (IsInside(matrix, bombRow - 1, bombCol) && matrix[bombRow - 1, bombCol] > 0)
                    {
                        matrix[bombRow - 1, bombCol] -= value;
                    }
                    if (IsInside(matrix, bombRow + 1, bombCol) && matrix[bombRow + 1, bombCol] > 0)
                    {
                        matrix[bombRow + 1, bombCol] -= value;
                    }
                    if (IsInside(matrix, bombRow - 1, bombCol + 1) && matrix[bombRow - 1, bombCol + 1] > 0)
                    {
                        matrix[bombRow - 1, bombCol + 1] -= value;
                    }
                    if (IsInside(matrix, bombRow - 1, bombCol - 1) && matrix[bombRow - 1, bombCol - 1] > 0)
                    {
                        matrix[bombRow - 1, bombCol - 1] -= value;
                    }
                    if (IsInside(matrix, bombRow + 1, bombCol + 1) && matrix[bombRow + 1, bombCol + 1] > 0)
                    {
                        matrix[bombRow + 1, bombCol + 1] -= value;
                    }
                    if (IsInside(matrix, bombRow + 1, bombCol - 1) && matrix[bombRow + 1, bombCol - 1] > 0)
                    {
                        matrix[bombRow + 1, bombCol - 1] -= value;
                    }
                }
            }
            int aliveCells = 0;
            int sum = 0;

            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    if (matrix[row, col] > 0)
                    {
                        aliveCells++;
                        sum += matrix[row, col];
                    }
                }
            }
            Console.WriteLine($"Alive cells: {aliveCells}");
            Console.WriteLine($"Sum: {sum}");

            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    Console.Write(matrix[row, col] + " ");
                }
                Console.WriteLine();
            }
        }

        private static bool IsInside(int[,] matrix, int bombRow, int bombCol)
        {
            if (bombRow >= 0 && bombRow < matrix.GetLength(0)
                && bombCol >= 0 && bombCol < matrix.GetLength(1))
            {
                return true;
            }
            return false;   
        }

        private static void InitializeMatrix(int[,] matrix)
        {
            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                int[] elements = Console.ReadLine()
                    .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray();

                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    matrix[row, col] = elements[col];
                }
            }
        }
    }
}
