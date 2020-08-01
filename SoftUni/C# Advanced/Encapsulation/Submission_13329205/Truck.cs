﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Vehicles
{
    public class Truck : IVehicle
    {
        public Truck(double fuelQuantity, double fuelConsumptionPerKm, double tankCapacity)
        {
            this.FuelConsumptionPerKm = fuelConsumptionPerKm + 1.6;

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
            if (fuelAmount > 0)
            {
                if (this.FuelQuantity + fuelAmount <= this.TankCapacity)
                {
                    this.FuelQuantity += fuelAmount * 0.95;
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