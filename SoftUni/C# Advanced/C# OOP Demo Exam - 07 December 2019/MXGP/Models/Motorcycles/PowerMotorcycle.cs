using MXGP.Models.Motorcycles.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace MXGP.Models.Motorcycles
{
    public class PowerMotorcycle : Motorcycle, IMotorcycle
    {
        private const double cubicCentimeters = 450;
        public PowerMotorcycle(string model, int horsePower) 
            : base(model, horsePower, cubicCentimeters)
        {
            if (horsePower < 70 || horsePower > 100)
            {
                throw new ArgumentException($"Invalid horse power: {horsePower}.");
            }
        }
    }
}
