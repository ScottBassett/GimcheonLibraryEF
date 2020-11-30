using System.ComponentModel.DataAnnotations;

namespace GimcheonLibraryEF.DataAccess.Models
{
    public class BorrowedBooks
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }
    }
}
