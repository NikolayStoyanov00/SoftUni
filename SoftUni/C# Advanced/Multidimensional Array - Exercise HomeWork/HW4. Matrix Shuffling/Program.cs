using System;
using System.Linq;

namespace HW4._Matrix_Shuffling
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] matrixSize = Console.ReadLine()   
               .Split(" ", StringSplitOptions.RemoveEmptyEntries)
               .Select(int.Parse)
               .ToArray();

            string[,] matrix = new string[matrixSize[0], matrixSize[1]];

            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                string[] rowElements = Console.ReadLine()
                    .Split(" ", StringSplitOptions.RemoveEmptyEntries);

                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    matrix[row, col] = rowElements[col];
                }
            }

            while (true)
            {
                string command = Console.ReadLine();

                if (command == "END")
                {
                    break;
                }

                string[] line = command.Split(" ", StringSplitOptions.RemoveEmptyEntries);

                if (line[0] == "swap")
                {
                    

                    if (line.Length == 5)
                    {
                        int row = int.Parse(line[1]);
                        int col = int.Parse(line[2]);
                        int changeRow = int.Parse(line[3]);
                        int changeCol = int.Parse(line[4]);

                        if (row >= 0 && row < matrix.GetLength(0)
                            && changeRow >= 0 && changeRow < matrix.GetLength(0)
                            && col >= 0 && col < matrix.GetLength(1)
                            && changeCol >= 0 && changeCol < matrix.GetLength(1))
                        {
                            string firstElement = matrix[row, col];
                            string secondElement = matrix[changeRow, changeCol];
                            matrix[row, col] = secondElement;
                            matrix[changeRow, changeCol] = firstElement;

                            for (int rows = 0; rows < matrix.GetLength(0); rows++)
                            {
                                for (int cols = 0; cols < matrix.GetLength(1); cols++)
                                {
                                    Console.Write(matrix[rows, cols] + " ");
                                }
                                Console.WriteLine();
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid input!");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid input!");
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
