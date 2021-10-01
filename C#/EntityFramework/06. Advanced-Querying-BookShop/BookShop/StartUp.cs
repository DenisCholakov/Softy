using System;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using BookShop.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace BookShop
{
    using Data;
    using Initializer;

    public class StartUp
    {
        public static void Main()
        {
            using var db = new BookShopContext();
            //DbInitializer.ResetDatabase(db);

            IncreasePrices(db);
        }

        public static int RemoveBooks(BookShopContext context)
        {
            var books = context.Books.Where(b => b.Copies < 4200).ToList();

            context.RemoveRange(books);

            context.SaveChanges();

            return books.Count;
        }

        public static void IncreasePrices(BookShopContext context)
        {
            var books = context.Books.Where(b => b.ReleaseDate.Value.Year < 2010).ToList();

            foreach (var book in books)
            {
                book.Price += 5;
            }

            context.SaveChanges();
        }

        public static string GetMostRecentBooks(BookShopContext context)
        {
            var categories = context.Categories
                .Select(c => new
                {
                    c.Name,
                    Books = c.CategoryBooks.Select(cb => new
                        {
                            cb.Book.Title,
                            cb.Book.ReleaseDate
                        }).OrderByDescending(b => b.ReleaseDate)
                        .Take(3)
                        .ToList()
                }).OrderBy(c => c.Name).ToList();

            var sb = new StringBuilder();

            foreach (var category in categories)
            {
                sb.AppendLine($"--{category.Name}");

                foreach (var book in category.Books)
                {
                    sb.AppendLine($"{book.Title} ({book.ReleaseDate.Value.Year})");
                }
            }

            return sb.ToString().TrimEnd();
        }

        public static string GetTotalProfitByCategory(BookShopContext context)
        {
            var categories = context.Categories
                .Select(c => new
                {
                    c.Name,
                    Profit = c.CategoryBooks.Sum(b => b.Book.Price * b.Book.Copies)
                }).OrderByDescending(c => c.Profit)
                .ThenBy(c => c.Name).ToList();

            return String.Join(Environment.NewLine, categories.Select(c =>
                        $"{c.Name} ${c.Profit:f2}"));
        }

        public static string CountCopiesByAuthor(BookShopContext context)
        {
            var authors = context.Authors.Select(a => new
            {
                a.FirstName,
                a.LastName,
                TotalCopies = a.Books.Sum(b => b.Copies)
            }).OrderByDescending(a => a.TotalCopies).ToList();

            return String.Join(Environment.NewLine, authors.Select(a => 
                                        $"{a.FirstName} {a.LastName} - {a.TotalCopies}"));
        }

        public static int CountBooks(BookShopContext context, int lengthCheck)
        {
            var books = context.Books.Where(b => b.Title.Length > lengthCheck)
                .Select(b => b.Title).ToList();

            return books.Count;
        }

        public static string GetBooksByAuthor(BookShopContext context, string input)
        {
            var books = context.Books
                .Where(b => EF.Functions.Like(b.Author.LastName, $"{input}%"))
                .Select(b => new
                {
                    b.Title,
                    AuthorName = b.Author.FirstName + " " + b.Author.LastName,
                    b.BookId
                }).OrderBy(b => b.BookId).ToList();

            return String.Join(Environment.NewLine, books.Select(b => 
                                                $"{b.Title} ({b.AuthorName})"));
        }

        public static string GetBookTitlesContaining(BookShopContext context, string input)
        {
            var books = context.Books
                .Where(b => EF.Functions.Like(b.Title, $"%{input}%"))
                .Select(b => b.Title).OrderBy(b => b).ToList();

            return String.Join(Environment.NewLine, books);
        }

        public static string GetAuthorNamesEndingIn(BookShopContext context, string input)
        {
            var authors = context.Authors.
                Where(a => EF.Functions.Like(a.FirstName, $"%{input}"))
                .Select(a => new
                {
                    a.FirstName,
                    a.LastName
                }).OrderBy(a => a.FirstName)
                .ThenBy(a => a.LastName);

            return String.Join(Environment.NewLine, authors.Select(a => $"{a.FirstName} {a.LastName}"));
        }

        public static string GetBooksReleasedBefore(BookShopContext context, string date)
        {
            var releaseDate = DateTime.ParseExact(date, "dd-MM-yyyy", CultureInfo.InvariantCulture);

            var books = context.Books
                .Where(b => b.ReleaseDate < releaseDate)
                .Select(b => new
                {
                    b.Title,
                    b.EditionType,
                    b.Price,
                    b.ReleaseDate
                }).OrderByDescending(b => b.ReleaseDate)
                .ToList();

            return String.Join(Environment.NewLine, books.Select(b =>
                $"{b.Title} - {b.EditionType} - ${b.Price:f2}"));
        }

        public static string GetBooksByCategory(BookShopContext context, string input)
        {
            var categories = input.Split().Select(t => t.ToLower()).ToList();

            var books = context.Books
                .Include(b => b.BookCategories)
                .ThenInclude(bc => bc.Category)
                .Where(b => b.BookCategories.Any(bc => categories.Contains(bc.Category.Name.ToLower())))
                .Select(b => b.Title)
                .OrderBy(b => b)
                .ToList();

            return String.Join(Environment.NewLine, books);
        }

        public static string GetBooksNotReleasedIn(BookShopContext context, int year)
        {
            var books = context.Books
                .Where(b => b.ReleaseDate.Value.Year != year)
                .Select(b => new
                {
                    b.Title,
                    b.BookId
                }).OrderBy(b => b.BookId);

            return String.Join(Environment.NewLine, books.Select(b => b.Title));
        }

        public static string GetBooksByPrice(BookShopContext context)
        {
            var books = context.Books.Where(b => b.Price > 40)
                .Select(b => new
                {
                    b.Title,
                    b.Price
                }).OrderByDescending(b => b.Price);

            return String.Join(Environment.NewLine, books.Select(b => $"{b.Title} - ${b.Price:f2}"));
        }

        public static string GetGoldenBooks(BookShopContext context)
        {
            var books = context.Books.Where(b => b.EditionType == EditionType.Gold && b.Copies < 5000)
                .Select(b => new
                {
                    b.BookId,
                    b.Title
                }).ToList();

            return String.Join(Environment.NewLine, books.Select(b => b.Title));
        }

        public static string GetBooksByAgeRestriction(BookShopContext context, string command)
        {
            var ageRestriction = Enum.Parse<AgeRestriction>(command, true);

            var books = context.Books
                .Where(book => book.AgeRestriction == ageRestriction)
                .Select(book => book.Title)
                .OrderBy(b => b).ToArray();

            return String.Join(Environment.NewLine, books);
        }
    }
}
