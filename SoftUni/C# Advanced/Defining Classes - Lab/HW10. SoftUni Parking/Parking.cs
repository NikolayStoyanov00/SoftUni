using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoftUniParking
{
    public class Parking
    {
        private int capacity;
        public List<Car> Cars { get; set; }
        public Parking(int capacity)
        {
            this.capacity = capacity;
            this.Cars = new List<Car>();
        }

        public int Count => this.Cars.Count;
        public string AddCar(Car car)
        {
            if (this.Cars.Any(x => x.RegistrationNumber == car.RegistrationNumber))
            {
                return $"Car with that registration number, already exists!"; 
            }

            else if (this.Cars.Count >= capacity)
            {
                return "Parking is full!";
            }
            else
            {
                Cars.Add(car);
                return $"Successfully added new car {car.Make} {car.RegistrationNumber}";
            }

        }

        public string RemoveCar(string registrationNumber)
        {
            if (!this.Cars.Any(x => x.RegistrationNumber == registrationNumber))
            {
                return "Car with that registration number, doesn't exist!";
            }

            else
            {
                this.Cars.Remove(this.Cars.FirstOrDefault(x => x.RegistrationNumber == registrationNumber));
                return $"Successfully removed {registrationNumber}";
            }
        }

        public Car GetCar(string registrationNumber)
        {
            return this.Cars.FirstOrDefault(x => x.RegistrationNumber == registrationNumber);
        }

        public void RemoveSetOfRegistrationNumber(List<string> registrationNumbers)
        {
            foreach (var currentNumber in registrationNumbers)
            {
                this.Cars.RemoveAll(x => x.RegistrationNumber == currentNumber);
            }
        }

    }
}
