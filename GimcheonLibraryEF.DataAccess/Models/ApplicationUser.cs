using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace GimcheonLibraryEF.DataAccess.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string City { get; set; }
        public virtual List<BorrowedBook> BorrowedBooks { get; set; }
    }
}
