using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore; 
using LibraryManagementApp.Core.Entities;
using LibraryManagementApp.Core.Interfaces;
using LibraryManagementApp.Infrastructure.Data;

namespace LibraryManagementApp.Infrastructure.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly LibraryDbContext _context;

        public AuthorRepository(LibraryDbContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<Author>> GetAllAuthorsAsync() => await _context.Authors.Include(a => a.Books).ToListAsync();

        public async Task<Author> GetAuthorByIdAsync(int id)
        {
            return await _context.Authors.Include(a => a.Books).FirstOrDefaultAsync(a => a.AuthorId == id);
        }

        public async Task AddAuthorAsync(Author author)
        {
            _context.Authors.Add(author);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAuthorAsync(Author author)
        {
            var existingAuthor = await _context.Authors.FindAsync(author.AuthorId);
            if (existingAuthor != null)
            {
                existingAuthor.Name = author.Name;
                existingAuthor.Description = author.Description;
                existingAuthor.Books = author.Books;
            }
            
            _context.Authors.Update(existingAuthor);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAuthorAsync(int id)
        {
            var author = await _context.Authors.FindAsync(id);
            if (author != null)
            {
                _context.Authors.Remove(author);
                await _context.SaveChangesAsync();
            }
        }
    }
}
