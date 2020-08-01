using System;
using System.Collections.Generic;
using System.Text;

namespace HW07._Raw_Data
{
    public class Cargo
    {
        public int CargoWeight { get; set; }

        public string CargoType { get; set; }

        public Cargo (int cargoWeight, string cargoType)
        {
            this.CargoWeight = cargoWeight;
            this.CargoType = cargoType;
        }
    }
}
