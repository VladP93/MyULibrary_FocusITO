using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    public class UsersController : ControllerBase
    {
        private readonly MyDbContext _context;

        public UsersController(MyDbContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<IEnumerable<PersonViewModel>> GetPerson()
        {
            var person = await _context.Person.Include(p=>p.Rol).ToListAsync();

            return person.Select(p => new PersonViewModel
            {
                IdPerson = p.IdPerson,
                FirstName = p.FirstName,
                LastName = p.LastName,
                Email = p.Email,
                Password = "*******",
                IdRol = p.IdRol,
                Rol = p.Rol.RolName
            });
        }

        // GET: api/Users/Librarians
        [HttpGet("Librarians")]
        public async Task<IEnumerable<PersonViewModel>> GetLibrarian()
        {
            var person =  await _context.Person.Include(p => p.Rol).Where(p => p.IdRol == 1).ToListAsync();

            return person.Select(p => new PersonViewModel
            {
                IdPerson = p.IdPerson,
                FirstName = p.FirstName,
                LastName = p.LastName,
                Email = p.Email,
                Password = "*******",
                IdRol = p.IdRol,
                Rol = p.Rol.RolName
            });
        }

        // GET: api/Users/Students
        [HttpGet("Students")]
        public async Task<IEnumerable<PersonViewModel>> GetStudent()
        {
            var person = await _context.Person.Include(p=>p.Rol).Where(p => p.IdRol == 2).ToListAsync();

            return person.Select(p => new PersonViewModel
            {
                IdPerson = p.IdPerson,
                FirstName = p.FirstName,
                LastName = p.LastName,
                Email = p.Email,
                Password = "*******",
                IdRol = p.IdRol,
                Rol = p.Rol.RolName
            });
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginViewModel person)
        {
            string email = person.Email;
            string password = person.Password;

            var _person = await _context.Person.Include(p => p.Rol).FirstOrDefaultAsync(p => p.Email == email);

            if (_person == null)
            {
                return NotFound();
            }

            if (!VerificarPasswordHash(password, _person.PasswordHash, _person.PasswordSalt))
            {
                return NotFound();
            }

            return Ok(new { loginStatus = "ok" });
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPerson(int id)
        {
            var person = await _context.Person.Include(r=>r.Rol).SingleOrDefaultAsync(p => p.IdPerson==id);

            if (person == null)
            {
                return NotFound();
            }

            return Ok(new PersonViewModel
            {
                    IdPerson = person.IdPerson,
                    FirstName = person.FirstName,
                    LastName = person.LastName,
                    Email = person.Email,
                    Password = "*******",
                    IdRol = person.IdRol,
                    Rol = person.Rol.RolName
        });
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPerson(int id, Person person)
        {
            if (id != person.IdPerson)
            {
                return BadRequest();
            }

            _context.Entry(person).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonExists(id))
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

        // POST: api/Users
        [HttpPost]
        public async Task<ActionResult<Person>> PostPerson(PersonViewModel person)
        {
            string password = person.Password;

            CrearPasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

            Person _person = new Person
            {
                FirstName = person.FirstName,
                LastName = person.LastName,
                Email = person.Email,
                IdRol = person.IdRol,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };

            _context.Person.Add(_person);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPerson", new { id = _person.IdPerson, response = "ok" });
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Person>> DeletePerson(int id)
        {
            var person = await _context.Person.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }

            _context.Person.Remove(person);
            await _context.SaveChangesAsync();

            return person;
        }

        private bool PersonExists(int id)
        {
            return _context.Person.Any(e => e.IdPerson == id);
        }

        private void CrearPasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerificarPasswordHash(string password, byte[] passwordHashAlmacenado, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var passwordHashNuevo = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return new ReadOnlySpan<byte>(passwordHashAlmacenado).SequenceEqual(new ReadOnlySpan<byte>(passwordHashNuevo));
            }
        }
    }
}
