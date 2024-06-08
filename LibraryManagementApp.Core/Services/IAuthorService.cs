using System.Collections.Generic;
using System.Threading.Tasks;
using LibraryManagementApp.Core.Models;

namespace LibraryManagementApp.Core.Interfaces
{
    public interface IAuthorService
    {
        Task<IEnumerable<AuthorDTO>> GetAllAuthors();
        Task<AuthorDTO> GetAuthorById(int id);
        Task AddAuthor(AuthorDTO authorDTO);
        Task UpdateAuthor(AuthorDTO authorDTO);
        Task DeleteAuthor(int id);
    }
}
