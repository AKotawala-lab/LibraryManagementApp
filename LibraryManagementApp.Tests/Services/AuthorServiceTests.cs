using System.Collections.Generic;
using LibraryManagementApp.Core.Entities;
using LibraryManagementApp.Core.Interfaces;
using LibraryManagementApp.Core.Services.Implementations;
using Moq;
using Xunit;

namespace LibraryManagementApp.Tests.Services
{
    public class AuthorServiceTests
    {
        private readonly Mock<IAuthorRepository> _mockAuthorRepository;
        private readonly AuthorService _authorService;

        public AuthorServiceTests()
        {
            _mockAuthorRepository = new Mock<IAuthorRepository>();
            _authorService = new AuthorService(_mockAuthorRepository.Object);
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
            _mockAuthorRepository.Setup(repo => repo.GetAllAuthors()).Returns(authors);

            // Act
            var result = _authorService.GetAllAuthors();

            // Assert
            //Assert.Equal(2, result.Count);
            Assert.Contains(result, a => a.Name == "Author 1");
            Assert.Contains(result, a => a.Name == "Author 2");
        }

        [Fact]
        public void GetAuthorById_ShouldReturnAuthor()
        {
            // Arrange
            var author = new Author { AuthorId = 1, Name = "Author 1", Description = "Description 1" };
            _mockAuthorRepository.Setup(repo => repo.GetAuthorById(1)).Returns(author);

            // Act
            var result = _authorService.GetAuthorById(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Author 1", result.Name);
        }

        [Fact]
        public void AddAuthor_ShouldAddAuthor()
        {
            // Arrange
            var author = new Author { AuthorId = 3, Name = "Author 3", Description = "Description 3" };

            // Act
            _authorService.AddAuthor(author);

            // Assert
            _mockAuthorRepository.Verify(repo => repo.AddAuthor(It.IsAny<Author>()), Times.Once);
        }

        [Fact]
        public void UpdateAuthor_ShouldUpdateAuthor()
        {
            // Arrange
            var author = new Author { AuthorId = 1, Name = "Updated Author", Description = "Updated Description" };

            // Act
            _authorService.UpdateAuthor(author);

            // Assert
            _mockAuthorRepository.Verify(repo => repo.UpdateAuthor(It.IsAny<Author>()), Times.Once);
        }

        [Fact]
        public void DeleteAuthor_ShouldDeleteAuthor()
        {
            // Act
            _authorService.DeleteAuthor(1);

            // Assert
            _mockAuthorRepository.Verify(repo => repo.DeleteAuthor(1), Times.Once);
        }
    }
}
