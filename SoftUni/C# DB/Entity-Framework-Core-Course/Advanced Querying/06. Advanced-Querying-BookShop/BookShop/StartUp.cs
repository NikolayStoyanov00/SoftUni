namespace BookShop
{
    using BookShop.Models;
    using Data;
    using Initializer;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class StartUp
    {
        public static void Main()
        {
            using var db = new BookShopContext();
            DbInitializer.ResetDatabase(db);

            int result = RemoveBooks(db);
            Console.WriteLine(result);
        }

        //1. Age Restriction
        public static string GetBooksByAgeRestriction(BookShopContext context, string command)
        {
            var booksTitles = context
                    .Books
                    .OrderBy(b => b.Title)
                    .ToList()
                    .Where(b => b.AgeRestriction.ToString().ToLower() == command.ToLower())
                    .Select(b => b.Title)
                    .ToList();
                    

            return string.Join(Environment.NewLine, booksTitles).ToString().TrimEnd();
        }

        //2. Golden Books
        public static string GetGoldenBooks(BookShopContext context)
        {
            StringBuilder sb = new StringBuilder();

            var books = context
                        .Books
                        .Where(b => b.Copies < 5000)
                        .OrderBy(b => b.BookId)
                        .Select(b => new
                        {
                            b.Title,
                            b.EditionType
                        })
                        .ToList()
                        .Where(b => b.EditionType.ToString() == "Gold")
                        .ToList();

            foreach (var book in books)
            {
                sb.AppendLine(book.Title);
            }

            return sb.ToString().TrimEnd();
        }

        //3. Books by Price
        public static string GetBooksByPrice(BookShopContext context)
        {
            StringBuilder sb = new StringBuilder();

            var books = context
                    .Books
                    .Select(b => new
                    {
                        b.Title,
                        b.Price
                    })
                    .Where(b => b.Price > 40)
                    .OrderByDescending(b => b.Price)
                    .ToList();

            foreach (var book in books)
            {
                sb.AppendLine($"{book.Title} - ${book.Price:f2}");
            }

            return sb.ToString();
        }

        //4. Not Released In
        public static string GetBooksNotReleasedIn(BookShopContext context, int year)
        {
            StringBuilder sb = new StringBuilder();

            var books = context
                        .Books
                        .Where(b => b.ReleaseDate.Value.Year != year)
                        .OrderBy(b => b.BookId)
                        .Select(b => b.Title)
                        .ToList();

            return string.Join(Environment.NewLine, books);
        }

        //5. Book Titles by Category
        public static string GetBooksByCategory(BookShopContext context, string input)
        {
            StringBuilder sb = new StringBuilder();

            List<string> categories = input
                .Split(" ")
                .Select(c => c.ToLower())
                .ToList();

            var books = context
                            .Books
                            .Select(b => new
                            {
                                b.Title,
                                b.BookCategories,
                            })
                            .Where(b => b.BookCategories.Any(b => categories.Contains(b.Category.Name.ToLower())))
                            .OrderBy(b => b.Title)
                            .ToList();

            foreach (var book in books)
            {
                sb.AppendLine(book.Title);
            }

            return sb.ToString();
        }

        //6. Released Before Date
        public static string GetBooksReleasedBefore(BookShopContext context, string date)
        {
            StringBuilder sb = new StringBuilder();

            DateTime parsedDate = DateTime.ParseExact(date, "dd-MM-yyyy", null);

            var books = context
                            .Books
                            .Where(b => b.ReleaseDate.Value < parsedDate)
                            .OrderByDescending(b => b.ReleaseDate)
                            .Select(b => new
                            {
                                Title = b.Title,
                                Edition = b.EditionType.ToString(),
                                Price = b.Price,
                                Date = b.ReleaseDate.Value
                            })
                            .ToList();

            foreach (var book in books)
            {
                sb.AppendLine($"{book.Title} - {book.Edition} - ${book.Price:f2}");
            }

            return sb.ToString();
        }

        //7. Author Search
        public static string GetAuthorNamesEndingIn(BookShopContext context, string input)
        {
            StringBuilder sb = new StringBuilder();

            var authors = context
                            .Authors
                            .Where(a => a.FirstName.EndsWith(input))
                            .Select(a => new
                            {
                                FullName = a.FirstName + " " + a.LastName
                            })
                            .OrderBy(a => a.FullName)
                            .ToList();

            foreach (var author in authors)
            {
                sb.AppendLine(author.FullName);
            }

            return sb.ToString().Trim();
        }

        //8. Book Search
        public static string GetBookTitlesContaining(BookShopContext context, string input)
        {
            input = input.ToLower();

            var books = context
                            .Books
                            .Where(b => b.Title.ToLower().Contains(input))
                            .Select(b => b.Title)
                            .OrderBy(b => b)
                            .ToList();

            return string.Join(Environment.NewLine, books);
        }

        //9. Book Search by Author
        public static string GetBooksByAuthor(BookShopContext context, string input)
        {
            StringBuilder sb = new StringBuilder();

            input = input.ToLower();

            var books = context
                            .Books
                            .Where(b => b.Author.LastName.ToLower().StartsWith(input))
                            .OrderBy(b => b.BookId)
                            .Select(b => new
                            {
                                b.Title,
                                AuthorFirstName = b.Author.FirstName,
                                AuthorLastName = b.Author.LastName
                            })
                            .ToList();

            foreach (var book in books)
            {
                sb.AppendLine($"{book.Title} ({book.AuthorFirstName} {book.AuthorLastName})");
            }

            return sb.ToString().Trim();
        }

        //10. Count Books
        public static int CountBooks(BookShopContext context, int lengthCheck)
        {
            var booksCount = context.Books.Count(b => b.Title.Length > lengthCheck);

            return booksCount;
        }

        //11. Total Book Copies
        public static string CountCopiesByAuthor(BookShopContext context)
        {
            StringBuilder sb = new StringBuilder();

            var authors = context
                            .Authors
                            .Select(a => new
                            {
                                FullName = a.FirstName + " " + a.LastName,
                                BookCopies = a.Books.Sum(b => b.Copies)
                            })
                            .OrderByDescending(a => a.BookCopies)
                            .ToList();

            foreach (var author in authors)
            {
                sb.AppendLine($"{author.FullName} - {author.BookCopies}");
            }

            return sb.ToString();
        }

        //12. Profit by Category
        public static string GetTotalProfitByCategory(BookShopContext context)
        {
            StringBuilder sb = new StringBuilder();

            var categories = context
                                .Categories
                                .Select(c => new
                                {
                                    c.Name,
                                    TotalProfit = c.CategoryBooks.Sum(b => b.Book.Copies * b.Book.Price)
                                })
                                .OrderByDescending(c => c.TotalProfit)
                                .ThenBy(c => c.Name)
                                .ToList();

            foreach (var category in categories)
            {
                sb.AppendLine($"{category.Name} ${category.TotalProfit:f2}");
            }

            return sb.ToString().Trim();
        }

        //13. Most Recent Books
        public static string GetMostRecentBooks(BookShopContext context)
        {
            StringBuilder sb = new StringBuilder();

            var categories = context
                                .Categories
                                .Select(c => new
                                {
                                    c.Name,
                                    c.CategoryBooks
                                })
                                .OrderBy(c => c.Name)
                                .ToList();

            foreach (var category in categories)
            {
                sb.AppendLine($"--{category.Name}");

                var bookIds = category
                                .CategoryBooks
                                .Select(b => b.BookId)
                                .ToList();

                var booksForCategory = context
                                            .Books
                                            .Where(b => bookIds.Contains(b.BookId))
                                            .OrderByDescending(b => b.ReleaseDate)
                                            .Select(b => new
                                            {
                                                b.Title,
                                                ReleaseYear = b.ReleaseDate.Value.Year
                                            })
                                            .Take(3)
                                            .ToList();

                foreach (var book in booksForCategory)
                {
                    sb.AppendLine($"{book.Title} ({book.ReleaseYear})");
                }
            }

            return sb.ToString().Trim();
        }

        //14. Increase Prices
        public static void IncreasePrices(BookShopContext context)
        {
            var books = context
                            .Books
                            .Where(b => b.ReleaseDate.Value.Year < 2010)
                            .ToList();

            books.ForEach(b => b.Price += 5);
            context.SaveChanges();
        }

        //15. Remove Books
        public static int RemoveBooks(BookShopContext context)
        {
            var books = context
                            .Books
                            .Where(b => b.Copies < 4200)
                            .ToList();

            int range = books.Count();

            context.Books.RemoveRange(books);
            context.SaveChanges();
            return range;
        }
    }
}
