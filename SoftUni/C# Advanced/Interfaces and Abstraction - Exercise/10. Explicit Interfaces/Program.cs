using System;

namespace _10._Explicit_Interfaces
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                string command = Console.ReadLine();

                if (command == "End")
                {
                    break;
                }

                string[] tokens = command
                    .Split(" ", StringSplitOptions.RemoveEmptyEntries);

                string name = tokens[0];
                string country = tokens[1];
                int age = int.Parse(tokens[2]);

                IResident citizenResident = new Citizen(name, country, age);
                IPerson citizenPerson = new Citizen(name, country, age);

                Console.WriteLine(citizenPerson.GetName());
                Console.WriteLine($"{citizenResident.GetName()} {citizenPerson.GetName()}");

            }
        }
    }
}
