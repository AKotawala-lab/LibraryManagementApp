using System.Collections.Generic;
using LibraryManagementApp.Core.Entities;
using LibraryManagementApp.Core.Interfaces;
using LibraryManagementApp.Core.Services.Implementations;
using Moq;
using Xunit;

namespace LibraryManagementApp.Tests.Services
{
    public class BookServiceTests
    {
        private readonly Mock<IBookRepository> _mockBookRepository;
        private readonly BookService _bookService;

        public BookServiceTests()
        {
            _mockBookRepository = new Mock<IBookRepository>();
            _bookService = new BookService(_mockBookRepository.Object);
        }

        [Fact]
        public void GetAllBooks_ShouldReturnAllBooks()
        {
            // Arrange
            var books = new List<Book>
            {
                new Book { BookId = 1, Title = "Book 1", Description = "Description 1", AuthorId = 1 },
                new Book { BookId = 2, Title = "Book 2", Description = "Description 2", AuthorId = 2 }
            };
            _mockBookRepository.Setup(repo => repo.GetAllBooks()).Returns(books);

            // Act
            var result = _bookService.GetAllBooks();

            // Assert
            //Assert.Equal(2, result.Count);
            Assert.Contains(result, b => b.Title == "Book 1");
            Assert.Contains(result, b => b.Title == "Book 2");
        }

        [Fact]
        public void GetBookById_ShouldReturnBook()
        {
            // Arrange
            var book = new Book { BookId = 1, Title = "Book 1", Description = "Description 1", AuthorId = 1 };
            _mockBookRepository.Setup(repo => repo.GetBookById(1)).Returns(book);

            // Act
            var result = _bookService.GetBookById(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Book 1", result.Title);
        }

        [Fact]
        public void AddBook_ShouldAddBook()
        {
            // Arrange
            var book = new Book { BookId = 3, Title = "Book 3", Description = "Description 3", AuthorId = 1 };

            // Act
            _bookService.AddBook(book);

            // Assert
            _mockBookRepository.Verify(repo => repo.AddBook(It.IsAny<Book>()), Times.Once);
        }

        [Fact]
        public void UpdateBook_ShouldUpdateBook()
        {
            // Arrange
            var book = new Book { BookId = 1, Title = "Updated Book", Description = "Updated Description", AuthorId = 1 };

            // Act
            _bookService.UpdateBook(book);

            // Assert
            _mockBookRepository.Verify(repo => repo.UpdateBook(It.IsAny<Book>()), Times.Once);
        }

        [Fact]
        public void DeleteBook_ShouldDeleteBook()
        {
            // Act
            _bookService.DeleteBook(1);

            // Assert
            _mockBookRepository.Verify(repo => repo.DeleteBook(1), Times.Once);
        }
    }
}
