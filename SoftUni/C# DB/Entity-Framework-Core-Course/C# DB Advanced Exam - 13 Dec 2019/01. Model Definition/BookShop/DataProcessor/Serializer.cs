namespace BookShop.DataProcessor
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;
    using BookShop.Data.Models.Enums;
    using BookShop.DataProcessor.ExportDto;
    using BookShop.XmlHelper;
    using Data;
    using Newtonsoft.Json;
    using Formatting = Newtonsoft.Json.Formatting;

    public class Serializer
    {
        public static string ExportMostCraziestAuthors(BookShopContext context)
        {
            var authors = context
                .Authors
                .Select(x => new ExportCraziestAuthorDto
                {
                    AuthorName = x.FirstName + " " + x.LastName,
                    Books = x.AuthorsBooks
                    .OrderByDescending(x => x.Book.Price)
                    .Select(b => new CraziestAuthorBookDto
                    {
                        BookName = b.Book.Name,
                        BookPrice = b.Book.Price.ToString("F2")
                    })
                    .ToList()

                })
                .ToList()
                .OrderByDescending(x => x.Books.Count)
                .ThenBy(x => x.AuthorName)
                .ToList();

            string jsonString = JsonConvert.SerializeObject(authors, Formatting.Indented);
            return jsonString;
        }

        public static string ExportOldestBooks(BookShopContext context, DateTime date)
        {
            StringBuilder sb = new StringBuilder();

            const string rootName = "Books";

            var books = context
                .Books
                .Where(x => x.Genre == Genre.Science && x.PublishedOn < date)
                .ToArray()
                .OrderByDescending(x => x.Pages)
                .ThenByDescending(x => x.PublishedOn)
                .Take(10)
                .Select(x => new ExportBookDto()
                {
                    Name = x.Name,
                    Pages = x.Pages,
                    PublishedOn = x.PublishedOn.ToString("d", CultureInfo.InvariantCulture)
                })
                .ToArray();

            string xmlString = XmlConverter.Serialize<ExportBookDto[]>(books, rootName);
            return xmlString.ToString().TrimEnd();
        }
    }
}