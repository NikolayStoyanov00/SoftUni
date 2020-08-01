namespace BookShop.DataProcessor
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
    using BookShop.Data.Models;
    using BookShop.Data.Models.Enums;
    using BookShop.DataProcessor.ImportDto;
    using BookShop.XmlHelper;
    using Data;
    using Newtonsoft.Json;
    using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";

        private const string SuccessfullyImportedBook
            = "Successfully imported book {0} for {1:F2}.";

        private const string SuccessfullyImportedAuthor
            = "Successfully imported author - {0} with {1} books.";

        public static string ImportBooks(BookShopContext context, string xmlString)
        {
            StringBuilder sb = new StringBuilder();

            const string rootName = "Books";

            var bookDtos = XmlConverter.Deserializer<ImportBookDto>(xmlString, rootName);

            var books = new List<Book>();

            foreach (var bookDto in bookDtos)
            {
                if (IsValid(bookDto))
                {
                    var date = DateTime.ParseExact(bookDto.PublishedOn, "MM/dd/yyyy", CultureInfo.InvariantCulture);

                    Book book = new Book()
                    {
                        Name = bookDto.Name,
                        Genre = (Genre)bookDto.Genre,
                        Price = bookDto.Price,
                        Pages = bookDto.Pages,
                        PublishedOn = date
                    };

                    books.Add(book);
                    sb.AppendLine(string.Format(SuccessfullyImportedBook, book.Name, book.Price));
                }
                else
                {
                    sb.AppendLine(ErrorMessage);
                }
            }

            context.Books.AddRange(books);
            context.SaveChanges();

            string result = sb.ToString().TrimEnd();
            return result;
        }

        public static string ImportAuthors(BookShopContext context, string jsonString)
        {
            var authorDtos = JsonConvert.DeserializeObject<ImportAuthorDto[]>(jsonString);

            StringBuilder sb = new StringBuilder();

            var authors = new List<Author>();

            foreach (var authorDto in authorDtos)
            {
                if (IsValid(authorDto))
                {
                    bool emailExists = authors
                    .FirstOrDefault(x => x.Email == authorDto.Email) != null;

                    if (emailExists)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    Author author = new Author
                    {
                        FirstName = authorDto.FirstName,
                        LastName = authorDto.LastName,
                        Email = authorDto.Email,
                        Phone = authorDto.Phone,
                    };

                    foreach (var authorDtoBookDto in authorDto.Books)
                    {
                        var book = context.Books.Find(authorDtoBookDto.Id);

                        if (book == null)
                        {
                            continue;
                        }

                        author.AuthorsBooks.Add(new AuthorBook
                        {
                            Author = author,
                            Book = book
                        });
                    }

                    if (author.AuthorsBooks.Count == 0)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    authors.Add(author);
                    sb.AppendLine(string.Format(SuccessfullyImportedAuthor, (author.FirstName + " " + author.LastName), author.AuthorsBooks.Count));
                }
                else
                {
                    sb.AppendLine(ErrorMessage);
                }
            }

            context.Authors.AddRange(authors);
            context.SaveChanges();

            string result = sb.ToString().TrimEnd();

            return result;
        }

        private static bool IsValid(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResult = new List<ValidationResult>();

            return Validator.TryValidateObject(dto, validationContext, validationResult, true);
        }
    }
}