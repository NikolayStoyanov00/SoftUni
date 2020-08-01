using System;
using System.Collections.Generic;

namespace __Generic_Count_Method_Doubles
{
    public class Program
    {
        public static void Main(string[] args)
        {
            List<Box<double>> boxes = new List<Box<double>>();

            int numbers = int.Parse(Console.ReadLine());
            for (int i = 0; i < numbers; i++)
            {
                double number = double.Parse(Console.ReadLine());

                Box<double> box = new Box<double>();
                box.Value = number;
                boxes.Add(box);
            }

            double comparableNumber = double.Parse(Console.ReadLine());

            int count = ComparesDoubles(boxes, comparableNumber);
            Console.WriteLine(count);
        }

        private static int ComparesDoubles(List<Box<double>> boxes, double comparableNumber)
        {
            int count = 0;

            foreach (var box in boxes)
            {
                if (box.Value > comparableNumber)
                {
                    count++;
                }
            }
            return count;
        }
    }
}
