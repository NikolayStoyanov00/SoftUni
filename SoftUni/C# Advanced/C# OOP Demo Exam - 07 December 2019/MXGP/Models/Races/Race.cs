using MXGP.Models.Races.Contracts;
using MXGP.Models.Riders.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace MXGP.Models.Races
{
    public class Race : IRace
    {
        public Race(string name, int laps)
        {
            this.Name = name;
            this.Laps = laps;
            riders = new List<IRider>();
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

        private int laps;
        public int Laps
        {
            get => this.laps;

            private set
            {
                if (value < 1)
                {
                    throw new ArgumentException("Laps cannot be less than 1.");
                }

                this.laps = value;
            }
        }

        private List<IRider> riders;
        public IReadOnlyCollection<IRider> Riders
        {
            get => this.riders;
        }

        public void AddRider(IRider rider)
        {
            if (rider == null)
            {
                throw new ArgumentNullException("Rider cannot be null.");
            }

            if (rider.CanParticipate == false)
            {
                throw new ArgumentException($"Rider {rider.Name} could not participate in race.");
            }

            if (riders.Contains(rider))
            {
                throw new ArgumentNullException($"Rider {rider.Name} is already added in {this.Name} race.");
            }

            riders.Add(rider);
        }
    }
}
