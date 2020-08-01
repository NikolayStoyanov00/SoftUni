using MXGP.Models.Motorcycles.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace MXGP.Models.Motorcycles
{
    public class SpeedMotorcycle : Motorcycle, IMotorcycle
    {
        private const double cubicCentimeters = 125;
        public SpeedMotorcycle(string model, int horsePower) 
            : base(model, horsePower, cubicCentimeters)
        {
            if (horsePower < 50 || horsePower > 69)
            {
                throw new ArgumentException($"Invalid horse power: {horsePower}.");
            }
        }
    }
}
