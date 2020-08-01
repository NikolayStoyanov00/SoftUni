using SpaceStation.Core.Contracts;
using SpaceStation.Models.Astronauts;
using SpaceStation.Models.Astronauts.Contracts;
using SpaceStation.Models.Mission;
using SpaceStation.Models.Planets;
using SpaceStation.Repositories;
using SpaceStation.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceStation.Core
{
    public class Controller : IController
    {
        private AstronautRepository astronautRepository = new AstronautRepository();
        private PlanetRepository planetRepository = new PlanetRepository();

        public string AddAstronaut(string type, string astronautName)
        {
            IAstronaut astronaut = null;

            if (type == "Biologist")
            {
                astronaut = new Biologist(astronautName);
            }

            else if (type == "Geodesist")
            {
                astronaut = new Geodesist(astronautName);
            }

            else if (type == "Meteorologist")
            {
                astronaut = new Meteorologist(astronautName);
            }

            if (astronaut == null)
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidAstronautType);
            }

            astronautRepository.Add(astronaut);
            return $"Successfully added {astronaut.GetType().Name}: {astronaut.Name}!";
        }

        public string AddPlanet(string planetName, params string[] items)
        {
            IPlanet planet = new Planet(planetName);
            foreach (var item in items)
            {
                planet.Items.Add(item);
            }

            planetRepository.Add(planet);
            return $"Successfully added Planet: {planet.Name}!";
        }

        public string ExplorePlanet(string planetName)
        {
            List<IAstronaut> suitableAstronauts = astronautRepository.Models.Where(x => x.Oxygen > 60).ToList();

            if (suitableAstronauts.Count == 0)
            {
                throw new InvalidOperationException("You need at least one astronaut to explore the planet!");
            }

            IPlanet planet = planetRepository.FindByName(planetName);

            Mission mission = new Mission();
            mission.Explore(planet, suitableAstronauts);

            int deadAstronauts = suitableAstronauts.Where(x => x.CanBreath == false).Count();

            return $"Planet: {planet.Name} was explored! Exploration finished with {deadAstronauts} dead astronauts!";
        }

        public string Report()
        {
            StringBuilder sb = new StringBuilder();

            int exploredPlanets = planetRepository.Models.Where(x => x.Items.Count == 0).Count();
            sb.AppendLine($"{exploredPlanets} planets were explored!");

            foreach (var astronaut in astronautRepository.Models)
            {
                sb.AppendLine($"Name: {astronaut.Name}");
                sb.AppendLine($"Oxygen: {astronaut.Oxygen}");

                sb.Append("Bag items: ");
                if (astronaut.Bag.Items.Count > 0)
                {
                    sb.AppendLine(string.Join(", ", astronaut.Bag.Items));
                }
                else
                {
                    sb.AppendLine("none");
                }
            }

            return sb.ToString().TrimEnd();
        }

        public string RetireAstronaut(string astronautName)
        {
            IAstronaut astronaut = astronautRepository.FindByName(astronautName);

            if (astronaut == null)
            {
                throw new InvalidOperationException($"Astronaut {astronautName} doesn't exists!");
            }

            astronautRepository.Remove(astronaut);
            return $"Astronaut {astronaut.Name} was retired!";
        }
    }
}
