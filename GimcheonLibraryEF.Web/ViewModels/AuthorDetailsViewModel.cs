using System.Collections.Generic;
using GimcheonLibraryEF.DataAccess.Models;

namespace GimcheonLibraryEF.Web.ViewModels
{
    public class AuthorDetailsViewModel
    {
        public Author Author { get; set; }
        public IEnumerable<Book> Books { get; set; }
    }
}
