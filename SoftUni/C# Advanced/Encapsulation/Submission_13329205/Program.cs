using System;

namespace Vehicles
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] carParams = Console.ReadLine()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries);

            double carFuelQuantity = double.Parse(carParams[1]);
            double carFuelConsumptionPerKm = double.Parse(carParams[2]);
            double carTankCapacity = double.Parse(carParams[3]);

            IVehicle car = new Car(carFuelQuantity, carFuelConsumptionPerKm, carTankCapacity);

            string[] truckParams = Console.ReadLine()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries);

            double truckFuelQuantity = double.Parse(truckParams[1]);
            double truckFuelConsumptionPerKm = double.Parse(truckParams[2]);
            double truckTankCapacity = double.Parse(truckParams[3]);

            IVehicle truck = new Truck(truckFuelQuantity, truckFuelConsumptionPerKm, truckTankCapacity);

            string[] busParams = Console.ReadLine()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries);

            double busFuelQuantity = double.Parse(busParams[1]);
            double busFuelConsumptionPerKm = double.Parse(busParams[2]);
            double busTankCapacity = double.Parse(busParams[3]);

            Bus bus = new Bus(busFuelQuantity, busFuelConsumptionPerKm, busTankCapacity);

            int n = int.Parse(Console.ReadLine());

            for (int i = 0; i < n; i++)
            {
                string[] command = Console.ReadLine()
                    .Split(" ", StringSplitOptions.RemoveEmptyEntries);

                if (command[0] == "Drive")
                {
                    if (command[1] == "Car")
                    {
                        car.Drive(double.Parse(command[2]));
                    }
                    else if (command[1] == "Truck")
                    {
                        truck.Drive(double.Parse(command[2]));
                    }
                    else if (command[1] == "Bus")
                    {
                        bus.Drive(double.Parse(command[2]));
                    }
                }
                else if (command[0] == "DriveEmpty")
                {
                    if (command[1] == "Bus")
                    {
                        bus.DriveEmpty(double.Parse(command[2]));
                    }
                }

                else if (command[0] == "Refuel")
                {
                    if (command[1] == "Car")
                    {
                        car.Refuel(double.Parse(command[2]));
                    }
                    else if (command[1] == "Truck")
                    {
                        truck.Refuel(double.Parse(command[2]));
                    }
                    else if (command[1] == "Bus")
                    {
                        bus.Refuel(double.Parse(command[2]));
                    }
                }
            }

            Console.WriteLine($"Car: {car.FuelQuantity:f2}");
            Console.WriteLine($"Truck: {truck.FuelQuantity:F2}");
            Console.WriteLine($"Bus: {bus.FuelQuantity:f2}");
        }
    }
}
