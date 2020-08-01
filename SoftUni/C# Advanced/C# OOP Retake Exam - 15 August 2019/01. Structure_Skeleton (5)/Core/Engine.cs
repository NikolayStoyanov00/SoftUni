using SpaceStation.Core.Contracts;
using SpaceStation.IO;
using SpaceStation.IO.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceStation.Core
{
    public class Engine : IEngine
    {
        private IWriter writer;
        private IReader reader;
        private IController controller;

        public Engine()
        {
            this.writer = new Writer();
            this.reader = new Reader();
            this.controller = new Controller();
        }
        public void Run()
        {
            while (true)
            {
                var input = reader.ReadLine().Split();
                if (input[0] == "Exit")
                {
                    Environment.Exit(0);
                }
                try
                {
                    if (input[0] == "AddAstronaut")
                    {
                        Console.WriteLine(controller.AddAstronaut(input[1], input[2]));
                    }
                    else if (input[0] == "AddPlanet")
                    {
                        string planetName = input[1];
                        List<string> items = new List<string>();

                        for (int i = 2; i < input.Length; i++)
                        {
                            items.Add(input[i]);
                        }

                        Console.WriteLine(controller.AddPlanet(planetName, items.ToArray()));
                    }
                    else if (input[0] == "RetireAstronaut")
                    {
                        Console.WriteLine(controller.RetireAstronaut(input[1]));
                    }
                    else if (input[0] == "ExplorePlanet")
                    {
                        Console.WriteLine(controller.ExplorePlanet(input[1]));
                    }
                    else if(input[0] == "Report")
                    {
                        Console.WriteLine(controller.Report());
                    }
                }
                catch (Exception ex)
                {
                    writer.WriteLine(ex.Message);
                }
            }
        }
    }
}
