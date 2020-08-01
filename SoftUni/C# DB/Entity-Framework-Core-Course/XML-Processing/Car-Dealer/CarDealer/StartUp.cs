using CarDealer.Data;
using CarDealer.Dtos.Export;
using CarDealer.Dtos.Import;
using CarDealer.Models;
using CarDealer.XmlHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CarDealer
{
    public class StartUp
    {
        const string path = @"../../../Results";
        public static void Main(string[] args)
        {
            CarDealerContext context = new CarDealerContext();

            if (!Directory.Exists(path))
            {
                DirectoryInfo di = Directory.CreateDirectory(path);
            }

            /*09. Import Suppliers
            string directory = File.ReadAllText(@"../../../Datasets/suppliers.xml");
            string result = ImportSuppliers(context, directory);
            Console.WriteLine(result);*/

            /*10. Import Parts
            //string directory = File.ReadAllText(@"../../../Datasets/parts.xml");
            //string result = ImportParts(context, directory);
            //Console.WriteLine(result);*/

            /*11.Import Cars
            string directory = File.ReadAllText(@"../../../Datasets/cars.xml");
            string result = ImportCars(context, directory);
            Console.WriteLine(result);*/

            /*12.Import Customers
            string directory = File.ReadAllText(@"../../../Datasets/customers.xml");
            string result = ImportCustomers(context, directory);
            Console.WriteLine(result);*/

            /*13.Import Sales
            string directory = File.ReadAllText(@"../../../Datasets/sales.xml");
            string result = ImportSales(context, directory);
            Console.WriteLine(result);*/

            /* 14. Export Cars With Distance
            var resultXml = GetCarsWithDistance(context);
            File.WriteAllText(path + "/cars.xml", resultXml);*/

            /* 15. Export Cars From Make BMW
            var resultXml = GetCarsFromMakeBmw(context);
            File.WriteAllText(path + "/bmw-cars.xml", resultXml);*/

            /*16. Export Local Suppliers
            var resultXml = GetLocalSuppliers(context);
            File.WriteAllText(path + "/local-suppliers.xml", resultXml); */

            /*17.Export Cars With Their List Of Parts
            var resultXml = GetCarsWithTheirListOfParts(context);
            File.WriteAllText(path + "/cars-and-parts.xml", resultXml);*/

            /*18. Export Total Sales By Customer
            var resultXml = GetTotalSalesByCustomer(context);
            File.WriteAllText(path + "/customers-total-sales.xml", resultXml);*/

            //19. Export Sales With Applied Discount
            var resultXml = GetSalesWithAppliedDiscount(context);
            File.WriteAllText(path + "/sales-discounts.xml", resultXml);
        }

        private static void ResetDatabase(CarDealerContext db)
        {
            db.Database.EnsureDeleted();
            Console.WriteLine("Database was successfully deleted");

            db.Database.EnsureCreated();
            Console.WriteLine("Database was successfully created");
        }

        //09. Import Suppliers
        public static string ImportSuppliers(CarDealerContext context, string inputXml)
        {
            const string rootName = "Suppliers";
            var suppliersResult = XmlConverter.Deserializer<ImportSupplierDto>(inputXml, rootName);

            var suppliers = suppliersResult
                .Select(s => new Supplier
                {
                    Name = s.Name,
                    IsImporter = s.IsImporter
                })
                .ToList();

            context.AddRange(suppliers);
            context.SaveChanges();

            return $"Successfully imported {suppliers.Count}";
        }

        //10. Import Parts
        public static string ImportParts(CarDealerContext context, string inputXml)
        {
            const string rootName = "Parts";

            var partsResult = XmlConverter.Deserializer<ImportPartDto>(inputXml, rootName);

            var parts = partsResult
                .Select(p => new Part
                {
                    Name = p.Name,
                    Price = p.Price,
                    Quantity = p.Quantity,
                    SupplierId = context.Suppliers.Any(s => s.Id == p.SupplierId) ? p.SupplierId : 0
                })
                .Where(p => p.SupplierId != 0)
                .ToList();

            context.AddRange(parts);
            context.SaveChanges();

            return $"Successfully imported {parts.Count}";
        }

        //11.Import Cars
        public static string ImportCars(CarDealerContext context, string inputXml)
        {
            const string rootName = "Cars";

            var cars = new List<Car>();
            var carParts = new List<PartCar>();

            var resultCars = XmlConverter.Deserializer<ImportCarDto>(inputXml, rootName);

            foreach (var car in resultCars)
            {
                var carPartsIds = car.CarParts
                    .Select(x => x.PartId)
                    .Distinct()
                    .ToList();

                var newCar = new Car()
                {
                    Make = car.Make,
                    Model = car.Model,
                    TravelledDistance = car.TravelledDistance
                };

                cars.Add(newCar);

                foreach (var carPartId in carPartsIds)
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

            return $"Successfully imported {cars.Count}";
        }

        //12. Import Customers
        public static string ImportCustomers(CarDealerContext context, string inputXml)
        {
            const string rootName = "Customers";
            var customersResult = XmlConverter.Deserializer<ImportCustomerDto>(inputXml, rootName);

            var customers = customersResult
                .Select(x => new Customer
                {
                    Name = x.Name,
                    BirthDate = x.BirthDate,
                    IsYoungDriver = x.IsYoungDriver
                })
                .ToList();

            context.AddRange(customers);
            context.SaveChanges();

            return $"Successfully imported {customers.Count}";
        }

        //13. Import Sales
        public static string ImportSales(CarDealerContext context, string inputXml)
        {
            const string rootName = "Sales";

            var salesResult = XmlConverter.Deserializer<ImportSaleDto>(inputXml, rootName);

            var sales = salesResult
                .Where(s => context.Cars.Any(x => x.Id == s.CarId))
                .Select(x => new Sale()
                {
                    CarId = x.CarId,
                    CustomerId = x.CustomerId,
                    Discount = x.Discount
                })
                .ToList();

            context.Sales.AddRange(sales);
            context.SaveChanges();
            return $"Successfully imported {sales.Count}";
        }

        //14. Export Cars With Distance
        public static string GetCarsWithDistance(CarDealerContext context)
        {
            const string rootName = "cars";

            var cars = context
                .Cars
                .Select(x => new ExportCarDto
                {
                    Make = x.Make,
                    Model = x.Model,
                    TravelledDistance = x.TravelledDistance
                })
                .Where(x => x.TravelledDistance > 2000000)
                .OrderBy(x => x.Make)
                .ThenBy(x => x.Model)
                .Take(10)
                .ToList();

            var resultXml = XmlConverter.Serialize(cars, rootName);

            return resultXml;
        }

        //15. Export Cars From Make BMW
        public static string GetCarsFromMakeBmw(CarDealerContext context)
        {
            const string rootName = "cars";

            var cars = context
                .Cars
                .Where(x => x.Make == "BMW")
                .Select(x => new ExportBmwCarDto
                {
                    Id = x.Id,
                    Model = x.Model,
                    TravelledDistance = x.TravelledDistance
                })
                .OrderBy(x => x.Model)
                .ThenByDescending(x => x.TravelledDistance)
                .ToList();

            var resultXml = XmlConverter.Serialize(cars, rootName);
            return resultXml;
        }

        //16. Export Local Suppliers
        public static string GetLocalSuppliers(CarDealerContext context)
        {
            const string rootName = "suppliers";

            var suppliers = context
                .Suppliers
                .Where(s => s.IsImporter == false)
                .Select(s => new ExportSupplierDto
                {
                    Id = s.Id,
                    Name = s.Name,
                    PartsCount = s.Parts.Count
                })
                .ToList();

            var resultXml = XmlConverter.Serialize(suppliers, rootName);
            return resultXml;
        }

        //17.Export Cars With Their List Of Parts
        public static string GetCarsWithTheirListOfParts(CarDealerContext context)
        {
            const string rootName = "cars";

            var cars = context
                .Cars
                .Select(c => new ExportCarWithPartsDto
                {
                    Make = c.Make,
                    Model = c.Model,
                    TravelledDistance = c.TravelledDistance,
                    Parts = c.PartCars.Select(p => new PartCarDto
                    {
                        Name = p.Part.Name,
                        Price = p.Part.Price
                    })
                    .OrderByDescending(p => p.Price)
                    .ToList()
                })
                .OrderByDescending(c => c.TravelledDistance)
                .ThenBy(c => c.Model)
                .Take(5)
                .ToList();

            var resultXml = XmlConverter.Serialize(cars, rootName);
            return resultXml;
        }

        //18. Export Total Sales By Customer
        public static string GetTotalSalesByCustomer(CarDealerContext context)
        {
            const string rootName = "customers";

            var customers = context
                .Sales
                .Where(x => x.Customer.Sales.Any())
                .Select(x => new ExportCustomerWithSalesDto
                {
                    FullName = x.Customer.Name,
                    BoughtCars = x.Customer.Sales.Count,
                    SpentMoney = x.Car.PartCars.Sum(p => p.Part.Price)
                })
                .OrderByDescending(x => x.SpentMoney)
                .ToList();

            var resultXml = XmlConverter.Serialize(customers, rootName);
            return resultXml;
        }

        //19. Export Sales With Applied Discount
        public static string GetSalesWithAppliedDiscount(CarDealerContext context)
        {
            const string rootName = "sales";

            var sales = context
                .Sales
                .Select(x => new ExportSaleWithDiscountDto
                {
                    Car = new SaleCarDto
                    {
                        Make = x.Car.Make,
                        Model = x.Car.Model,
                        TravelledDistance = x.Car.TravelledDistance
                    },
                    Discount = x.Discount,
                    CustomerName = x.Customer.Name,
                    Price = x.Car.PartCars.Sum(pc => pc.Part.Price),
                    PriceWithDiscount = x.Car.PartCars.Sum(pc => pc.Part.Price) - 
                    x.Car.PartCars.Sum(pc => pc.Part.Price) * x.Discount / 100.0M
                })
                .ToList();

            var resultXml = XmlConverter.Serialize(sales, rootName);
            return resultXml;
        }
    }
}