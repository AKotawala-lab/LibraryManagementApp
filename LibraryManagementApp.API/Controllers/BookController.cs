using Microsoft.AspNetCore.Mvc;
using LibraryManagementApp.Core.Entities;
using LibraryManagementApp.Core.Interfaces;
using LibraryManagementApp.Core.Models;
using System.Collections.Generic;

namespace LibraryManagementApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookDTO>>> GetAllBooks()
        {
            var books = await _bookService.GetAllBooks();
            return Ok(books);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BookDTO>> GetBookById(int id)
        {
            var book = await _bookService.GetBookById(id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }

        [HttpPost]
        public async Task<ActionResult<BookDTO>> AddBook([FromBody] BookDTO book)
        {
            await _bookService.AddBook(book);
            return CreatedAtAction(nameof(GetBookById), new { id = book.BookId }, book);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, [FromBody] BookDTO book)
        {
            if (id != book.BookId)
            {
                return BadRequest();
            }

            await _bookService.UpdateBook(book);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            await _bookService.DeleteBook(id);
            return NoContent();
        }

        [HttpGet("author/{authId}")]
        public async Task<ActionResult<IEnumerable<BookDTO>>> GetBooksByAuthor(int authId)
        {
            var books = await _bookService.GetBooksByAuthor(authId);
            return Ok(books);
        }

    }
}
