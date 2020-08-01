using System;

namespace _7._Pascal_Triangle
{
    class Program
    {
        static void Main(string[] args)
        {
            int pascalSize = int.Parse(Console.ReadLine());

            long[][] pascalArray = new long[pascalSize][];
            pascalArray[0] = new long[1];
            pascalArray[0][0] = 1;

            if (pascalSize > 1)
            {
                pascalArray[1] = new long[2];
                pascalArray[1][0] = 1;
                pascalArray[1][1] = 1;

                for (int row = 2; row < pascalArray.Length; row++)
                {
                    pascalArray[row] = new long[row + 1];
                    pascalArray[row][0] = 1;
                    pascalArray[row][pascalArray[row].Length - 1] = 1;

                    for (int col = 1; col < pascalArray[row].Length - 1; col++)
                    {
                        pascalArray[row][col] = pascalArray[row - 1][col] + pascalArray[row - 1][col - 1];
                    }
                }
            }
            foreach (var array in pascalArray)
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
