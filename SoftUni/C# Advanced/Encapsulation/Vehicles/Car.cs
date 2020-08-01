using System;
using System.Collections.Generic;
using System.Text;

namespace Vehicles
{
    public class Car : IVehicle
    {
        public Car(double fuelQuantity, double fuelConsumptionPerKm)
        {
            this.FuelQuantity = fuelQuantity;
            this.FuelConsumptionPerKm = fuelConsumptionPerKm + 0.9;
        }

        public double FuelQuantity { get; set; }
        public double FuelConsumptionPerKm { get; set; }

        public void Drive(double distance)
        {
            if (this.FuelQuantity > distance * this.FuelConsumptionPerKm)
            {
                Console.WriteLine($"Car travelled {distance} km");
                this.FuelQuantity -= distance * this.FuelConsumptionPerKm;
            }
            else
            {
                Console.WriteLine($"Car needs refueling");
            }
        }

        public void Refuel(double fuelAmount)
        {
            this.FuelQuantity += fuelAmount;
        }
    }
}
