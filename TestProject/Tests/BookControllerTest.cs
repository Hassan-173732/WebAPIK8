using Demo.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using Xunit;

namespace Tests
{
    public class BookControllerTests
    {
        [Fact]
        public void Get_ReturnsListOfBooks()
        {
            // Arrange
            var controller = new BookController();

            // Act
            var result = controller.Get();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var books = Assert.IsAssignableFrom<System.Collections.Generic.List<Book>>(okResult.Value);
            Assert.Equal(2, books.Count);
        }

        [Fact]
        public void Get_WithValidId_ReturnsBook()
        {
            // Arrange
            var controller = new BookController();
            int validId = 1;

            // Act
            var result = controller.Get(validId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var book = Assert.IsType<Book>(okResult.Value);
            Assert.Equal(validId, book.Id);
        }

        [Fact]
        public void Get_WithInvalidId_ReturnsNotFound()
        {
            // Arrange
            var controller = new BookController();
            int invalidId = 999;

            // Act
            var result = controller.Get(invalidId);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        // Similarly, you can write test cases for POST, PUT, DELETE operations
        // Ensure to cover various scenarios like valid inputs, invalid inputs, etc.
    }
}
