using System;
using System.Collections.Generic;
using System.Text;

namespace Vehicles
{
    public class Bus : IVehicle
    {
        public Bus(double fuelQuantity, double fuelConsumptionPerKm, double tankCapacity)
        {
            this.FuelConsumptionPerKm = fuelConsumptionPerKm;

            if (tankCapacity >= fuelQuantity)
            {
                this.TankCapacity = tankCapacity;
                this.FuelQuantity = fuelQuantity;
            }
            else
            {
                this.TankCapacity = 0;
                this.FuelQuantity = this.TankCapacity;
            }
        }
        public double FuelQuantity { get; set; }

        public double FuelConsumptionPerKm { get; set; }

        public double TankCapacity { get; set; }

        public void Drive(double distance)
        {
            if (this.FuelQuantity > distance * this.FuelConsumptionPerKm)
            {
                Console.WriteLine($"Bus travelled {distance} km");
                this.FuelQuantity -= distance * (this.FuelConsumptionPerKm + 1.4);
            }
            else
            {
                Console.WriteLine($"Bus needs refueling");
            }
        }

        public void DriveEmpty(double distance)
        {
            if (this.FuelQuantity > distance * this.FuelConsumptionPerKm)
            {
                Console.WriteLine($"Bus travelled {distance} km");
                this.FuelQuantity -= distance * this.FuelConsumptionPerKm;
            }
            else
            {
                Console.WriteLine($"Bus needs refueling");
            }
        }

        public void Refuel(double fuelAmount)
        {
            if (fuelAmount > 0)
            {
                if (this.FuelQuantity + fuelAmount <= this.TankCapacity)
                {
                    this.FuelQuantity += fuelAmount;
                }
                else
                {
                    Console.WriteLine($"Cannot fit {fuelAmount} fuel in the tank");
                }
            }
            else
            {
                Console.WriteLine("Fuel must be a positive number");
            }
        }
    }
}
