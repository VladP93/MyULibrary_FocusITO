using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyULibrary.Models
{
    public class BookRegistry
    {
        [Key]
        public int IdBookRegistry { get; set; }
        [Required]
        public DateTime DateCheckout { get; set; }
        [Required]
        public DateTime DateReturn { get; set; }
        [Required]
        public bool Returned { get; set; }
        public int IdBook { get; set; }
        public Book Book { get; set; }
        public int IdStudent { get; set; }
        public Person Student { get; set; }
    }
}
