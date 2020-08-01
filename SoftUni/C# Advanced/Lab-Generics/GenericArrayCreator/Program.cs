using System;

namespace GenericArrayCreator
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            string[] strings = ArrayCreator.Create(5, "Pesho");
            int[] integers = ArrayCreator.Create(10, 33);

            foreach (var str in strings)
            {
                Console.WriteLine(str);
            }

            foreach (var integer in integers)
            {
                Console.WriteLine(integer);
            }
        }
    }
}
