using System;
using System.Linq;

namespace HW4._Matrix_Shuffling
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] sizeDimensions = Console.ReadLine()
                .Split(" ")
                .Select(int.Parse)
                .ToArray();

            string[,] matrix = new string[sizeDimensions[0], sizeDimensions[1]];

            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                string[] colNumbers = Console.ReadLine()
                    .Split(" ");

                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    matrix[row, col] = colNumbers[col];
                }
            }

            while (true)
            {
                string command = Console.ReadLine();

                if (command == "END")
                {
                    break;
                }

                string[] tokens = command.Split(" ");

                if (tokens[0] == "swap")
                {
                    if (tokens.Length == 5)
                    {
                        int row1 = int.Parse(tokens[1]);
                        int col1 = int.Parse(tokens[2]);
                        int row2 = int.Parse(tokens[3]);
                        int col2 = int.Parse(tokens[4]);

                        if (row1 < 0 || row1 > matrix.GetLength(0) || row2 < 0 || row2 > matrix.GetLength(0))
                        {
                            Console.WriteLine("Invalid input!");
                            continue;
                        }
                        if (col1 < 0 || col1 > matrix.GetLength(1) || col2 < 0 || col2 > matrix.GetLength(1))
                        {
                            Console.WriteLine("Invalid input!");
                            continue;
                        }

                        string firstElement = matrix[row1, col1];
                        string secondElement = matrix[row2, col2];

                        matrix[row1, col1] = secondElement;
                        matrix[row2, col2] = firstElement;

                        for (int row = 0; row < matrix.GetLength(0); row++)
                        {
                            for (int col = 0; col < matrix.GetLength(1); col++)
                            {
                                Console.Write($"{matrix[row, col]} ");
                            }
                            Console.WriteLine();
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid input!");
                        continue;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input!");
                }
            }
        }
    }
}
