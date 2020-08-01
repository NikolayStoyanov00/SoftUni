using System;
using System.Collections.Generic;
using System.Text;

namespace Vehicles
{
    public interface IVehicle
    {
        public double FuelQuantity { get; set; }

        public double FuelConsumptionPerKm { get; set; }

        public void Drive(double distance);

        public void Refuel(double fuelAmount);
    }
}
