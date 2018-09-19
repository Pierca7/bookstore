using TechChallenge.Models;
using System.Collections.Generic;


namespace TechChallenge.BusinessLogic
{
    public interface IBooksLogic
    {
        //Insert a new book into the database.
        void AddBook(Book newBook);

        //Remove a book from the database
        void RemoveBook(int id);

        //Update the given book.
        void UpdateBook(Book editedBook);

        //Search for a book by id.
        Book GetBookById(int Id);

        //Return all books.
        List<Book> GetAllBooks();

        //Sort the books by the given key.
        List<Book> SortBy(List<Book> books, string key);

    }

}