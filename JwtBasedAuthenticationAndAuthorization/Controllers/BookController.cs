using JwtBasedAuthenticationAndAuthorization.Data;
using JwtBasedAuthenticationAndAuthorization.Entities;
using JwtBasedAuthenticationAndAuthorization.Payloads;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JwtBasedAuthenticationAndAuthorization.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BookController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("/create")]
        public async Task<IActionResult> CreateAsync([FromBody] BookCreateRequest bookCreateRequest)
        {
            var book = new Book
            {
                Name = bookCreateRequest.Name,
                Price = bookCreateRequest.Price,
                PublishedDate = bookCreateRequest.PublishedDate
            };

            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();
            return Created($"/get/{book.Id}", book);
        }

        [HttpGet]
        [Route("/get/{id:long}")]
        public async Task<IActionResult> GetAsync(long id)
        {
            var foundBook = await _context.Books.FindAsync(id);
            if (foundBook is null) return NotFound($"Book not found with id {id}");
            return Ok(foundBook);
        }

        [HttpGet]
        [Route("/get/all")]
        public async Task<IActionResult> GetAllAsync()
        {
            var bookList = await _context.Books.ToListAsync();
            return Ok(bookList);
        }

        [HttpPut]
        [Route("/update/{id:long}")]
        public async Task<IActionResult> UpdateAsync(long id, [FromBody] BookCreateRequest bookCreateRequest)
        {
            var foundBook = await _context.Books.FindAsync(id);
            if (foundBook is null) return NotFound($"Book not found with id {id}");
            foundBook.Name = bookCreateRequest.Name;
            foundBook.Price = bookCreateRequest.Price;
            foundBook.PublishedDate = bookCreateRequest.PublishedDate;
            await _context.SaveChangesAsync();
            return Ok(foundBook);
        }

        [HttpDelete]
        [Route("/delete/{id:long}")]
        public async Task<IActionResult> DeleteAsync(long id)
        {
            var foundBook = await _context.Books.FindAsync(id);
            if (foundBook is null) return NotFound($"Book not found with id {id}");
            _context.Books.Remove(foundBook);
            await _context.SaveChangesAsync();
            return Ok($"Book with id {id} deleted successfully");
        }
    }
}
