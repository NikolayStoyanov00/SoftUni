using System;
using System.Collections.Generic;
using System.Linq;

namespace HW8._Car_Salesman
{
    public class Program
    {
        public static void Main(string[] args)
        {
            int linesOfEngines = int.Parse(Console.ReadLine());
            List<Car> cars = new List<Car>();
            List<Engine> engines = new List<Engine>();

            for (int i = 0; i < linesOfEngines; i++)
            {
                string[] engineInfo = Console.ReadLine()
                    .Split(" ", StringSplitOptions.RemoveEmptyEntries);

                Engine engine = FillsEngineInfo(engineInfo);
                engines.Add(engine);
            }

            int linesOfCars = int.Parse(Console.ReadLine());

            for (int i = 0; i < linesOfCars; i++)
            {
                string[] carParams = Console.ReadLine()
                    .Split(" ", StringSplitOptions.RemoveEmptyEntries);

                Car car = FillsCarInfo(carParams, engines);
                cars.Add(car);
            }

            PrintsCars(cars);
        }

        private static void PrintsCars(List<Car> cars)
        {
            foreach (var car in cars)
            {
                Console.WriteLine($"{car.Model}:");
                Console.WriteLine($"  {car.Engine.Model}:");
                Console.WriteLine($"   Power: {car.Engine.Power}");
                Console.Write($"   Displacement: ");

                if (car.Engine.Displacement != 0)
                {
                    Console.WriteLine(car.Engine.Displacement);
                }
                else
                {
                    Console.WriteLine("n/a");
                }

                Console.Write($"   Efficiency: ");

                if (!string.IsNullOrEmpty(car.Engine.Efficiency))
                {
                    Console.WriteLine(car.Engine.Efficiency);
                }
                else
                {
                    Console.WriteLine("n/a");
                }

                Console.Write($"  Weight: ");

                if (car.Weight != 0)
                {
                    Console.WriteLine(car.Weight);
                }
                else
                {
                    Console.WriteLine("n/a");
                }

                Console.Write("  Color: ");

                if (!string.IsNullOrEmpty(car.Color))
                {
                    Console.WriteLine(car.Color);
                }
                else
                {
                    Console.WriteLine("n/a");
                }
            }
        }

        private static Car FillsCarInfo(string[] carParams, List<Engine> engines)
        {
            string model = carParams[0];
            string engineName = carParams[1];
            var engineWithSameName = engines.Where(x => x.Model == engineName).ToList();
            Engine engine = engineWithSameName.FirstOrDefault();
            Car car = new Car(model, engine);

            int weight = 0;

            if (carParams.Length >= 3)
            {
                bool isNum = int.TryParse(carParams[2], out weight);

                if (isNum == true)
                {
                    car = new Car(model, engine, weight);
                }
                else
                {
                    car = new Car(model, engine, carParams[2]);
                }
            }

            if (carParams.Length >= 4)
            {
                string color = carParams[3];
                car = new Car(model, engine, weight, color);
            }

            return car;
        }

        private static Engine FillsEngineInfo(string[] engineInfo)
        {
            string model = engineInfo[0];
            int power = int.Parse(engineInfo[1]);
            Engine engine = new Engine(model, power);
            int displacement = 0;
            string efficiency = string.Empty;
            if (engineInfo.Length >= 3)
            {
                bool isNum = int.TryParse(engineInfo[2], out displacement);

                if (isNum == true)
                {
                    engine = new Engine(model, power, displacement);
                }
                else
                {
                    engine = new Engine(model, power, engineInfo[2]);
                }
            }
            if (engineInfo.Length >= 4)
            {
                efficiency = engineInfo[3];

                if (efficiency.Length != 0)
                {
                    engine = new Engine(model, power, displacement, efficiency);
                }
            }
            return engine;
        }
    }
}
