using System;
using System.Collections.Generic;
using System.Linq;

namespace HW07._Raw_Data
{
    public class Program
    {
        public static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            List<Car> cars = new List<Car>();

            for (int i = 0; i < n; i++)
            {
                string[] carParams = Console.ReadLine()
                    .Split(" ", StringSplitOptions.RemoveEmptyEntries);

                string model = carParams[0];
                int engineSpeed = int.Parse(carParams[1]);
                int enginePower = int.Parse(carParams[2]);
                int cargoWeight = int.Parse(carParams[3]);
                string cargoType = carParams[4];

                Tire[] tires = new Tire[4];

                int tireCounter = 0;
                for (int j = 0; j < 7; j+=2)
                {
                    double pressure = double.Parse(carParams[5 + j]);
                    int age = int.Parse(carParams[5 + j + 1]);

                    tires[tireCounter] = new Tire(pressure, age);
                    tireCounter++;
                }

                Engine engine = new Engine(engineSpeed, enginePower);
                Cargo cargo = new Cargo(cargoWeight, cargoType);

                Car car = new Car(model, engine, cargo, tires);

                cars.Add(car);
            }

            string type = Console.ReadLine();

            if (type == "fragile")
            {
                List<Car> fragileCars = cars
                    .Where(x => x.Tires.Any(t => t.Pressure < 1))
                    .Where(x => x.Cargo.CargoType == "fragile").ToList();
                foreach (var fragileCar in fragileCars)
                {
                    Console.WriteLine(fragileCar.Model);
                }
            }
            else if (type == "flamable")
            {
                List<Car> flamableCars = cars
                    .Where(x => x.Engine.EnginePower > 250)
                    .Where(x => x.Cargo.CargoType == "flamable").ToList();

                foreach (var flamableCar in flamableCars)
                {
                    Console.WriteLine(flamableCar.Model);
                }
            }
        }
    }
}
