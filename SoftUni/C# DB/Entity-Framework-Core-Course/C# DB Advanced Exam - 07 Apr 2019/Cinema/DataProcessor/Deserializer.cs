namespace Cinema.DataProcessor
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using Cinema.Data.Models;
    using Cinema.Data.Models.Enums;
    using Cinema.DataProcessor.ImportDto;
    using Cinema.XmlHelper;
    using Data;
    using Microsoft.EntityFrameworkCore.Internal;
    using Newtonsoft.Json;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";
        private const string SuccessfulImportMovie 
            = "Successfully imported {0} with genre {1} and rating {2:F2}!";
        private const string SuccessfulImportHallSeat 
            = "Successfully imported {0}({1}) with {2} seats!";
        private const string SuccessfulImportProjection 
            = "Successfully imported projection {0} on {1}!";
        private const string SuccessfulImportCustomerTicket 
            = "Successfully imported customer {0} {1} with bought tickets: {2}!";

        public static string ImportMovies(CinemaContext context, string jsonString)
        {
            StringBuilder sb = new StringBuilder();

            var movieDtos = JsonConvert.DeserializeObject<ImportMovieDto[]>(jsonString);

            var movies = new List<Movie>();

            foreach (var movieDto in movieDtos)
            {
                if (context.Movies.Where(x => x.Title == movieDto.Title).Any())
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                if (IsValid(movieDto))
                {
                    var movie = new Movie
                    {
                        Title = movieDto.Title,
                        Genre = movieDto.Genre,
                        Duration = movieDto.Duration,
                        Rating = movieDto.Rating,
                        Director = movieDto.Director
                    };

                    sb.AppendLine(string.Format(SuccessfulImportMovie, movie.Title, movie.Genre, movie.Rating));
                    movies.Add(movie);
                }
                else
                {
                    sb.AppendLine(ErrorMessage);
                }
            }

            context.Movies.AddRange(movies);
            context.SaveChanges();
            return sb.ToString().TrimEnd();
        }

        public static string ImportHallSeats(CinemaContext context, string jsonString)
        {
            StringBuilder sb = new StringBuilder();

            var hallDtos = JsonConvert.DeserializeObject<ImportHallDto[]>(jsonString);

            var halls = new List<Hall>();

            foreach (var hallDto in hallDtos)
            {
                if (IsValid(hallDto))
                {
                    var hall = new Hall()
                    {
                        Name = hallDto.Name,
                        Is4Dx = hallDto.Is4Dx,
                        Is3D = hallDto.Is3D,
                    };

                    for (int i = 0; i < hallDto.Seats; i++)
                    {
                        Seat seat = new Seat
                        {
                            Hall = hall
                        };

                        hall.Seats.Add(seat);
                    }

                    if (hall.Is3D == true && hall.Is4Dx == true)
                    {
                        sb.AppendLine(string.Format(SuccessfulImportHallSeat, hall.Name, "4Dx/3D", hall.Seats.Count));
                    }
                    else if (hall.Is3D == true && hall.Is4Dx == false)
                    {
                        sb.AppendLine(string.Format(SuccessfulImportHallSeat, hall.Name, "3D", hall.Seats.Count));
                    }
                    else if (hall.Is4Dx == true && hall.Is3D == false)
                    {
                        sb.AppendLine(string.Format(SuccessfulImportHallSeat, hall.Name, "4Dx", hall.Seats.Count));
                    }
                    else
                    {
                        sb.AppendLine(string.Format(SuccessfulImportHallSeat, hall.Name, "Normal", hall.Seats.Count));
                    }

                    halls.Add(hall);
                }
                else
                {
                    sb.AppendLine(ErrorMessage);
                }
            }

            context.Halls.AddRange(halls);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static string ImportProjections(CinemaContext context, string xmlString)
        {
            StringBuilder sb = new StringBuilder();

            const string rootName = "Projections";

            var projectionDtos = XmlConverter.Deserializer<ImportProjectionDto>(xmlString, rootName);

            foreach (var projectionDto in projectionDtos)
            {
                if (!context.Movies.Where(m => m.Id == projectionDto.MovieId).Any())
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                if (!context.Halls.Where(h => h.Id == projectionDto.HallId).Any())
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var projection = new Projection
                {
                    MovieId = projectionDto.MovieId,
                    HallId = projectionDto.HallId,
                    DateTime = DateTime.Parse(projectionDto.DateTime, CultureInfo.InvariantCulture)
                };

                context.Projections.Add(projection);
                sb.AppendLine(string.Format(SuccessfulImportProjection, projection.Movie.Title, projection.DateTime.ToString("MM/dd/yyyy")));
            }

            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static string ImportCustomerTickets(CinemaContext context, string xmlString)
        {
            StringBuilder sb = new StringBuilder();

            const string rootName = "Customers";

            var customerDtos = XmlConverter.Deserializer<ImportCustomerDto>(xmlString, rootName);

            var customers = new List<Customer>();

            foreach (var customerDto in customerDtos)
            {
                if (IsValid(customerDto))
                {
                    var customer = new Customer()
                    {
                        FirstName = customerDto.FirstName,
                        LastName = customerDto.LastName,
                        Age = customerDto.Age,
                        Balance = customerDto.Balance,
                        Tickets = customerDto.Tickets.Select(x => new Ticket
                        {
                            ProjectionId = x.ProjectionId,
                            Price = x.Price
                        }).ToList()
                    };

                    customers.Add(customer);
                    sb.AppendLine(string.Format(SuccessfulImportCustomerTicket, customer.FirstName, customer.LastName, customer.Tickets.Count));
                }
                else
                {
                    sb.AppendLine(ErrorMessage);
                }
            }

            context.Customers.AddRange(customers);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        private static bool IsValid(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResult = new List<ValidationResult>();

            return Validator.TryValidateObject(dto, validationContext, validationResult, true);
        }
    }
}