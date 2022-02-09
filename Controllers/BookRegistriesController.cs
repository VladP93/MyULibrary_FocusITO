using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyULibrary.Models;

namespace MyULibrary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookRegistriesController : ControllerBase
    {
        private readonly MyDbContext _context;

        public BookRegistriesController(MyDbContext context)
        {
            _context = context;
        }

        // GET: api/BookRegistries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookRegistry>>> GetBookRegistry()
        {
            return await _context.BookRegistry.ToListAsync();
        }

        // GET: api/BookRegistries/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BookRegistry>> GetBookRegistry(int id)
        {
            var bookRegistry = await _context.BookRegistry.FindAsync(id);

            if (bookRegistry == null)
            {
                return NotFound();
            }

            return bookRegistry;
        }

        // PUT: api/BookRegistries/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBookRegistry(int id, BookRegistry bookRegistry)
        {
            if (id != bookRegistry.IdBookRegistry)
            {
                return BadRequest();
            }

            _context.Entry(bookRegistry).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookRegistryExists(id))
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

        // POST: api/BookRegistries
        [HttpPost]
        public async Task<ActionResult<BookRegistry>> PostBookRegistry(BookRegistry bookRegistry)
        {
            _context.BookRegistry.Add(bookRegistry);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBookRegistry", new { id = bookRegistry.IdBookRegistry }, bookRegistry);
        }

        // DELETE: api/BookRegistries/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<BookRegistry>> DeleteBookRegistry(int id)
        {
            var bookRegistry = await _context.BookRegistry.FindAsync(id);
            if (bookRegistry == null)
            {
                return NotFound();
            }

            _context.BookRegistry.Remove(bookRegistry);
            await _context.SaveChangesAsync();

            return bookRegistry;
        }

        private bool BookRegistryExists(int id)
        {
            return _context.BookRegistry.Any(e => e.IdBookRegistry == id);
        }
    }
}
