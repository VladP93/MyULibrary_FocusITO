using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyULibrary.Models
{
    public class Book
    {
        [Key]
        public int Idbook { get; set; }
        [Required]
        [StringLength(100)]
        public string Title { get; set; }
        [Required]
        [StringLength(100)]
        public string Author { get; set; }
        [Required]
        public int Stock { get; set; }
        [Required]
        public DateTime PublishedYear { get; set; }
        public int IdGenre { get; set; }
        public Genre Genre{ get; set; }
        public ICollection<BookRegistry> Registries { get; set; }
    }
}
