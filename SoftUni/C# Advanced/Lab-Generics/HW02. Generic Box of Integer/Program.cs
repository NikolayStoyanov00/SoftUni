using System;

namespace Generic_Box_of_Integer
{
    class Program
    {
        static void Main(string[] args)
        {
            int numbers = int.Parse(Console.ReadLine());

            for (int i = 0; i < numbers; i++)
            {
                int number = int.Parse(Console.ReadLine());

                Box<int> box = new Box<int>();
                box.Value = number;
                Console.WriteLine(box.ToString());
            }
        }
    }
}
