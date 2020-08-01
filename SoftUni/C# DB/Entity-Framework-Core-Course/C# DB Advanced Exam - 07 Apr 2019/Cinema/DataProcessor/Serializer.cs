namespace Cinema.DataProcessor
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Cryptography.X509Certificates;
    using Cinema.Data.Models;
    using Cinema.DataProcessor.ExportDto;
    using Cinema.XmlHelper;
    using Data;
    using Newtonsoft.Json;

    public class Serializer
    {
        public static string ExportTopMovies(CinemaContext context, int rating)
        {
            var movies = context
                .Movies
                .Where(x => x.Rating >= rating && x.Projections.Any(x => x.Tickets.Count >= 1))
                .OrderByDescending(r => r.Rating)
                .ThenByDescending(p => p.Projections.Sum(t => t.Tickets.Sum(pc => pc.Price)))
                .Select(x => new 
                {
                    MovieName = x.Title,
                    Rating = x.Rating.ToString("F2"),
                    TotalIncomes = x.Projections.Sum(x => x.Tickets.Sum(t => t.Price)).ToString("F2"),
                    Customers = x.Projections.SelectMany(t => t.Tickets).Select(x => new 
                    { 
                        FirstName = x.Customer.FirstName,
                        LastName = x.Customer.LastName,
                        Balance = x.Customer.Balance.ToString("f2"),

                    })
                        .OrderByDescending(b => b.Balance)
                        .ThenBy(f => f.FirstName)
                        .ThenBy(l => l.LastName)
                        .ToArray()
                })
                .Take(10)
                .ToList();

            var jsonResult = JsonConvert.SerializeObject(movies, Formatting.Indented);
            return jsonResult;
        }

        public static string ExportTopCustomers(CinemaContext context, int age)
        {
            const string rootName = "Customers";

            var customers = context
                .Customers
                .Where(c => c.Age >= age)
                .OrderByDescending(x => x.Tickets.Sum(p => p.Price))
                .Take(10)
                .Select(c => new ExportCustomerDto
                {
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    SpentMoney = c.Tickets.Sum(t => t.Price).ToString("F2"),
                    SpentTime = new TimeSpan(c.Tickets.Sum(t => t.Projection.Movie.Duration.Ticks)).ToString(@"hh\:mm\:ss")
                })
                .ToArray();

            var xmlOutput = XmlConverter.Serialize<ExportCustomerDto[]>(customers, rootName);
            return xmlOutput.TrimEnd();
        }
    }
}