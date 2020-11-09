using System.ComponentModel.DataAnnotations;

namespace GimcheonLibraryEF.DataAccess.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Display(Name = "Author")]
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
