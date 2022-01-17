namespace BookShop
{
    using BookShop.Models;
    using BookShop.Models.Enums;
    using Data;
    using Initializer;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text;

    public class StartUp
    {
        public static void Main()
        {
            using var db = new BookShopContext();
            //DbInitializer.ResetDatabase(db);

            //01.Age Restriction
            //var command = Console.ReadLine();
            //Console.WriteLine(GetBooksByAgeRestriction(db, command));

            //02.Golden Books
            //Console.WriteLine(GetGoldenBooks(db));

            //03. Books by Price
            //Console.WriteLine(GetBooksByPrice(db));

            //4. Not Released In
            //var year = int.Parse(Console.ReadLine());
            //Console.WriteLine(GetBooksNotReleasedIn(db, year));

            //05. Book Titles by Category
            //var input = Console.ReadLine();
            //Console.WriteLine(GetBooksByCategory(db, input));

            //06. Released Before Date
            //Console.WriteLine(GetBooksReleasedBefore(db, input));

            //07. Author Search
            //Console.WriteLine(GetAuthorNamesEndingIn(db, input));

            //08. Book Search
            //Console.WriteLine(GetBookTitlesContaining(db, input));

            //09. Book Search by Author
            //Console.WriteLine(GetBooksByAuthor(db, input));

            //10. Count Books
            //var input = int.Parse(Console.ReadLine());
            //Console.WriteLine(CountBooks(db, input));

            //11. Total Book Copies
            //Console.WriteLine(CountCopiesByAuthor(db));

            //12. Profit by Category
            //Console.WriteLine(GetTotalProfitByCategory(db));

            //13.Most Recent Books
            //Console.WriteLine(GetMostRecentBooks(db));

            //14. Increase Prices
            //IncreasePrices(db);

            //15. Remove Books
            Console.WriteLine(RemoveBooks(db));
        }

        //01.Age Restriction
        public static string GetBooksByAgeRestriction(BookShopContext context, string command)
        {
            var restriction = Enum.Parse<AgeRestriction>(command, true);

            var books = context.Books
                .Where(b => b.AgeRestriction == restriction)
                .Select(t => t.Title)
                .OrderBy(b => b)
                .ToList();

            var result = string.Join(Environment.NewLine, books);

            return result.ToString();
        }

        //02.Golden Books
        public static string GetGoldenBooks(BookShopContext context)
        {
            var books = context.Books
                .Where(b => b.EditionType == EditionType.Gold &&
                            b.Copies < 5000)
                .Select(t => new
                {
                    Id = t.BookId,
                    Tittle = t.Title
                })
                .OrderBy(b => b.Id)
                .ToList();

            return string.Join(Environment.NewLine, books.Select(b => b.Tittle));
        }

        //03. Books by Price
        public static string GetBooksByPrice(BookShopContext context)
        {
            var books = context.Books
                .Where(p => p.Price > 40)
                .Select(b => new
                {
                    Title = b.Title,
                    Price = b.Price
                })
                .OrderByDescending(b => b.Price)
                .ToList();

            var result = new StringBuilder();

            foreach (var b in books)
            {
                result.AppendLine($"{b.Title} - ${b.Price:F2}");
            }

            return result.ToString().Trim();
        }

        //04. Not Released In
        public static string GetBooksNotReleasedIn(BookShopContext context, int year)
        {

            var books = context.Books
                .Where(b => b.ReleaseDate.Value.Year != year)
                .Select(b => new
                {
                    b.BookId,
                    b.Title
                })
                .OrderBy(b => b.BookId)
                .ToList();

            return string.Join(Environment.NewLine, books.Select(b => b.Title));
        }

        //05. Book Titles by Category
        public static string GetBooksByCategory(BookShopContext context, string input)
        {
            var categoriesInfo = input
                                  .ToLower()
                                  .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                                  .ToArray();

            var books = context.Books
                .Include(b => b.BookCategories)
                .ThenInclude(c => c.Category)
                .Where(b => b.BookCategories
                        .Any(c => categoriesInfo
                        .Contains(c.Category.Name.ToLower())))
                .Select(b => b.Title)
                .OrderBy(b => b)
                .ToList();

            return string.Join(Environment.NewLine, books);
        }

        //06. Released Before Date
        public static string GetBooksReleasedBefore(BookShopContext context, string date)
        {
            var dateTime = DateTime.ParseExact(date, "dd-MM-yyyy", CultureInfo.InvariantCulture);

            var books = context.Books
                .Where(b => b.ReleaseDate < dateTime)
                .Select(b => new
                {
                    Title = b.Title,
                    EditionType = b.EditionType,
                    Price = b.Price,
                    ReleaseDate = b.ReleaseDate
                })
                .OrderByDescending(r => r.ReleaseDate)
                .ToList();

            return string.Join(Environment.NewLine, books
                               .Select(b => $"{b.Title} - {b.EditionType} - ${b.Price:F2}"));
        }

        //07. Author Search
        public static string GetAuthorNamesEndingIn(BookShopContext context, string input)
        {
            var authors = context.Authors
                .Where(n => n.FirstName.EndsWith(input))
                .Select(a => new { FullName = a.FirstName + " " + a.LastName })
                .OrderBy(f => f.FullName)
                .ToList();

            return string.Join(Environment.NewLine, authors
                                          .Select(a => a.FullName));
        }

        //08. Book Search
        public static string GetBookTitlesContaining(BookShopContext context, string input)
        {
            var bookTitles = context.Books
                .Where(b => b.Title.ToLower().Contains(input.ToLower()))
                .Select(b => new { Title = b.Title })
                .OrderBy(b => b.Title)
                .ToList();

            return string.Join(Environment.NewLine, bookTitles
                                          .Select(b => b.Title));
        }

        //09. Book Search by Author
        public static string GetBooksByAuthor(BookShopContext context, string input)
        {
            var books = context.Books
                .Where(a => a.Author.LastName.ToLower()
                                    .StartsWith(input.ToLower()))
                .Select(b => new
                {
                    Id = b.BookId,
                    Title = b.Title,
                    AuthorFullName = b.Author.FirstName + ' ' + b.Author.LastName
                })
                .OrderBy(i => i.Id)
                .ToList();

            return string.Join(Environment.NewLine, books
                                          .Select(b => $"{b.Title} ({b.AuthorFullName})"));
        }

        //10. Count Books
        public static int CountBooks(BookShopContext context, int lengthCheck)
        {
            var count = context.Books
                .Where(b => b.Title.Length > lengthCheck)
                .Select(b => b.Title)
                .ToList();

            return count.Count;
        }

        //11. Total Book Copies
        public static string CountCopiesByAuthor(BookShopContext context)
        {

            var books = context.Authors
                            .Select(a => new
                            {
                                TotalBookCopies = a.Books
                                                   .Sum(b => b.Copies),
                                FullName = a.FirstName + ' ' + a.LastName
                            })
                            .OrderByDescending(c => c.TotalBookCopies)
                            .ToList();

            return string.Join(Environment.NewLine, books
                                          .Select(a => $"{a.FullName} - {a.TotalBookCopies}"));
        }

        //12. Profit by Category
        public static string GetTotalProfitByCategory(BookShopContext context)
        {
            var categoriesProfit = context.Categories
                               .Select(c => new
                               {
                                   Category = c.Name,
                                   TotalProfit = c.CategoryBooks
                                                     .Sum(b => b.Book.Price * b.Book.Copies)
                               })
                               .OrderByDescending(t => t.TotalProfit)
                               .ThenBy(c => c.Category)
                               .ToList();

            return string.Join(Environment.NewLine, categoriesProfit
                                          .Select(cp => $"{cp.Category} ${cp.TotalProfit:F2}"));

        }

        //13.Most Recent Books
        public static string GetMostRecentBooks(BookShopContext context)
        {
            var books = context.Categories
                .Select(c => new
                {
                    Category = c.Name,
                    Top3 = c.CategoryBooks
                    .OrderByDescending(b => b.Book.ReleaseDate)
                    .Take(3)
                    .Select(b => new
                    {
                        BookTitle = b.Book.Title,
                        ReleaseYear = b.Book.ReleaseDate.Value.Year
                    })
                })
                .OrderBy(c=>c.Category)
                .ToList();

            var result = new StringBuilder();

            foreach (var b in books)
            {
                result.AppendLine($"--{b.Category}");

                foreach (var top in b.Top3)
                {
                    result.AppendLine($"{top.BookTitle} ({top.ReleaseYear})");
                }
            }

            return result.ToString();
        }

        //14. Increase Prices
        public static void IncreasePrices(BookShopContext context)
        {

            var books = context.Books
                               .Where(x => x.ReleaseDate.Value.Year < 2010)
                               .ToList();

            foreach (var b in books)
            {
                b.Price += 5;
            }

            context.SaveChanges();
        }

        //15. Remove Books
        public static int RemoveBooks(BookShopContext context)
        {
            var books = context.Books
                               .Where(x => x.Copies < 4200)
                               .ToList();

            context.Books.RemoveRange(books);

            context.SaveChanges();

            return books.Count;
        }
    }
}
