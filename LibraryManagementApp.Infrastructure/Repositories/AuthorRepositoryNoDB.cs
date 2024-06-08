using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryManagementApp.Core.Entities;
using LibraryManagementApp.Core.Interfaces;

namespace LibraryManagementApp.Infrastructure.Repositories
{
    public class AuthorRepositoryNoDB : IAuthorRepository
    {
        private readonly List<Author> _authors;

        public AuthorRepositoryNoDB()
        {
            _authors = new List<Author>
            {
                new Author { AuthorId = 1, Name = "Shakespeare", Description = "Shakespeare is a great author." },
                new Author { AuthorId = 2, Name = "Robert Kiyosaki", Description = "Robert Kiyosaki is a famous author." }
            };
        }


        public Task<IEnumerable<Author>> GetAllAuthorsAsync() => Task.FromResult(_authors.AsEnumerable());

        public Task<Author> GetAuthorByIdAsync(int id)
        {
            var author = _authors.FirstOrDefault(a => a.AuthorId == id);
            return Task.FromResult(author);
        }

        public Task AddAuthorAsync(Author author)
        {
            author.AuthorId = _authors.Count + 1;
            _authors.Add(author);
            return Task.CompletedTask;
        }

        public Task UpdateAuthorAsync(Author author)
        {
            var existingAuthor = _authors.FirstOrDefault(a => a.AuthorId == author.AuthorId);
            if (existingAuthor != null)
            {
                existingAuthor.Name = author.Name;
                existingAuthor.Description = author.Description;
                existingAuthor.Books = author.Books;
            }
            return Task.CompletedTask;
        }

        public Task DeleteAuthorAsync(int id)
        {
            var author = _authors.FirstOrDefault(a => a.AuthorId == id);
            if (author != null)
            {
                _authors.Remove(author);
            }
            return Task.CompletedTask;
        }
    }
}
