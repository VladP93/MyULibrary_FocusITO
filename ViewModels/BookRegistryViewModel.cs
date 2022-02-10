using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyULibrary.ViewModels
{
    public class BookRegistryViewModel
    {
        public int IdBookRegistry { get; set; }
        public DateTime DateCheckout { get; set; }
        public DateTime DateReturn { get; set; }
        public bool Returned { get; set; }
        public int IdBook { get; set; }
        public string Book { get; set; }
        public int IdStudent { get; set; }
        public string Student { get; set; }
    }
}
