using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryManagementApp.Core.Models;
using LibraryManagementApp.Core.Entities;
using LibraryManagementApp.Core.Interfaces;

namespace LibraryManagementApp.Core.Services.Implementations
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;

        public AuthorService(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<IEnumerable<AuthorDTO>> GetAllAuthors()
        {
            var authors = await _authorRepository.GetAllAuthorsAsync();

            return authors.Select(author => new AuthorDTO
            {
                AuthorId = author.AuthorId,
                Name = author.Name,
                Description = author.Description,
                Books = author.Books.Select(book => new BookDTO
                {
                    BookId = book.BookId,
                    Title = book.Title,
                    Description = book.Description,
                    AuthorId = book.AuthorId
                }).ToList()
            }).ToList();
        }

        public async Task<AuthorDTO> GetAuthorById(int id)
        {
            var author = await _authorRepository.GetAuthorByIdAsync(id);
            if (author == null)
            {
                return null;
            }

            return new AuthorDTO
            {
                AuthorId = author.AuthorId,
                Name = author.Name,
                Description = author.Description,
                Books = author.Books.Select(book => new BookDTO
                {
                    BookId = book.BookId,
                    Title = book.Title,
                    Description = book.Description,
                    AuthorId = book.AuthorId
                }).ToList()
            };
        }

        public async Task AddAuthor(AuthorDTO authorDTO)
        {
            var author = new Author
            {
                Name = authorDTO.Name,
                Description = authorDTO.Description,
                Books = authorDTO.Books.Select(bookDTO => new Book
                {
                    BookId = bookDTO.BookId,
                    Title = bookDTO.Title,
                    Description = bookDTO.Description,
                    AuthorId = bookDTO.AuthorId
                }).ToList()
            };

            await _authorRepository.AddAuthorAsync(author);
        }

        public async Task UpdateAuthor(AuthorDTO authorDTO)
        {
            var author = new Author
            {
                AuthorId = authorDTO.AuthorId,
                Name = authorDTO.Name,
                Description = authorDTO.Description,
                Books = authorDTO.Books.Select(bookDTO => new Book
                {
                    BookId = bookDTO.BookId,
                    Title = bookDTO.Title,
                    Description = bookDTO.Description,
                    AuthorId = bookDTO.AuthorId
                }).ToList()
            };

            await _authorRepository.UpdateAuthorAsync(author);
        }

        public async Task DeleteAuthor(int id)
        {
            await _authorRepository.DeleteAuthorAsync(id);
        }


    }
}
