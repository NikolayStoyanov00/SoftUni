using System;

namespace _7._Pascal_Triangle
{
    class Program
    {
        static void Main(string[] args)
        {
            int rowsSize = int.Parse(Console.ReadLine());
            long[][] jaggedArray = new long[rowsSize][];
            long cols = 1;

            for (int row = 0; row < rowsSize; row++)
            {
                jaggedArray[row] = new long[cols];
                jaggedArray[row][0] = 1;
                jaggedArray[row][cols - 1] = 1;

                if (cols > 2)
                {
                    long[] previousRow = jaggedArray[row - 1];
                    for (int j = 1; j < cols - 1; j++)
                    {
                        jaggedArray[row][j] = previousRow[j] + previousRow[j - 1];
                    }
                }

                cols++;
            }

            foreach (var item in jaggedArray)
            {
                Console.WriteLine(string.Join(" ", item));
            }
        }
    }
}
