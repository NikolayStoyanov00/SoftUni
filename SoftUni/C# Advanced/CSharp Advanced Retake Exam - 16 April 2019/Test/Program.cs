using System;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            int num = 5;
            int secondNum = 3;

            for (int i = 0; i < 20; i++)
            {
                if (i % num == 0)
                {
                    Console.WriteLine(i);
                }
            }
        }
    }
}
