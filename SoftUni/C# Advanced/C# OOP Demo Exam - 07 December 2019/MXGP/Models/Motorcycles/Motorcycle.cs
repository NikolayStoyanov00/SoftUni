using MXGP.Models.Motorcycles.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace MXGP.Models.Motorcycles
{
    public abstract class Motorcycle : IMotorcycle
    {
        protected Motorcycle(string model, int horsePower, double cubicCentimeters)
        {
            this.Model = model;
            this.HorsePower = horsePower;
            this.CubicCentimeters = cubicCentimeters;
        }

        private string model;
        public string Model
        {
            get => this.model;

            private set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Length < 4)
                {
                    throw new ArgumentException($"Model {value} cannot be less than 4 symbols.");
                }

                this.model = value;
            }
        }

        private int horsePower;

        public int HorsePower
        {
            get => this.horsePower;
            protected set
            { 
                this.horsePower = value;
            }
        }

        private double cubicCentimeters;
        public double CubicCentimeters
        {
            get => this.cubicCentimeters;

            private set
            {
                this.cubicCentimeters = value;
            }
        }

        public double CalculateRacePoints(int laps)
        {
            double result = this.CubicCentimeters / this.HorsePower * laps;
            return result;
        }
    }
}
