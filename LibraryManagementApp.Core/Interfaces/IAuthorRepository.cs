using System.Collections.Generic;
using System.Threading.Tasks;
using LibraryManagementApp.Core.Entities;

namespace LibraryManagementApp.Core.Interfaces
{
    public interface IAuthorRepository
    {
        Task<IEnumerable<Author>> GetAllAuthorsAsync();
        Task<Author> GetAuthorByIdAsync(int id);
        Task AddAuthorAsync(Author author);
        Task UpdateAuthorAsync(Author author);
        Task DeleteAuthorAsync(int id);
    }
}
