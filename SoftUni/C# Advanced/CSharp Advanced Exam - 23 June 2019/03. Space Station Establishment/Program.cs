using System;

namespace _03._Space_Station_Establishment
{
    class Program
    {
        static void Main(string[] args)
        {
            int size = int.Parse(Console.ReadLine());

            char[,] matrix = new char[size, size];

            int starEnergy = 0;

            int playerRow = 0;
            int playerCol = 0;

            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                char[] rowElements = Console.ReadLine().ToCharArray();

                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    matrix[row, col] = rowElements[col];

                    if (rowElements[col] == 'S')
                    {
                        playerRow = row;
                        playerCol = col;
                    }
                }
            }

            while (true)
            {
                string command = Console.ReadLine();

                if (command == "up")
                {
                    if (IsInside(playerRow - 1, playerCol, matrix))
                    {
                        matrix[playerRow, playerCol] = '-';
                        playerRow--;

                        int[] rowCol = ChecksChars(playerRow, playerCol, matrix, starEnergy);

                        playerRow = rowCol[0];
                        playerCol = rowCol[1];
                        starEnergy += rowCol[2];
                    }
                    else
                    {
                        matrix[playerRow, playerCol] = '-';
                        Console.WriteLine("Bad news, the spaceship went to the void.");
                        break;
                    }
                }
                else if (command == "down")
                {
                    if (IsInside(playerRow + 1, playerCol, matrix))
                    {
                        matrix[playerRow, playerCol] = '-';
                        playerRow++;

                        int[] rowCol = ChecksChars(playerRow, playerCol, matrix, starEnergy);

                        playerRow = rowCol[0];
                        playerCol = rowCol[1];
                        starEnergy += rowCol[2];
                    }
                    else
                    {
                        matrix[playerRow, playerCol] = '-';
                        Console.WriteLine("Bad news, the spaceship went to the void.");
                        break;
                    }
                }

                else if (command == "left")
                {
                    if (IsInside(playerRow, playerCol - 1, matrix))
                    {
                        matrix[playerRow, playerCol] = '-';
                        playerCol--;

                        int[] rowCol = ChecksChars(playerRow, playerCol, matrix, starEnergy);

                        playerRow = rowCol[0];
                        playerCol = rowCol[1];
                        starEnergy += rowCol[2];
                    }
                    else
                    {
                        matrix[playerRow, playerCol] = '-';
                        Console.WriteLine("Bad news, the spaceship went to the void.");
                        break;
                    }
                }
                else if (command == "right")
                {
                    if (IsInside(playerRow, playerCol + 1, matrix))
                    {
                        matrix[playerRow, playerCol] = '-';
                        playerCol++;

                        int[] rowCol = ChecksChars(playerRow, playerCol, matrix, starEnergy);

                        playerRow = rowCol[0];
                        playerCol = rowCol[1];
                        starEnergy += rowCol[2];
                    }
                    else
                    {
                        matrix[playerRow, playerCol] = '-';
                        Console.WriteLine("Bad news, the spaceship went to the void.");
                        break;
                    }
                }

                if (starEnergy >= 50)
                {
                    Console.WriteLine("Good news! Stephen succeeded in collecting enough star power!");
                    break;
                }
            }

            Console.WriteLine($"Star power collected: {starEnergy}");

            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                for (int col = 0; col < matrix.GetLength(0); col++)
                {
                    Console.Write(matrix[row, col]);
                }
                Console.WriteLine();
            }


        }

        private static int[] ChecksChars(int playerRow, int playerCol, char[,] matrix, int starEnergy)
        {
            int[] rowColEnergy = new int[3];
            if (char.IsDigit(matrix[playerRow, playerCol]))
            {
                starEnergy = (int)(matrix[playerRow, playerCol] - '0');
                matrix[playerRow, playerCol] = 'S';
            }
            else
            {
                starEnergy = 0;
            }

            if (matrix[playerRow, playerCol] == 'O')
            {
                matrix[playerRow, playerCol] = '-';
                for (int row = 0; row < matrix.GetLength(0); row++)
                {
                    for (int col = 0; col < matrix.GetLength(1); col++)
                    {
                        if (matrix[row, col] == 'O')
                        {
                            playerRow = row;
                            playerCol = col;
                        }
                    }
                }
            }
            rowColEnergy[0] = playerRow;
            rowColEnergy[1] = playerCol;
            rowColEnergy[2] = starEnergy;
            starEnergy = 0;

            return rowColEnergy;
        }

        private static bool IsInside(int row, int col, char[,] matrix)
        {
            if (row >= 0 && row < matrix.GetLength(0)
                && col >= 0 && col < matrix.GetLength(1))
            {
                return true;
            }
            return false;
        }
    }
}
