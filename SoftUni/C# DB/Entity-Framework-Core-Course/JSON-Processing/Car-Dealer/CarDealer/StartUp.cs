using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using AutoMapper;
using CarDealer.Data;
using CarDealer.DTO;
using CarDealer.Models;
using Microsoft.SqlServer.Server;
using Newtonsoft.Json;

namespace CarDealer
{
    public class StartUp
    {
        private static string ResultDirectoryPath = @"../../../Datasets/Results";
        public static void Main(string[] args)
        {
            CarDealerContext db = new CarDealerContext();

            string inputJson = File.ReadAllText(@"../../../Datasets/sales.json");

            string result = GetSalesWithAppliedDiscount(db);

            EnsureDirectoryExists(ResultDirectoryPath);

            File.WriteAllText(ResultDirectoryPath + "/sales-discounts.json", result);
        }

        private static void EnsureDirectoryExists(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        public static void ResetDatabase(CarDealerContext context)
        {
            context.Database.EnsureDeleted();
            Console.WriteLine("Database was successfully deleted");

            context.Database.EnsureCreated();
            Console.WriteLine("Database was successfully created");
        }

        //09. Import Suppliers
        public static string ImportSuppliers(CarDealerContext context, string inputJson)
        {
            List<Supplier> suppliers = JsonConvert.DeserializeObject<List<Supplier>>(inputJson);
            context.Suppliers.AddRange(suppliers);
            context.SaveChanges();

            return $"Successfully imported {suppliers.Count}.";
        }

        //10. Import Parts
        public static string ImportParts(CarDealerContext context, string inputJson)
        {
            int supplierCount = context.Suppliers.Count();

            List<Part> parts = JsonConvert
                .DeserializeObject<List<Part>>(inputJson).Where(x => x.SupplierId <= supplierCount).ToList();

            context.Parts.AddRange(parts);
            context.SaveChanges();

            return $"Successfully imported {parts.Count}.";
        }

        //11. Import Cars
        public static string ImportCars(CarDealerContext context, string inputJson)
        {
            var carsDto = JsonConvert.DeserializeObject<List<CarDTO>>(inputJson);
            var cars = new List<Car>();
            var carParts = new List<PartCar>();

            foreach (var carDto in carsDto)
            {
                var newCar = new Car()
                {
                    Make = carDto.Make,
                    Model = carDto.Model,
                    TravelledDistance = carDto.TravelledDistance,

                };
                cars.Add(newCar);

                foreach (var carPartId in carDto.PartsId.Distinct())
                {
                    var newCarPart = new PartCar()
                    {
                        PartId = carPartId,
                        Car = newCar
                    };
                    carParts.Add(newCarPart);
                }
            }

            context.Cars.AddRange(cars);
            context.PartCars.AddRange(carParts);
            context.SaveChanges();

            return $"Successfully imported {cars.Count}.";
        }

        //12. Import Customers
        public static string ImportCustomers(CarDealerContext context, string inputJson)
        {
            var customers = JsonConvert.DeserializeObject<List<Customer>>(inputJson);

            context.Customers.AddRange(customers);
            context.SaveChanges();

            return $"Successfully imported {customers.Count}.";
        }

        //13. Import Sales
        public static string ImportSales(CarDealerContext context, string inputJson)
        {
            var sales = JsonConvert.DeserializeObject<List<Sale>>(inputJson, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

            context.Sales.AddRange(sales);
            context.SaveChanges();

            return $"Successfully imported {sales.Count}.";
        }

        //14. Export Ordered Customers
        public static string GetOrderedCustomers(CarDealerContext context)
        {
            var customers = context
                .Customers
                .OrderBy(c => c.BirthDate)
                .ThenBy(c => c.IsYoungDriver)
                .Select(c => new
                {
                    c.Name,
                    BirthDate = c.BirthDate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture),
                    c.IsYoungDriver
                })
                .ToList();

            string json = JsonConvert.SerializeObject(customers, Formatting.Indented);
            return json;
        }

        //15. Export Cars From Make Toyota
        public static string GetCarsFromMakeToyota(CarDealerContext context)
        {
            var cars = context
                .Cars
                .Where(c => c.Make.ToLower() == "toyota")
                .OrderBy(c => c.Make)
                .ThenByDescending(c => c.TravelledDistance)
                .Select(c => new
                {
                    c.Id,
                    c.Make,
                    c.Model,
                    c.TravelledDistance
                })
                .ToList();

            string json = JsonConvert.SerializeObject(cars, Formatting.Indented);
            return json;
        }

        //16. Export Local Suppliers
        public static string GetLocalSuppliers(CarDealerContext context)
        {
            var suppliers = context
                .Suppliers
                .Where(x => x.IsImporter == false)
                .Select(x => new
                {
                    x.Id,
                    x.Name,
                    PartsCount = x.Parts.Count
                })
                .ToList();

            string json = JsonConvert.SerializeObject(suppliers, Formatting.Indented);
            return json;
        }

        //17. Export Cars With Their List Of Parts
        public static string GetCarsWithTheirListOfParts(CarDealerContext context)
        {
            var carsWithParts = context
                .Cars
                .Select(cp => new
                {
                    car = new
                    {
                        cp.Make,
                        cp.Model,
                        cp.TravelledDistance
                    },
                    parts = cp.PartCars.Select(x => new
                    {
                        Name = x.Part.Name,
                        Price = x.Part.Price.ToString("F")
                    }).ToList()
                })
                .ToList();

            string json = JsonConvert.SerializeObject(carsWithParts, Formatting.Indented);
            return json;
        }

        //18. Export Total Sales By Customer
        public static string GetTotalSalesByCustomer(CarDealerContext context)
        {
            var customers = context
                .Customers
                .Where(c => c.Sales.Count > 0)
                .Select(c => new
                {
                    fullName = c.Name,
                    boughtCars = c.Sales.Count,
                    spentMoney = c.Sales.Sum(x => x.Car.PartCars.Sum(pc => pc.Part.Price))
                })
                .OrderByDescending(c => c.spentMoney)
                .ThenByDescending(c => c.boughtCars)
                .ToList();

            string json = JsonConvert.SerializeObject(customers, Formatting.Indented);
            return json;
        }

        //19. Export Sales With Applied Discount
        public static string GetSalesWithAppliedDiscount(CarDealerContext context)
        {
            var sales = context
                .Sales
                .Take(10)
                .Select(s => new
                {
                    car = new
                    {
                        Make = s.Car.Make,
                        Model = s.Car.Model,
                        TravelledDistance = s.Car.TravelledDistance
                    },
                    customerName = s.Customer.Name,
                    Discount = s.Discount.ToString("F2"),
                    price = s.Car.PartCars.Sum(pc => pc.Part.Price).ToString("F"),
                    priceWithDiscount = (s.Car.PartCars.Sum(pc => pc.Part.Price) -
                                        (s.Car.PartCars.Sum(pc => pc.Part.Price) * (s.Discount / 100))).ToString("F2")
                })
                .ToList();

            string json = JsonConvert.SerializeObject(sales, Formatting.Indented);
            return json;
        }
    }
}