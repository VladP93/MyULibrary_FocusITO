using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyULibrary.Models
{
    public class Genre
    {
        [Key]
        public int IdGenre { get; set; }
        [Required]
        [StringLength(100)]
        public string GenreName { get; set; }
        public ICollection<Book> Books { get; set; }
    }
}
