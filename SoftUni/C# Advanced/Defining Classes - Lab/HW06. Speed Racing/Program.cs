using System;
using System.Collections.Generic;
using System.Linq;

namespace HW6._Speed_Racing
{
    class Program
    {
        static void Main(string[] args)
        {
            int numberOfCars = int.Parse(Console.ReadLine());
            List<Car> cars = new List<Car>();

            for (int i = 0; i < numberOfCars; i++)
            {
                string[] carParams = Console.ReadLine()
                    .Split(" ", StringSplitOptions.RemoveEmptyEntries);

                string model = carParams[0];
                double fuelAmount = double.Parse(carParams[1]);
                double fuelConsumptionFor1km = double.Parse(carParams[2]);

                Car car = new Car();

                car.Model = model;
                car.FuelAmount = fuelAmount;
                car.FuelConsumptionPerKilometer = fuelConsumptionFor1km;
                car.TravelledDistance = 0;

                cars.Add(car);
            }

            while (true)
            {
                string command = Console.ReadLine();

                if (command == "End")
                {
                    break;
                }

                string[] tokens = command.Split(" ", StringSplitOptions.RemoveEmptyEntries);

                if (tokens[0] == "Drive")
                {
                    string carModel = tokens[1];
                    double amountOfKm = double.Parse(tokens[2]);

                    var car = cars.Where(x => x.Model == carModel);

                    car.FirstOrDefault().CalculatesIfDistanceIsPossible(amountOfKm);
                }
            }

            foreach (var car in cars)
            {
                Console.WriteLine($"{car.Model} {car.FuelAmount:f2} {car.TravelledDistance}");
            }
        }
    }
}
