using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore; 
using LibraryManagementApp.Core.Entities;
using LibraryManagementApp.Core.Interfaces;
using LibraryManagementApp.Infrastructure.Data;

namespace LibraryManagementApp.Infrastructure.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly List<Book> _books;

        private readonly LibraryDbContext _context;

        public BookRepository(LibraryDbContext context)
        {
            _books = new List<Book>
            {
                new Book { BookId = 1, Title = "Romeo & Julliet", Description = "Ever green love story", AuthorId = 1 },
                new Book { BookId = 2, Title = "Hamlet", Description = "Description 2", AuthorId = 2 },
                new Book { BookId = 3, Title = "Rich Dad Poor Dad", Description = "Description 2", AuthorId = 2 }
            };

            _context = context;
        }

        public async Task<IEnumerable<Book>> GetAllBooks() => await _context.Books.Include(b => b.Author).ToListAsync();

        public async Task<Book> GetBookById(int id) => await _context.Books.Include(b => b.Author).FirstOrDefaultAsync(b => b.BookId == id);

        public async Task AddBook(Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
        } 

        public async Task UpdateBook(Book book)
        {
            var existingBook = await _context.Books.FirstOrDefaultAsync(b => b.BookId == book.BookId);
            if (existingBook != null)
            {
                existingBook.Title = book.Title;
                existingBook.Description = book.Description;
                existingBook.AuthorId = book.AuthorId;
                existingBook.Author = book.Author;
            }
            _context.Books.Update(existingBook);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteBook(int id)
        {
            var book = await _context.Books.FirstOrDefaultAsync(b => b.BookId == id);
            if (book != null)
            {
               _context.Books.Remove(book);
                await _context.SaveChangesAsync();
            }
            //_books.RemoveAll(b => b.BookId == id);
        }  

        public async Task<IEnumerable<Book>> GetBooksByAuthor(int authId) 
        { 
            return await _context.Books.Include(b => b.Author).Where(b => b.AuthorId == authId).ToListAsync();
        }

    }
}
