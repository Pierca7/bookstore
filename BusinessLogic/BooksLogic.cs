using TechChallenge.Models;
using TechChallenge.DataAccess;
using System.Collections.Generic;
using System.Linq;

namespace TechChallenge.BusinessLogic
{
    public class BooksLogic : IBooksLogic
    {
        public BooksLogic(ApplicationDbContext _context)
        {
            context = _context;
        }

        private ApplicationDbContext context;

        //Insert a new book into the database.
        public void AddBook(Book newBook)
        {
            context.Books.Add(newBook);
            context.SaveChanges();
        }

        //Remove a book from the database
        public void RemoveBook(int id)
        {
            var book = context.Books.Find(id);
            context.Books.Remove(book);
            context.SaveChanges();
        }

        //Update the given book.
        public void UpdateBook(Book editedBook)
        {
            var book = context.Books.Find(editedBook.Id);
            context.Entry(book).CurrentValues.SetValues(editedBook);
            context.SaveChanges();
        }

        //Search for a book by id.
        public Book GetBookById(int id)
        {
            return context.Books.SingleOrDefault(book => book.Id == id);
        }

        //Return all books.
        public List<Book> GetAllBooks()
        {
            return context.Books.ToList();
        }

        //Sort the books by the given key.
        public List<Book> SortBy(List<Book> books, string key)
        {
            switch (key)
            {
                case "Title":
                    return books.OrderBy(book => book.Title).ToList();
                case "Author":
                    return books.OrderBy(book => book.Author).ToList();
                case "Published":
                    return books.OrderByDescending(book => book.Published).ToList();
                case "Relevance":
                    return books.OrderBy(book => book.Id).ToList();
                default:
                    return books.OrderBy(book => book.Id).ToList();
            }
        }
    }

}