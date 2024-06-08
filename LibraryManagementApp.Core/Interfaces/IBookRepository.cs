using System.Collections.Generic;
using System.Threading.Tasks;
using LibraryManagementApp.Core.Entities;

namespace LibraryManagementApp.Core.Interfaces
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetAllBooks();
        Task<Book> GetBookById(int id);
        Task AddBook(Book book);
        Task UpdateBook(Book book);
        Task DeleteBook(int id);
        Task<IEnumerable<Book>> GetBooksByAuthor(int authId);
    }
}
