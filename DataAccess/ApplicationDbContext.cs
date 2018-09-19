using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TechChallenge.Models;

namespace TechChallenge.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Book> Books { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //Seed the database.
            builder.Entity<Book>().HasData(
                Seeding().ToArray());
        }

        //Generate a list of books to seed the database.
        private List<Book> Seeding()
        {
            char[] alphabet = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
            string[] genres = new string[] { "Fantasy", "Horror", "Drama" };
            List<Book> books = new List<Book>();
            var random = new Random();

            for (int i = 0; i < 12; i++)
            {
                var book = new Book
                {
                    /*Entity Framework Code First doesn't have an option for setting auto-incremented property initial value,
                    so for the sake of simplicity I'm using high numbers to avoid id conficts.
                    If I were using a real SQL Database I should have change initial value within the initial migration.*/
                    Id = i + 100,
                    Title = "Book " + alphabet[i],
                    Author = "Author " + alphabet[alphabet.Count() - 1 - i],
                    Summary = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor. Aenean massa. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Donec quam felis, ultricies nec, pellentesque eu, pretium quis.",
                    Genre = genres[random.Next(0, 3)],
                    CoverPage = "/images/Book" + alphabet[i] + ".png",
                    Published = random.Next(1900, 2019),
                    Link = "http://www.google.com"
                };
                books.Add(book);
            }
            return books;
        }
    }
}