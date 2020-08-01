using System;

namespace _03._Ferrari
{
    class Program
    {
        static void Main(string[] args)
        {
            string driverName = Console.ReadLine();

            ICar ferrari = new Ferrari(driverName);

            Console.WriteLine(ferrari);
        }
    }
}
