using System;

namespace HW07._Predicate_For_Names
{
    class Program
    {
        static void Main(string[] args)
        {
            int nameLenght = int.Parse(Console.ReadLine());

            string[] names = Console.ReadLine()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries);

            Predicate<string> predicate = x => x.Length <= nameLenght;

            foreach (var name in names)
            {
                bool nameIsViable = predicate(name);

                if (nameIsViable)
                {
                    Console.WriteLine(name);
                }
            }
        }
    }
}
