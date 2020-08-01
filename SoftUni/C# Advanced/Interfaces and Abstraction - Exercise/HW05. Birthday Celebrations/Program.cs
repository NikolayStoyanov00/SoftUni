using System;
using System.Collections.Generic;

namespace _05
{
    class Program
    {
        static void Main(string[] args)
        {
            List<IBirthdate> entities = new List<IBirthdate>();

            while (true)
            {
                string command = Console.ReadLine();

                if (command == "End")
                {
                    break;
                }

                string[] tokens = command
                    .Split(" ", StringSplitOptions.RemoveEmptyEntries);

                if (tokens[0] == "Citizen")
                {
                    string citizenName = tokens[1];
                    int citizenAge = int.Parse(tokens[2]);
                    string citizenId = tokens[3];
                    string citizenBirthdate = tokens[4];

                    Citizen citizen = new Citizen(citizenName, citizenAge, citizenId, citizenBirthdate);
                    entities.Add(citizen);
                }

                else if (tokens[0] == "Pet")
                {
                    string petName = tokens[1];
                    string petBirthdate = tokens[2];

                    Pet pet = new Pet(petName, petBirthdate);
                    entities.Add(pet);
                }
            }

            string year = Console.ReadLine();

            foreach (var entity in entities)
            {
                string[] entityBirthdate = entity.Birthdate.Split("/");

                string birthdateYear = entityBirthdate[2];

                if (birthdateYear == year)
                {
                    Console.WriteLine(entity.Birthdate);
                }
            }

        }
    }
}
