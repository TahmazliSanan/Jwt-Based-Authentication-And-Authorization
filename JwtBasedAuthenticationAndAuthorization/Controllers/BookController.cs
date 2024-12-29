using JwtBasedAuthenticationAndAuthorization.Data;
using JwtBasedAuthenticationAndAuthorization.Entities;
using JwtBasedAuthenticationAndAuthorization.Payloads.Book;
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
                PublishedDate = bookCreateRequest.PublishedDate,
                CreatedDateTime = DateTime.UtcNow
            };

            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();

            var bookResponse = new BookResponse
            {
                Message = "Book created successfully",
                Id = book.Id,
                Name = book.Name,
                Price = book.Price,
                PublishedDate = book.PublishedDate,
                CreatedDateTime = book.CreatedDateTime,
                ModifiedDateTime = book.ModifiedDateTime
            };

            return Created($"/get/{bookResponse.Id}", bookResponse);
        }

        [HttpGet]
        [Route("/get/{id:long}")]
        public async Task<IActionResult> GetAsync(long id)
        {
            BookResponse bookResponse;
            var foundBook = await _context.Books.FindAsync(id);
            
            if (foundBook is null)
            {
                bookResponse = new BookResponse
                {
                    Message = $"Book not found with id {id}",
                    Id = id
                };
            }
            else
            {
                bookResponse = new BookResponse
                {
                    Message = "Book found",
                    Id = id,
                    Name = foundBook.Name,
                    Price = foundBook.Price,
                    PublishedDate = foundBook.PublishedDate,
                    CreatedDateTime = foundBook.CreatedDateTime,
                    ModifiedDateTime = foundBook.ModifiedDateTime
                };
            }

            return Ok(bookResponse);
        }

        [HttpGet]
        [Route("/get/all")]
        public async Task<IActionResult> GetAllAsync()
        {
            var bookList = await _context.Books.ToListAsync();

            var bookListResponse = bookList.Select(book => new BookResponse
            {
                Message = "Book list retrieved successfully",
                Id = book.Id,
                Name = book.Name,
                Price = book.Price,
                PublishedDate = book.PublishedDate,
                CreatedDateTime = book.CreatedDateTime,
                ModifiedDateTime = book.ModifiedDateTime
            }).ToList();

            return Ok(bookListResponse);
        }

        [HttpPut]
        [Route("/update/{id:long}")]
        public async Task<IActionResult> UpdateAsync(long id, [FromBody] BookUpdateRequest bookUpdateRequest)
        {
            BookResponse bookResponse;
            var foundBook = await _context.Books.FindAsync(id);
            
            if (foundBook is null)
            {
                bookResponse = new BookResponse
                {
                    Message = $"Book not found with id {id}",
                    Id = id
                };
            }
            else
            {
                foundBook.Name = bookUpdateRequest.Name;
                foundBook.Price = bookUpdateRequest.Price;
                foundBook.PublishedDate = bookUpdateRequest.PublishedDate;
                foundBook.ModifiedDateTime = DateTime.UtcNow;

                await _context.SaveChangesAsync();

                bookResponse = new BookResponse
                {
                    Message = "Book updated successfully",
                    Id = id,
                    Name = foundBook.Name,
                    Price = foundBook.Price,
                    PublishedDate = foundBook.PublishedDate,
                    CreatedDateTime = foundBook.CreatedDateTime,
                    ModifiedDateTime = foundBook.ModifiedDateTime
                };
            }

            return Ok(bookResponse);
        }

        [HttpDelete]
        [Route("/delete/{id:long}")]
        public async Task<IActionResult> DeleteAsync(long id)
        {
            BookResponse bookResponse;
            var foundBook = await _context.Books.FindAsync(id);

            if (foundBook is null)
            {
                bookResponse = new BookResponse
                {
                    Message = $"Book not found with id {id}",
                    Id = id
                };
            }
            else
            {
                _context.Books.Remove(foundBook);
                await _context.SaveChangesAsync();

                bookResponse = new BookResponse
                {
                    Message = "Book deleted successfully",
                    Id = id
                };
            }

            return Ok(bookResponse);
        }
    }
}
