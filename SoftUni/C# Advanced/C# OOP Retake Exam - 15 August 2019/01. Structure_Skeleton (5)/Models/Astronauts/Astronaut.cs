using SpaceStation.Models.Astronauts.Contracts;
using SpaceStation.Models.Bags;
using SpaceStation.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceStation.Models.Astronauts
{
    public abstract class Astronaut : IAstronaut
    {
        public Astronaut(string name, double oxygen)
        {
            this.Name = name;
            this.Oxygen = oxygen;
            this.Bag = new Backpack();
        }

        private string name;
        public string Name
        {
            get => this.name;

            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException(ExceptionMessages.InvalidAstronautName);
                }

                this.name = value;
            }
        }


        private double oxygen;
        public double Oxygen
        {
            get => this.oxygen;

            //check if structure doesn't give 50/50 because of protected instead of private
            protected set
            {
                if (value < 0)
                {
                    throw new ArgumentException(ExceptionMessages.InvalidOxygen);
                }

                this.oxygen = value;
            }
        }

        public bool CanBreath => oxygen > 0;

        private IBag bag;
        public IBag Bag
        {
            get => this.bag;

            private set
            {
                this.bag = value;
            }
        }

        public virtual void Breath()
        {
            this.Oxygen -= 10;

            if (this.Oxygen < 0)
            {
                this.Oxygen = 0;
            }
        }
    }
}
