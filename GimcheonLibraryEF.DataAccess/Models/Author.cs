using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
