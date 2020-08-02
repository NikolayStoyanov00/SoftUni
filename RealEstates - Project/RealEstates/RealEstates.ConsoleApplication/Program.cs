using Microsoft.EntityFrameworkCore;
using RealEstates.Data;
using RealEstates.Services.Interfaces;
using RealEstates.Services.Services;
using System;
using System.Text;

namespace RealEstates.ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            var db = new RealEstateDbContext();
            db.Database.Migrate();

            IPropertiesService propertiesService = new PropertiesService(db);
            propertiesService.Create("Младост", 120, 2020, 100000, "4-СТАЕН", "Панел", 5, 6);

            IDistrictsService districtsService = new DistrictsService(db);
            var districts = districtsService.GetTopDistrictsByAveragePrice();

            foreach (var district in districts)
            {
                Console.WriteLine($"{district.Name} => Price: {district.AveragePrice:f2} ({district.MinPrice:f2}-{district.MaxPrice:f2}) => {district.PropertiesCount} properties");
            }
        }
    }
}
