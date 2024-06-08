using System.Collections.Generic;
using LibraryManagementApp.Api.Controllers;
using LibraryManagementApp.Core.Entities;
using LibraryManagementApp.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace LibraryManagementApp.Tests.Controllers
{
    public class AuthorControllerTests
    {
        private readonly Mock<IAuthorService> _mockAuthorService;
        private readonly AuthorController _authorController;

        public AuthorControllerTests()
        {
            _mockAuthorService = new Mock<IAuthorService>();
            _authorController = new AuthorController(_mockAuthorService.Object);
        }

        [Fact]
        public void GetAllAuthors_ShouldReturnAllAuthors()
        {
            // Arrange
            var authors = new List<Author>
            {
                new Author { AuthorId = 1, Name = "Author 1", Description = "Description 1" },
                new Author { AuthorId = 2, Name = "Author 2", Description = "Description 2" }
            };
            _mockAuthorService.Setup(service => service.GetAllAuthors()).Returns(authors);

            // Act
            var result = _authorController.GetAllAuthors();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnAuthors = Assert.IsType<List<Author>>(okResult.Value);
            Assert.Equal(2, returnAuthors.Count);
        }

        [Fact]
        public void GetAuthorById_ShouldReturnAuthor()
        {
            // Arrange
            var author = new Author { AuthorId = 1, Name = "Author 1", Description = "Description 1" };
            _mockAuthorService.Setup(service => service.GetAuthorById(1)).Returns(author);

            // Act
            var result = _authorController.GetAuthorById(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnAuthor = Assert.IsType<Author>(okResult.Value);
            Assert.Equal("Author 1", returnAuthor.Name);
        }

        [Fact]
        public void AddAuthor_ShouldAddAuthor()
        {
            // Arrange
            var author = new Author { AuthorId = 3, Name = "Author 3", Description = "Description 3" };

            // Act
            var result = _authorController.AddAuthor(author);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var returnAuthor = Assert.IsType<Author>(createdAtActionResult.Value);
            Assert.Equal("Author 3", returnAuthor.Name);
        }

        [Fact]
        public void UpdateAuthor_ShouldUpdateAuthor()
        {
            // Arrange
            var author = new Author { AuthorId = 1, Name = "Updated Author", Description = "Updated Description" };

            // Act
            var result = _authorController.UpdateAuthor(1, author);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void DeleteAuthor_ShouldDeleteAuthor()
        {
            // Act
            var result = _authorController.DeleteAuthor(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }
    }
}
