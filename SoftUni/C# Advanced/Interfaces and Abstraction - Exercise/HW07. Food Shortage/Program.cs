using System;
using System.Collections.Generic;
using System.Linq;

namespace _07._Food_Shortage
{
    class Program
    {
        static void Main(string[] args)
        {
            int numberOfPeople = int.Parse(Console.ReadLine());

            List<Rebel> rebels = new List<Rebel>();
            List<Citizen> citizens = new List<Citizen>();

            for (int i = 0; i < numberOfPeople; i++)
            {
                string[] input = Console.ReadLine()
                    .Split(" ", StringSplitOptions.RemoveEmptyEntries);

                if (input.Length == 3)
                {
                    string rebelName = input[0];
                    int rebelAge = int.Parse(input[1]);
                    string rebelGroup = input[2];

                    Rebel rebel = new Rebel(rebelName, rebelAge, rebelGroup);
                    rebels.Add(rebel);
                }

                else if (input.Length == 4)
                {
                    string citizenName = input[0];
                    int citizenAge = int.Parse(input[1]);
                    string citizenId = input[2];
                    string citizenBirthdate = input[3];

                    Citizen citizen = new Citizen(citizenName, citizenAge, citizenId, citizenBirthdate);
                    citizens.Add(citizen);
                }
            }

            while (true)
            {
                string command = Console.ReadLine();

                if (command == "End")
                {
                    break;
                }

                string name = command;

                if (citizens.FirstOrDefault(x => x.Name == name) != null)
                {
                    citizens.FirstOrDefault(x => x.Name == name).BuyFood();
                }
                else if (rebels.FirstOrDefault(x => x.Name == name) != null)
                {
                    rebels.FirstOrDefault(x => x.Name == name).BuyFood();
                }
            }

            Console.WriteLine($"{citizens.Sum(x => x.Food) + rebels.Sum(x => x.Food)}");
        }
    }
}
