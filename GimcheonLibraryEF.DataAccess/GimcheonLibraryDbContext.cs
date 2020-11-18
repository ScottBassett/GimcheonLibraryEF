using GimcheonLibraryEF.DataAccess.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GimcheonLibraryEF.DataAccess
{
    public class GimcheonLibraryDbContext : IdentityDbContext
    {
        public GimcheonLibraryDbContext(DbContextOptions<GimcheonLibraryDbContext> options)
            : base(options)
        { }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<User> LibraryUsers { get; set; }
    }
}
