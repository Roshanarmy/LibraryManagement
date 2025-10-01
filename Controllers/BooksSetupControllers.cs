using Microsoft.AspNetCore.Mvc;
using LibraryAPI.Data;
using LibraryAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Controllers
{
    [ApiController]
    [Route("[controller]/LibraryManagement")]
    public class BooksSetupController : ControllerBase
    {
        private readonly LibraryContext _context;

        public BooksSetupController(LibraryContext context)
        {
            _context = context;
        }

        
        // Getting Book by ID
        [HttpGet("GetBookById")]
        public IActionResult GetBookById([FromQuery] int id)
        {
            var book = _context.Books.FirstOrDefault(b => b.Id == id);
            if (book == null) return NotFound($"Book with Id {id} not found.");
            return Ok(book);
        }


        // Getting All book
        [HttpGet("GetAllBooksDetails")]
        public IActionResult GetAllBooks()
        {
            var books = _context.Books.ToList();
            return Ok(books);
        }


        [HttpPost("AddNewBook")]
        public async Task<IActionResult> AddBook([FromBody] Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
            //return CreatedAtAction(nameof(GetBookById), new { id = book.Id }, book);
            return Ok($"Book saved successfully with Id {book.Id}");
        }

     
        [HttpPut("UpdateBookDetails/{id}")]
        public async Task<IActionResult> UpdateBook(int id, [FromBody] Book updatedBook)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null) return NotFound($"Book with Id {id} not found.");

            book.Title = updatedBook.Title;
            book.Author = updatedBook.Author;
            book.Year = updatedBook.Year;

            await _context.SaveChangesAsync();
            return Ok(book);
        }

      //Delete the book by Id
        [HttpDelete("DeleteBook")]
        public async Task<IActionResult> DeleteBook([FromQuery] int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null) return NotFound($"Book with Id {id} not found.");

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return Ok($"Book with Id {id} deleted successfully.");
        }

    }
}
