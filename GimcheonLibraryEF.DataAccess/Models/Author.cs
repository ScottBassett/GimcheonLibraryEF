using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GimcheonLibraryEF.DataAccess.Models
{
    public class Author
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string About { get; set; }

        public virtual List<Book> Books { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
