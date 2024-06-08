using System.Collections.Generic;
using System.Threading.Tasks;
using LibraryManagementApp.Core.Entities;
using LibraryManagementApp.Core.Interfaces;
using LibraryManagementApp.Core.Models;

namespace LibraryManagementApp.Core.Services.Implementations
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<IEnumerable<BookDTO>> GetAllBooks()
        {
            var books = await _bookRepository.GetAllBooks();  
            return books.Select(book => new BookDTO {
                BookId = book.BookId,
                Title = book.Title,
                Description = book.Description,
                AuthorId = book.AuthorId,
                Author = new AuthorDTO 
                {
                    AuthorId = book.AuthorId,
                    Name = book.Author.Name,
                    Description = book.Author.Description
                }
            });
        } 

        public async Task<BookDTO> GetBookById(int id) {
         
            var book = await _bookRepository.GetBookById(id);
            if (book == null) return null;

            return new BookDTO
            {
                BookId = book.BookId,
                Title = book.Title,
                Description = book.Description,
                AuthorId = book.AuthorId,
                Author = new AuthorDTO 
                {
                    AuthorId = book.AuthorId,
                    Name = book.Author.Name,
                    Description = book.Author.Description
                }
            };
            //return _bookRepository.GetBookById(id);   
        }

        public async Task AddBook(BookDTO bookDTO) {
            

            var book = new Book
            {
                BookId = bookDTO.BookId,
                Title = bookDTO.Title,
                Description = bookDTO.Description,
                AuthorId = bookDTO.AuthorId
            };

            if (bookDTO.AuthorId == 0)
            {
                book.Author = new Author {
                        AuthorId = bookDTO.AuthorId,
                        Name = bookDTO.Author.Name,
                        Description = bookDTO.Author.Description
                    };
            }
            await _bookRepository.AddBook(book);
        }

        public async Task UpdateBook(BookDTO bookDTO) {

            var book = new Book
            {
                BookId = bookDTO.BookId,
                Title = bookDTO.Title,
                Description = bookDTO.Description,
                AuthorId = bookDTO.AuthorId,
                Author =  new Author {
                    AuthorId = bookDTO.AuthorId,
                    Name = bookDTO.Author.Name,
                    Description = bookDTO.Author.Description
                }
            };

            await _bookRepository.UpdateBook(book);
        }

        public async Task DeleteBook(int id) => await _bookRepository.DeleteBook(id);

        public async Task<IEnumerable<BookDTO>> GetBooksByAuthor(int authId)
        {
            var books = await _bookRepository.GetBooksByAuthor(authId);  
            return books.Select(book => new BookDTO {
                BookId = book.BookId,
                Title = book.Title,
                Description = book.Description,
                AuthorId = book.AuthorId,
                Author = new AuthorDTO 
                {
                    AuthorId = book.AuthorId,
                    Name = book.Author.Name,
                    Description = book.Author.Description
                }
            });

        }
    }
}
