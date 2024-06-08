using System.Collections.Generic;
using LibraryManagementApp.Api.Controllers;
using LibraryManagementApp.Core.Entities;
using LibraryManagementApp.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace LibraryManagementApp.Tests.Controllers
{
    public class BookControllerTests
    {
        private readonly Mock<IBookService> _mockBookService;
        private readonly BookController _bookController;

        public BookControllerTests()
        {
            _mockBookService = new Mock<IBookService>();
            _bookController = new BookController(_mockBookService.Object);
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
            _mockBookService.Setup(service => service.GetAllBooks()).Returns(books);

            // Act
            var result = _bookController.GetAllBooks();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnBooks = Assert.IsType<List<Book>>(okResult.Value);
            Assert.Equal(2, returnBooks.Count);
        }

        [Fact]
        public void GetBookById_ShouldReturnBook()
        {
            // Arrange
            var book = new Book { BookId = 1, Title = "Book 1", Description = "Description 1", AuthorId = 1 };
            _mockBookService.Setup(service => service.GetBookById(1)).Returns(book);

            // Act
            var result = _bookController.GetBookById(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnBook = Assert.IsType<Book>(okResult.Value);
            Assert.Equal("Book 1", returnBook.Title);
        }

        [Fact]
        public void AddBook_ShouldAddBook()
        {
            // Arrange
            var book = new Book { BookId = 3, Title = "Book 3", Description = "Description 3", AuthorId = 1 };

            // Act
            var result = _bookController.AddBook(book);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var returnBook = Assert.IsType<Book>(createdAtActionResult.Value);
            Assert.Equal("Book 3", returnBook.Title);
        }

        [Fact]
        public void UpdateBook_ShouldUpdateBook()
        {
            // Arrange
            var book = new Book { BookId = 1, Title = "Updated Book", Description = "Updated Description", AuthorId = 1 };

            // Act
            var result = _bookController.UpdateBook(1, book);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void DeleteBook_ShouldDeleteBook()
        {
            // Act
            var result = _bookController.DeleteBook(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }
    }
}
