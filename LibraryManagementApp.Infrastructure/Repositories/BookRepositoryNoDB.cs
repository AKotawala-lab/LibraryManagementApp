using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryManagementApp.Core.Entities;
using LibraryManagementApp.Core.Interfaces;

namespace LibraryManagementApp.Infrastructure.Repositories
{
    public class BookRepositoryNoDB : IBookRepository
    {
        private readonly List<Book> _books;

        public BookRepositoryNoDB()
        {
            _books = new List<Book>
            {
                new Book { BookId = 1, Title = "Romeo & Julliet", Description = "Ever green love story", AuthorId = 1 },
                new Book { BookId = 2, Title = "Hamlet", Description = "Description 2", AuthorId = 2 },
                new Book { BookId = 3, Title = "Rich Dad Poor Dad", Description = "Description 2", AuthorId = 2 }
            };
        }

        public Task<IEnumerable<Book>> GetAllBooks() => Task.FromResult(_books.AsEnumerable());

        public Task<Book> GetBookById(int id) => Task.FromResult(_books.FirstOrDefault(b => b.BookId == id));

        public Task AddBook(Book book)
        {
            book.BookId = _books.Count + 1;
            _books.Add(book);
            return Task.CompletedTask;
        } 

        public Task UpdateBook(Book book)
        {
            var existingBook = _books.FirstOrDefault(b => b.BookId == book.BookId);
            if (existingBook != null)
            {
                existingBook.Title = book.Title;
                existingBook.Description = book.Description;
                existingBook.AuthorId = book.AuthorId;
                existingBook.Author = book.Author;
            }
            return Task.CompletedTask;
        }

        public Task DeleteBook(int id)
        {
            var book = _books.FirstOrDefault(b => b.BookId == id);
            if (book != null)
            {
                _books.Remove(book);
            }
            return Task.CompletedTask;
            //_books.RemoveAll(b => b.BookId == id);
        }  

        public Task<IEnumerable<Book>> GetBooksByAuthor(int authId) 
        { 
            return Task.FromResult(_books.Where(b => b.AuthorId == authId).AsEnumerable());
        }

    }
}
