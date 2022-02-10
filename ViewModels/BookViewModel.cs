using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyULibrary.ViewModels
{
    public class BookViewModel
    {
        public int Idbook { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public string ImageURL { get; set; }
        public int Stock { get; set; }
        public DateTime PublishedYear { get; set; }
        public int IdGenre { get; set; }
        public string Genre { get; set; }
    }
}
