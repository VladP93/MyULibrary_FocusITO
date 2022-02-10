using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyULibrary.Models;
using MyULibrary.ViewModels;

namespace MyULibrary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly MyDbContext _context;

        public BooksController(MyDbContext context)
        {
            _context = context;
        }

        // GET: api/Books
        [HttpGet]
        public async Task<IEnumerable<BookViewModel>> GetBook()
        {
            var book = await _context.Book.Include(b=>b.Genre).ToListAsync();

            return book.Select(b => new BookViewModel
            {
                Idbook = b.Idbook,
                Title = b.Title,
                Author = b.Author,
                Description = b.Description,
                ImageURL = b.ImageURL,
                Stock = b.Stock,
                PublishedYear = b.PublishedYear,
                IdGenre = b.IdGenre,
                Genre = b.Genre.GenreName
            });
        }

        // GET: api/Books/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBook(int id)
        {
            var book = await _context.Book.Include(b => b.Genre).SingleOrDefaultAsync(b=>b.Idbook==id);

            if(book == null)
            {
                return NotFound();
            }

            return Ok( new BookViewModel
            {
                Idbook = book.Idbook,
                Title = book.Title,
                Author = book.Author,
                Description = book.Description,
                ImageURL = book.ImageURL,
                Stock = book.Stock,
                PublishedYear = book.PublishedYear,
                IdGenre = book.IdGenre,
                Genre = book.Genre.GenreName
            });
        }

        // PUT: api/Books/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook(int id, Book book)
        {
            if (id != book.Idbook)
            {
                return BadRequest();
            }

            _context.Entry(book).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Books
        [HttpPost]
        public async Task<ActionResult<Book>> PostBook(Book book)
        {
            _context.Book.Add(book);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBook", new { id = book.Idbook }, book);
        }

        // DELETE: api/Books/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Book>> DeleteBook(int id)
        {
            var book = await _context.Book.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            _context.Book.Remove(book);
            await _context.SaveChangesAsync();

            return book;
        }

        private bool BookExists(int id)
        {
            return _context.Book.Any(e => e.Idbook == id);
        }
    }
}
