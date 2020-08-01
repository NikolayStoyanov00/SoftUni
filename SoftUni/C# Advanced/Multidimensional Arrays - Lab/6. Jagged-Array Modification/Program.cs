using System;
using System.Linq;

namespace _6._Jagged_Array_Modification
{
    class Program
    {
        static void Main(string[] args)
        {
            int matrixSize = int.Parse(Console.ReadLine());

            int[][] jaggedArray = new int[matrixSize][];

            for (int row = 0; row < jaggedArray.GetLength(0); row++)
            {
                int[] colNumbers = Console.ReadLine()
                    .Split(" ")
                    .Select(int.Parse)
                    .ToArray();

                jaggedArray[row] = new int[colNumbers.Length];

                for (int col = 0; col < colNumbers.Length; col++)
                {
                    jaggedArray[row][col] = colNumbers[col];
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

                if (tokens[0] == "Add")
                {
                    int row = int.Parse(tokens[1]);
                    int col = int.Parse(tokens[2]);
                    int value = int.Parse(tokens[3]);

                    if (row >= 0 && row < jaggedArray.GetLength(0) && col >= 0 && col < jaggedArray[row].Length)
                    {
                        jaggedArray[row][col] += value;
                    }
                    else
                    {
                        Console.WriteLine("Invalid coordinates");
                    }
                }
                else if (tokens[0] == "Subtract")
                {
                    int row = int.Parse(tokens[1]);
                    int col = int.Parse(tokens[2]);
                    int value = int.Parse(tokens[3]);

                    if (row >= 0 && row < jaggedArray.GetLength(0) && col >= 0 && col < jaggedArray[row].Length)
                    {
                        jaggedArray[row][col] -= value;
                    }
                    else
                    {
                        Console.WriteLine("Invalid coordinates");
                    }
                }
            }

            foreach (var array in jaggedArray)
            {
                foreach (var number in array)
                {
                    Console.Write(number + " ");
                }
                Console.WriteLine();
            }
        }
    }
}
