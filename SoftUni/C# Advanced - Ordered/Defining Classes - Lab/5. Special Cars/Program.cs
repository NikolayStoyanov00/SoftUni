using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarManufacturer
{
    public class Car
    {
        private string make;
        private string model;
        private int year;
        private double fuelQuantity;
        private double fuelConsumption;
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public double FuelQuantity { get; set; }
        public double FuelConsumption { get; set; }

        private Engine engine;
        private Tire[] tires;

        public Engine Engine { get; set; }

        public Tire[] Tires { get; set; }

        public Car()
        {
            this.Make = "VW";
            this.Model = "Golf";
            this.Year = 2025;
            this.FuelQuantity = 200;
            this.FuelConsumption = 10;

        }

        public Car(string make, string model, int year)
            : this()
        {
            this.Make = make;
            this.Model = model;
            this.Year = year;
        }

        public Car(string make, string model, int year, double fuelQuantity, double fuelConsumption)
            : this(make, model, year)
        {
            this.FuelQuantity = fuelQuantity;
            this.FuelConsumption = fuelConsumption;
        }

        public Car(string make, string model, int year, double fuelQuantity, double fuelConsumption, Engine engine, Tire[] tires)
            : this(make, model, year, fuelQuantity, fuelConsumption)

        {
            this.Engine = engine;
            this.Tires = tires;
        }

        public void Drive(double distance)
        {
            if (FuelQuantity - distance * FuelConsumption / 100 > 0)
            {
                FuelQuantity -= distance * FuelConsumption / 100;
            }
            else
            {
                Console.WriteLine($"Not enough fuel to perform this trip!");
            }
        }

        public string WhoAmI()
        {
            var result = new StringBuilder();

            result.AppendLine($"Make: {this.Make}");
            result.AppendLine($"Model: {this.Model}");
            result.AppendLine($"Year: {this.Year}");
            result.Append($"Fuel: {this.FuelQuantity:F2}L");

            return result.ToString();
        }
    }

    public class Engine
    {
        private int horsePower;
        private double cubicCapacity;

        public int HorsePower { get; set; }
        public double CubicCapacity { get; set; }

        public Engine(int horsePower, double cubicCapacity)
        {
            this.HorsePower = horsePower;
            this.CubicCapacity = cubicCapacity;
        }
    }

    public class Tire
    {
        private int year;
        private double pressure;

        public int Year { get; set; }

        public double Pressure { get; set; }

        public Tire(int year, double pressure)
        {
            this.Year = year;
            this.Pressure = pressure;
        }
    }
    public class StartUp
    {
        static void Main(string[] args)
        {
            List<Tire[]> tires = new List<Tire[]>();
            List<Engine> engines = new List<Engine>();
            List<Car> cars = new List<Car>();

            while (true)
            {
                string command = Console.ReadLine();

                if (command == "No more tires")
                {
                    break;
                }

                string[] tiresParams = command.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                List<Tire> currentTires = new List<Tire>();

                for (int i = 0; i < tiresParams.Length - 1; i+=2)
                {
                    int tireYear = int.Parse(tiresParams[i]);
                    double tirePressure = double.Parse(tiresParams[i + 1]);

                    Tire tire = new Tire(tireYear, tirePressure);
                    currentTires.Add(tire);
                }

                tires.Add(currentTires.ToArray());
            }

            while (true)
            {
                string command = Console.ReadLine();

                if (command == "Engines done")
                {
                    break;
                }

                string[] engineInfo = command.Split(" ", StringSplitOptions.RemoveEmptyEntries);

                int horsePower = int.Parse(engineInfo[0]);
                double cubicCapacity = double.Parse(engineInfo[1]);

                Engine engine = new Engine(horsePower, cubicCapacity);
                engines.Add(engine);
            }

            while (true)
            {
                string command = Console.ReadLine();

                if (command == "Show special")
                {
                    break;
                }

                string[] carInfo = command.Split(" ", StringSplitOptions.RemoveEmptyEntries);

                string make = carInfo[0];
                string model = carInfo[1];
                int year = int.Parse(carInfo[2]);
                double fuelQuantity = double.Parse(carInfo[3]);
                double fuelConsumption = double.Parse(carInfo[4]);
                int engineIndex = int.Parse(carInfo[5]);
                int tiresIndex = int.Parse(carInfo[6]);

                Car car = new Car(make, model, year, fuelQuantity, fuelConsumption, engines[engineIndex], tires[tiresIndex]);

                cars.Add(car);
            }

            List<Car> specialCars = new List<Car>();

            foreach (Car car in cars)
            {
                double totalTirePressure = car.Tires.Sum(x => x.Pressure);
                if (car.Year >= 2017 && car.Engine.HorsePower >= 330 && totalTirePressure > 9 && totalTirePressure < 10)
                {
                    car.Drive(20);
                    specialCars.Add(car);
                }
            }

            foreach (var specialCar in specialCars)
            {
                Console.WriteLine($"Make: {specialCar.Make}");
                Console.WriteLine($"Model: {specialCar.Model}");
                Console.WriteLine($"Year: {specialCar.Year}");
                Console.WriteLine($"HorsePowers: {specialCar.Engine.HorsePower}");
                Console.WriteLine($"FuelQuantity: {specialCar.FuelQuantity}");
            }
        }
    }
}
