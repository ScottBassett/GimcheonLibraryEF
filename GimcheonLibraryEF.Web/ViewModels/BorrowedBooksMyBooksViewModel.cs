using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GimcheonLibraryEF.DataAccess.Models;

namespace GimcheonLibraryEF.Web.ViewModels
{
    public class BorrowedBooksMyBooksViewModel
    {
        public ApplicationUser User { get; set; }
        public BorrowedBook BorrowedBooks { get; set; }
    }
}
