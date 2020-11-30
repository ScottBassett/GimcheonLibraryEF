﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GimcheonLibraryEF.DataAccess.Models
{
    public class BorrowedBooks
    {
        [Key]
        public int Id { get; set; }

        public string UserId { get; set; }

        public int BookId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual Book Book { get; set; }
    }
}
