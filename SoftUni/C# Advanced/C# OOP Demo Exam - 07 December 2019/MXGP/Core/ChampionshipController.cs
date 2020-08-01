using MXGP.Core.Contracts;
using MXGP.Models.Motorcycles;
using MXGP.Models.Motorcycles.Contracts;
using MXGP.Models.Races;
using MXGP.Models.Races.Contracts;
using MXGP.Models.Riders;
using MXGP.Models.Riders.Contracts;
using MXGP.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MXGP.Core
{
    public class ChampionshipController : IChampionshipController
    {
        private RiderRepository riderRepository;
        private MotorcycleRepository motorcycleRepository;
        private RaceRepository raceRepository;

        public ChampionshipController()
        {
            riderRepository = new RiderRepository();
            motorcycleRepository = new MotorcycleRepository();
            raceRepository = new RaceRepository();
        }

        public string AddMotorcycleToRider(string riderName, string motorcycleModel)
        {
            IMotorcycle motorcycle = motorcycleRepository.GetByName(motorcycleModel);
            IRider rider = riderRepository.GetByName(riderName);

            if (rider == null)
            {
                throw new InvalidOperationException($"Rider {riderName} could not be found.");
            }

            if (motorcycle == null)
            {
                throw new InvalidOperationException($"Motorcycle {motorcycleModel} could not be found.");
            }

            rider.AddMotorcycle(motorcycle);

            return $"Rider {rider.Name} received motorcycle {rider.Motorcycle.Model}.";
        }

        public string AddRiderToRace(string raceName, string riderName)
        {
            IRider rider = riderRepository.GetByName(riderName);
            IRace race = raceRepository.GetByName(raceName);

            if (race == null)
            {
                throw new InvalidOperationException($"Race {raceName} could not be found.");
            }

            if (rider == null)
            {
                throw new InvalidOperationException($"Rider {riderName} could not be found.");
            }

            race.AddRider(rider);
            return $"Rider {rider.Name} added in {race.Name} race.";
        }

        public string CreateMotorcycle(string type, string model, int horsePower)
        {
            IMotorcycle motorcycle = null;

            if (type == "Speed")
            {
                motorcycle = new SpeedMotorcycle(model, horsePower);
            }
            else if (type == "Power")
            {
                motorcycle = new PowerMotorcycle(model, horsePower);
            }

            if (motorcycleRepository.GetByName(model) != null)
            {
                throw new ArgumentException($"Motorcycle {model} is already created.");
            }

            motorcycleRepository.Add(motorcycle);

            return $"{motorcycle.GetType().Name} {motorcycle.Model} is created.";
        }

        public string CreateRace(string name, int laps)
        {
            Race race = new Race(name, laps);

            if (raceRepository.GetByName(name) != null)
            {
                throw new InvalidOperationException($"Race {name} is already created.");
            }

            raceRepository.Add(race);
            return $"Race {race.Name} is created.";
        }

        public string CreateRider(string riderName)
        {
            IRider rider = new Rider(riderName);
            if (riderRepository.GetByName(riderName) != null) 
            {
                throw new ArgumentException($"Rider {riderName} is already created.");
            }

            riderRepository.Add(rider);
            return $"Rider {rider.Name} created.";
        }

        public string StartRace(string raceName)
        {
            int laps = raceRepository.GetByName(raceName).Laps;

            if (raceRepository.GetByName(raceName) == null)
            {
                throw new InvalidOperationException($"Race {raceName} could not be found.");
            }

            List<IRider> allRiders = riderRepository.GetAll().OrderByDescending(x => x.Motorcycle.CalculateRacePoints(laps)).ToList();

            if (allRiders.Count < 3)
            {
                throw new InvalidOperationException($"Race {raceName} cannot start with less than 3 participants.");
            }

            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Rider {allRiders[0].Name} wins {raceName} race.");
            sb.AppendLine($"Rider {allRiders[1].Name} is second in {raceName} race.");
            sb.AppendLine($"Rider {allRiders[2].Name} is third in {raceName} race.");
            return sb.ToString().TrimEnd();
        }
    }
}
