using System;
using System.Collections.Generic;
using System.Text;

namespace Vehicles
{
    public class Truck : IVehicle
    {
        public Truck(double fuelQuantity, double fuelConsumptionPerKm)
        {
            this.FuelQuantity = fuelQuantity;
            this.FuelConsumptionPerKm = fuelConsumptionPerKm + 1.6;
        }

        public double FuelQuantity { get; set; }
        public double FuelConsumptionPerKm { get; set; }

        public void Drive(double distance)
        {
            if (this.FuelQuantity > distance * this.FuelConsumptionPerKm)
            {
                Console.WriteLine($"Truck travelled {distance} km");
                this.FuelQuantity -= distance * this.FuelConsumptionPerKm;
            }
            else
            {
                Console.WriteLine($"Truck needs refueling");
            }
        }

        public void Refuel(double fuelAmount)
        {
            this.FuelQuantity += fuelAmount * 0.95;
        }
    }
}
