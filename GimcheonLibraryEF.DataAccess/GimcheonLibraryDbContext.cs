using GimcheonLibraryEF.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace GimcheonLibraryEF.DataAccess
{
    public class GimcheonLibraryDbContext : DbContext
    {
        public GimcheonLibraryDbContext(DbContextOptions<GimcheonLibraryDbContext> options)
            : base(options)
        { }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
