using System;
using System.Collections.Generic;
using System.Text;

namespace MXGP.Core.Contracts
{
    class Engine : IEngine
    {
        public Engine()
        {
            championshipController = new ChampionshipController();
            Run();
        }

        private IChampionshipController championshipController;
        private void Run()
        {
            try
            {
                while (true)
                {
                    string command = Console.ReadLine();

                    if (command == "End")
                    {
                        break;
                    }

                    string[] commandParams = command.Split(" ", StringSplitOptions.RemoveEmptyEntries);

                    if (commandParams[0] == "CreateRider")
                    {
                        string riderName = commandParams[1];
                        Console.WriteLine(championshipController.CreateRider(riderName));
                    }

                    if (commandParams[0] == "CreateMotorcycle")
                    {
                        string motorcycleType = commandParams[1];
                        string model = commandParams[2];
                        int horsePower = int.Parse(commandParams[3]);

                        Console.WriteLine(championshipController.CreateMotorcycle(motorcycleType, model, horsePower));
                    }

                    if (commandParams[0] == "AddMotorcycleToRider")
                    {
                        string riderName = commandParams[1];
                        string motorcycleName = commandParams[2];

                        Console.WriteLine(championshipController.AddMotorcycleToRider(riderName, motorcycleName));
                    }

                    if (commandParams[0] == "AddRiderToRace")
                    {
                        string raceName = commandParams[1];
                        string riderName = commandParams[2];

                        Console.WriteLine(championshipController.AddRiderToRace(raceName, riderName));
                    }

                    if (commandParams[0] == "CreateRace")
                    {
                        string name = commandParams[1];
                        int laps = int.Parse(commandParams[2]);

                        Console.WriteLine(championshipController.CreateRace(name, laps));
                    }

                    if (commandParams[0] == "StartRace")
                    {
                        string raceName = commandParams[1];
                        Console.WriteLine(championshipController.StartRace(raceName));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Run();
            }
           
        }
    }
}
