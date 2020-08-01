using System;
using System.Collections.Generic;
using System.Text;

namespace HW6._Speed_Racing
{
    public class Car
    {
        public string Model { get; set; }

        public double FuelAmount { get; set; }

        public double FuelConsumptionPerKilometer { get; set; }

        public double TravelledDistance { get; set; }

        public void CalculatesIfDistanceIsPossible(double distance)
        {
            double totalFuelNeeded = FuelConsumptionPerKilometer * distance;

            if (FuelAmount >= totalFuelNeeded)
            {
                FuelAmount -= totalFuelNeeded;
                TravelledDistance += distance;
            }
            else
            {
                Console.WriteLine("Insufficient fuel for the drive") ;
            }
        }
    }
}
