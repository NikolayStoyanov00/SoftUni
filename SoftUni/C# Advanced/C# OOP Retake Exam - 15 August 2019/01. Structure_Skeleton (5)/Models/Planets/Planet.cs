using SpaceStation.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceStation.Models.Planets
{
    public class Planet : IPlanet
    {
        public Planet(string name)
        {
            this.Name = name;
            items = new List<string>();
        }

        private List<string> items;
        public ICollection<string> Items
        {
            get => this.items;

            // Check if structure doesn't give 50/50 because of private instead of protected
            private set
            {
                this.items = value.ToList();
            }
        }

        private string name;
        public string Name
        {
            get => this.name;

            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException(ExceptionMessages.InvalidPlanetName);
                }

                this.name = value;
            }
        }
    }
}
