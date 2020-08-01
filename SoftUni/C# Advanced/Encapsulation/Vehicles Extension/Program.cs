//////////////////////////////// MAIN
using System;


class Program
{
    static void Main(string[] args)
    {
        string[] carInfo = Console.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        string[] truckInfo = Console.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        string[] busInfo = Console.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

        Car car = (Car)CreateVehicle(carInfo);
        Truck truck = (Truck)CreateVehicle(truckInfo);
        Bus bus = (Bus)CreateVehicle(busInfo);

        int n = int.Parse(Console.ReadLine());

        for (int i = 0; i < n; i++)
        {
            string[] command = Console.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            string toDo = command[0];
            string type = command[1];

            try
            {
                switch (toDo)
                {
                    case "Drive":
                        double distance = double.Parse(command[2]);
                        if (type == "Car")
                        {
                            car.Drive(distance);
                        }
                        else if (type == "Truck")
                        {
                            truck.Drive(distance);
                        }
                        else if (type == "Bus")
                        {
                            bus.Drive(distance);
                        }
                        break;
                    case "Refuel":
                        double liters = double.Parse(command[2]);
                        if (type == "Car")
                        {
                            car.Refuel(liters);
                        }
                        else if (type == "Truck")
                        {
                            truck.Refuel(liters);
                        }
                        else if (type == "Bus")
                        {
                            bus.Refuel(liters);
                        }
                        break;
                    case "DriveEmpty":
                        double busDistance = double.Parse(command[2]);
                        bus.DriveEmpty(busDistance);
                        break;
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        Console.WriteLine($"Car: {car.FuelQuantity:F2}");
        Console.WriteLine($"Truck: {truck.FuelQuantity:F2}");
        Console.WriteLine($"Bus: {bus.FuelQuantity:F2}");
    }

    public static Vehicle CreateVehicle(string[] vehicleInfo)
    {
        double initialFuelQuantity = double.Parse(vehicleInfo[1]);
        double litersPerKilometer = double.Parse(vehicleInfo[2]);
        double tankCapacity = double.Parse(vehicleInfo[3]);

        string type = vehicleInfo[0];

        switch (type)
        {
            case "Car":
                Car car = new Car(initialFuelQuantity, litersPerKilometer, tankCapacity);
                return car;
            case "Truck":
                Truck truck = new Truck(initialFuelQuantity, litersPerKilometer, tankCapacity);
                return truck;
            case "Bus":
                Bus bus = new Bus(initialFuelQuantity, litersPerKilometer, tankCapacity);
                return bus;
            default:
                throw new Exception();
        }
    }
}
 

public class Bus : Vehicle
{
    public Bus(double fuel, double consumption, double tankCapacity)
    {
        this.FuelQuantity = fuel > tankCapacity ? 0 : fuel;
        this.ConsumprionPerKilometer = consumption;
        this.TankCapacity = tankCapacity;
    }

    public override void Drive(double distance)
    {
        double consumedFuel = distance * (this.ConsumprionPerKilometer + 1.4);
        if (this.FuelQuantity - consumedFuel >= 0)
        {
            Console.WriteLine($"Bus travelled {distance} km");
            this.FuelQuantity -= consumedFuel;
        }
        else
        {
            Console.WriteLine("Bus needs refueling");
        }
    }

    public void DriveEmpty(double distance)
    {
        double consumedFuel = distance * this.ConsumprionPerKilometer;
        if (this.FuelQuantity - consumedFuel >= 0)
        {
            Console.WriteLine($"Bus travelled {distance} km");
            this.FuelQuantity -= consumedFuel;
        }
        else
        {
            Console.WriteLine("Bus needs refueling");
        }
    }
    public override void Refuel(double liters)
    {
        if (liters <= 0)
        {
            throw new ArgumentException("Fuel must be a positive number");
        }
        else if (this.FuelQuantity + liters > this.TankCapacity)
        {
            throw new ArgumentException($"Cannot fit {liters} fuel in the tank");
        }
        this.FuelQuantity += liters;
    }
}

public class Car : Vehicle
{
    private const double CAR_CONSUMPTION_DURING_SUMMER = 0.9;

    public Car(double fuel, double consumption, double tankCapacity)
    {
        this.FuelQuantity = fuel > tankCapacity ? 0 : fuel;
        this.ConsumprionPerKilometer = consumption + CAR_CONSUMPTION_DURING_SUMMER;
        this.TankCapacity = tankCapacity;
    }

    public override void Drive(double distance)
    {
        double consumedFuel = distance * this.ConsumprionPerKilometer;
        if (this.FuelQuantity - consumedFuel >= 0)
        {
            Console.WriteLine($"Car travelled {distance} km");
            this.FuelQuantity -= consumedFuel;
        }
        else
        {
            Console.WriteLine("Car needs refueling");
        }
    }

    public override void Refuel(double liters)
    {
        if (liters <= 0)
        {
            throw new ArgumentException("Fuel must be a positive number");
        }
        else if (this.FuelQuantity + liters > this.TankCapacity)
        {
            throw new ArgumentException($"Cannot fit {liters} fuel in the tank");
        }
        this.FuelQuantity += liters;
    }
}

///////////////////////////////// TRUCK



public class Truck : Vehicle
{
    private const double TRUCK_CONSUMPTION_DURING_SUMMER = 1.6;

    public Truck(double fuel, double consumption, double tankCapacity)
    {
        this.FuelQuantity = fuel > tankCapacity ? 0 : fuel;
        this.ConsumprionPerKilometer = consumption + TRUCK_CONSUMPTION_DURING_SUMMER;
        this.TankCapacity = tankCapacity;
    }

    public override void Drive(double distance)
    {
        double consumedFuel = distance * this.ConsumprionPerKilometer;
        if (this.FuelQuantity - consumedFuel >= 0)
        {
            Console.WriteLine($"Truck travelled {distance} km");
            this.FuelQuantity -= consumedFuel;
        }
        else
        {
            Console.WriteLine("Truck needs refueling");
        }
    }

    public override void Refuel(double liters)
    {
        if (liters <= 0)
        {
            throw new ArgumentException("Fuel must be a positive number");
        }
        else if (this.FuelQuantity + liters > this.TankCapacity)
        {
            throw new ArgumentException($"Cannot fit {liters} fuel in the tank");
        }
        double fuelToAdd = (liters * 95) / 100.0;
        this.FuelQuantity += fuelToAdd;
    }
}



public abstract class Vehicle
{
    private double fuelQuantity;
    private double consumprionPerKilometer;
    private double tankCapacity;

    public double FuelQuantity
    {
        get { return fuelQuantity; }
        set { fuelQuantity = value; }
    }

    public double ConsumprionPerKilometer
    {
        get { return consumprionPerKilometer; }
        set { consumprionPerKilometer = value; }
    }

    public double TankCapacity
    {
        get { return tankCapacity; }
        set
        { tankCapacity = value; }
    }


    public abstract void Drive(double distance);
    public abstract void Refuel(double liters);
}