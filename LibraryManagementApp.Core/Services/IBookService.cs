using System.Collections.Generic;
using System.Threading.Tasks;
using LibraryManagementApp.Core.Models;

namespace LibraryManagementApp.Core.Interfaces
{
    public interface IBookService
    {
        Task<IEnumerable<BookDTO>> GetAllBooks();
        Task<BookDTO> GetBookById(int id);
        Task AddBook(BookDTO book);
        Task UpdateBook(BookDTO book);
        Task DeleteBook(int id);
        Task<IEnumerable<BookDTO>> GetBooksByAuthor(int authId);
    }
}
