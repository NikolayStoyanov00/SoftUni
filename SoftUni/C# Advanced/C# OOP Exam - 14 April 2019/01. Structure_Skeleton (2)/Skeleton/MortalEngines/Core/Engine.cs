using MortalEngines.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace MortalEngines.Core
{
    public class Engine : IEngine
    {
        private IMachinesManager machinesManager = new MachinesManager();
        public void Run()
        {

            while (true)
            {
                try
                {
                    string command = Console.ReadLine();

                    if (command == "Quit")
                    {
                        break;
                    }

                    string[] commandParts = command.Split(" ", StringSplitOptions.RemoveEmptyEntries);

                    if (commandParts[0] == "HirePilot")
                    {
                        Console.WriteLine(machinesManager.HirePilot(commandParts[1]));
                    }

                    else if (commandParts[0] == "PilotReport")
                    {
                        Console.WriteLine(machinesManager.PilotReport(commandParts[1]));
                    }

                    else if (commandParts[0] == "ManufactureTank")
                    {
                        string name = commandParts[1];
                        double attack = double.Parse(commandParts[2]);
                        double defense = double.Parse(commandParts[3]);

                        Console.WriteLine(machinesManager.ManufactureTank(name, attack, defense));
                    }

                    else if (commandParts[0] == "ManufactureFighter")
                    {
                        string name = commandParts[1];
                        double attack = double.Parse(commandParts[2]);
                        double defense = double.Parse(commandParts[3]);

                        Console.WriteLine(machinesManager.ManufactureFighter(name, attack, defense));
                    }

                    else if (commandParts[0] == "MachineReport")
                    {
                        Console.WriteLine(machinesManager.MachineReport(commandParts[1]));
                    }

                    else if (commandParts[0] == "AggressiveMode")
                    {
                        Console.WriteLine(machinesManager.ToggleFighterAggressiveMode(commandParts[1]));
                    }

                    else if (commandParts[0] == "DefenseMode")
                    {
                        Console.WriteLine(machinesManager.ToggleTankDefenseMode(commandParts[1]));
                    }

                    else if (commandParts[0] == "Engage")
                    {
                        string pilotName = commandParts[1];
                        string machineName = commandParts[2];
                        Console.WriteLine(machinesManager.EngageMachine(pilotName, machineName));
                    }

                    else if (commandParts[0] == "Attack")
                    {
                        string attackingMachineName = commandParts[1];
                        string defendingMachineName = commandParts[2];
                        Console.WriteLine(machinesManager.AttackMachines(attackingMachineName, defendingMachineName));
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }

    }
}
