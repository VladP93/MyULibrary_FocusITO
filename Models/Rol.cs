using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyULibrary.Models
{
    public class Rol
    {
        [Key]
        public int IdRol { get; set; }
        [Required]
        [StringLength(100)]
        public string RolName { get; set; }
        public ICollection<Person> Persons{ get; set; }
    }
}
