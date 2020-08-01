using MXGP.Models.Motorcycles.Contracts;
using MXGP.Models.Riders.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace MXGP.Models.Riders
{
    public class Rider : IRider
    {
        public Rider(string name)
        {
            this.Name = name;
        }

        private string name;
        public string Name
        {
            get => this.name;

            private set
            {
                if (string.IsNullOrEmpty(value) || value.Length < 5)
                {
                    throw new ArgumentException($"Name {value} cannot be less than 5 symbols.");
                }

                this.name = value;
            }
        }

        private IMotorcycle motorcycle;
        public IMotorcycle Motorcycle
        {
            get => this.motorcycle;

            private set
            {
                this.motorcycle = value;
            }
        }

        private int numberOfWins;
        public int NumberOfWins
        {
            get => this.numberOfWins;

            private set
            {
                this.numberOfWins = value;
            }
        }

        private bool canParticipate;
        public bool CanParticipate
        {
            get => this.canParticipate;

            private set
            {
                if (motorcycle != null)
                {
                    this.canParticipate = true;
                }
            }
        }

        public void AddMotorcycle(IMotorcycle motorcycle)
        {
            if (motorcycle == null)
            {
                throw new ArgumentException("Motorcycle cannot be null.");
            }

            this.motorcycle = motorcycle;
            this.canParticipate = true;
        }

        public void WinRace()
        {
            this.numberOfWins++;
        }
    }
}
