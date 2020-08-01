using System;
using System.Collections.Generic;

namespace Generic_Box_of_String
{
    public class Program
    {
        public static void Main(string[] args)
        {
            int lines = int.Parse(Console.ReadLine());
            for (int i = 0; i < lines; i++)
            {
                var line = Console.ReadLine();
                var lineType = line.GetType();

                int number = 0;
                bool trueOrFalse;
                bool parser = int.TryParse(line, out number);
                bool booler = bool.TryParse(line, out trueOrFalse);
                if (number != 0)
                {
                    Box<int> box = new Box<int>();
                    box.Value = number;
                    Console.WriteLine(box.ToString());
                    continue;
                }
                else if (booler == true)
                {
                    Box<bool> box = new Box<bool>();
                    box.Value = trueOrFalse;
                    Console.WriteLine(box.ToString());
                }
                else
                {
                    Box<string> box = new Box<string>();
                    box.Value = line;
                    Console.WriteLine(box.ToString());
                }
            }
        }
    }
}
