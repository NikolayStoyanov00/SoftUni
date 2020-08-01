using AquaShop.Core.Contracts;
using AquaShop.Models.Aquariums;
using AquaShop.Models.Aquariums.Contracts;
using AquaShop.Models.Decorations;
using AquaShop.Models.Decorations.Contracts;
using AquaShop.Models.Fish;
using AquaShop.Models.Fish.Contracts;
using AquaShop.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AquaShop.Core
{
    public class Controller : IController
    {
        public Controller()
        {
            this.decorations = new DecorationRepository();
            aquariums = new List<IAquarium>();
        }

        private DecorationRepository decorations;
        private List<IAquarium> aquariums;
        public string AddAquarium(string aquariumType, string aquariumName)
        {
            IAquarium aquarium = null;

            if (aquariumType == "FreshwaterAquarium")
            {
                aquarium = new FreshwaterAquarium(aquariumName);
            }
            else if (aquariumType == "SaltwaterAquarium")
            {
                aquarium = new SaltwaterAquarium(aquariumName);
            }
            else
            {
                throw new InvalidOperationException("Invalid aquarium type.");
            }

            aquariums.Add(aquarium);
            return $"Successfully added {aquarium.GetType().Name}.";
        }

        public string AddDecoration(string decorationType)
        {
            IDecoration decoration = null;

            if (decorationType == "Ornament")
            {
                decoration = new Ornament();
            }
            else if (decorationType == "Plant")
            {
                decoration = new Plant();
            }
            else
            {
                throw new InvalidOperationException("Invalid decoration type.");
            }

            decorations.Add(decoration);
            return $"Successfully added {decoration.GetType().Name}.";
        }

        public string AddFish(string aquariumName, string fishType, string fishName, string fishSpecies, decimal price)
        {
            IAquarium aquarium = aquariums.Find(x => x.Name == aquariumName);
            IFish fish = null;

            if (fishType == "FreshwaterFish")
            {
                fish = new FreshwaterFish(fishName, fishSpecies, price);
            }
            else if (fishType == "SaltwaterFish")
            {
                fish = new SaltwaterFish(fishName, fishSpecies, price);
            }
            else
            {
                throw new InvalidOperationException("Invalid fish type.");
            }

            if (aquarium.GetType().Name == "FreshwaterAquarium" && fish.GetType().Name == "FreshwaterFish")
            {
                aquarium.AddFish(fish);
                return $"Successfully added {fish.GetType().Name} to {aquarium.Name}.";
            }
            else if (aquarium.GetType().Name == "SaltwaterAquarium" && fish.GetType().Name == "SaltwaterFish")
            {
                aquarium.AddFish(fish);
                return $"Successfully added {fish.GetType().Name} to {aquarium.Name}.";
            }
            else
            {
                return "Water not suitable.";
            }
        }

        public string CalculateValue(string aquariumName)
        {
            IAquarium aquarium = aquariums.Find(x => x.Name == aquariumName);
            decimal value = aquarium.Fish.Sum(x => x.Price) + aquarium.Decorations.Sum(x => x.Price);
            return $"The value of Aquarium {aquariumName} is {value:f2}.";
        }

        public string FeedFish(string aquariumName)
        {
            IAquarium aquarium = aquariums.Find(x => x.Name == aquariumName);
            int countOfFedFish = aquarium.Fish.Count;
            aquarium.Feed();
            return $"Fish fed: {countOfFedFish}";
        }

        public string InsertDecoration(string aquariumName, string decorationType)
        {
            IAquarium aquarium = aquariums.Find(x => x.Name == aquariumName);
            IDecoration decoration = decorations.FindByType(decorationType);

            if (decoration == null)
            {
                throw new InvalidOperationException($"There isn't a decoration of type {decorationType}.");
            }

            aquarium.AddDecoration(decoration);
            decorations.Remove(decoration);

            return $"Successfully added {decorationType} to {aquariumName}.";
        }

        public string Report()
        {
            StringBuilder sb = new StringBuilder();

            foreach (var aquarium in aquariums)
            {
                sb.AppendLine(aquarium.GetInfo());
            }

            return sb.ToString().TrimEnd();
        }
    }
}
