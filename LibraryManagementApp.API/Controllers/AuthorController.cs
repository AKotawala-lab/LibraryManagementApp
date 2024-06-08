using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LibraryManagementApp.Core.Interfaces;
using LibraryManagementApp.Core.Models;
using LibraryManagementApp.Core.Services;


namespace LibraryManagementApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController(IAuthorService authorService) : ControllerBase
    {
        private readonly IAuthorService _authorService = authorService;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthorDTO>>> GetAllAuthors()
        {
            var authors = await _authorService.GetAllAuthors();
            return Ok(authors);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorDTO>> GetAuthorById(int id)
        {
            var author = await _authorService.GetAuthorById(id);
            if (author == null)
            {
                return NotFound();
            }
            return Ok(author);
        }

        [HttpPost]
        public async Task<ActionResult<AuthorDTO>> AddAuthor([FromBody] AuthorDTO author)
        {
            await _authorService.AddAuthor(author);
            return CreatedAtAction(nameof(GetAuthorById), new { id = author.AuthorId }, author);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAuthor(int id, [FromBody] AuthorDTO author)
        {
            if (id != author.AuthorId)
            {
                return BadRequest();
            }

            await _authorService.UpdateAuthor(author);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            await _authorService.DeleteAuthor(id);
            return NoContent();
        }
    }
}
