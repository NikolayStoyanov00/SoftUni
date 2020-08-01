using System;
using System.Linq;

namespace HW9._Miner
{
    class Program
    {
        static void Main(string[] args)
        {
            int size = int.Parse(Console.ReadLine());
            string[] directions = Console.ReadLine()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries);

            int[,] matrix = new int[size, size];


            int minerRow = 0;
            int minerCol = 0;
            int coalsCollected = 0;
            int totalCoals = 0;

            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                char[] rowElements = Console.ReadLine()
                    .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                    .Select(char.Parse)
                    .ToArray();

                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    matrix[row, col] = rowElements[col];

                    if (rowElements[col] == 's')
                    {
                        minerRow = row;
                        minerCol = col;
                    }
                    if (rowElements[col] == 'c')
                    {
                        totalCoals++;
                    }
                }
            }

            for (int i = 0; i < directions.Length; i++)
            {
                if (directions[i] == "left")
                {
                    if (IsInside(matrix, minerRow, minerCol - 1))
                    {
                        if (matrix[minerRow, minerCol - 1] == 'e')
                        {
                            Console.WriteLine($"Game over! ({minerRow}, {minerCol - 1})");
                            return;
                        }

                        if (matrix[minerRow, minerCol - 1] == 'c')
                        {
                            coalsCollected++;
                            matrix[minerRow, minerCol - 1] = '*';

                            if (coalsCollected == totalCoals)
                            {
                                Console.WriteLine($"You collected all coals! ({minerRow}, {minerCol - 1})");
                                return;
                            }
                        }
                        minerCol -= 1;
                    }
                }

                else if (directions[i] == "right")
                {
                    if (IsInside(matrix, minerRow, minerCol + 1))
                    {
                        if (matrix[minerRow, minerCol + 1] == 'e')
                        {
                            Console.WriteLine($"Game over! ({minerRow}, {minerCol + 1})");
                            return;
                        }

                        if (matrix[minerRow, minerCol + 1] == 'c')
                        {
                            coalsCollected++;
                            matrix[minerRow, minerCol + 1] = '*';

                            if (coalsCollected == totalCoals)
                            {
                                Console.WriteLine($"You collected all coals! ({minerRow}, {minerCol + 1})");
                                return;
                            }
                        }

                        minerCol += 1;
                    }
                }

                else if (directions[i] == "up")
                {
                    if (IsInside(matrix, minerRow - 1, minerCol))
                    {
                        if (matrix[minerRow - 1, minerCol] == 'e')
                        {
                            Console.WriteLine($"Game over! ({minerRow - 1}, {minerCol})");
                            return;
                        }

                        if (matrix[minerRow - 1, minerCol] == 'c')
                        {
                            coalsCollected++;
                            matrix[minerRow - 1, minerCol] = '*';

                            if (coalsCollected == totalCoals)
                            {
                                Console.WriteLine($"You collected all coals! ({minerRow - 1}, {minerCol})");
                                return;
                            }
                        }
                        minerRow -= 1;
                    }
                }

                else if (directions[i] == "down")
                {
                    if (IsInside(matrix, minerRow + 1, minerCol))
                    {
                        if (matrix[minerRow + 1, minerCol] == 'e')
                        {
                            Console.WriteLine($"Game over! ({minerRow + 1}, {minerCol})");
                            return;
                        }

                        if (matrix[minerRow + 1, minerCol] == 'c')
                        {
                            coalsCollected++;
                            matrix[minerRow + 1, minerCol] = '*';

                            if (coalsCollected == totalCoals)
                            {
                                Console.WriteLine($"You collected all coals! ({minerRow + 1}, {minerCol})");
                                return;
                            }
                        }
                        minerRow += 1;
                    }
                }
            }

            Console.WriteLine($"{totalCoals - coalsCollected} coals left. ({minerRow}, {minerCol})");
        }

        private static bool IsInside(int[,] matrix, int minerRow, int minerCol)
        {
            if (minerRow >= 0 && minerRow < matrix.GetLength(0)
                && minerCol >= 0 && minerCol < matrix.GetLength(1))
            {
                return true;
            }
            return false;
        }
    }
}
