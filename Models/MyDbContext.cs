using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyULibrary.Models
{
    public class MyDbContext:DbContext
    {
        public DbSet<Book> Book { get; set; }
        public DbSet<BookRegistry> BookRegistry { get; set; }
        public DbSet<Genre> Genre { get; set; }
        public DbSet<Person> Person { get; set; }
        public DbSet<Rol> Rol { get; set; }

        public MyDbContext(DbContextOptions<MyDbContext> options): base(options)
        {

        }
    }
}
