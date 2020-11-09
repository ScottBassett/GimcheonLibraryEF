using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GimcheonLibraryEF.DataAccess.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public int AuthorId { get; set; }
        public Author Author { get; set; }

        public string Description { get; set; }

        [Required]
        [Display(Name = "Total Copies")]
        public int TotalCopies { get; set; }

        [Required]
        [Display(Name = "Available Copies")]
        public int AvailableCopies { get; set; }

        public string ImageUrl { get; set; }
    }
}
