using System.Linq;
using GimcheonLibraryEF.DataAccess.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting.Internal;

namespace GimcheonLibraryEF.DataAccess
{
    public class GimcheonLibraryDbContext : IdentityDbContext<ApplicationUser>
    {
        public GimcheonLibraryDbContext(DbContextOptions<GimcheonLibraryDbContext> options)
            : base(options)
        { }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<BorrowedBook> BorrowedBooks { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            foreach (var foreignKey in builder.Model.GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys()))
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            }

            builder.Entity<BorrowedBook>()
                .HasKey(bb => bb.Id);

            builder.Entity<BorrowedBook>()
                .HasOne(bb => bb.Book)
                .WithMany(b => b.BorrowedBooks)
                .HasForeignKey(bb => bb.BookId);

            builder.Entity<BorrowedBook>()
                .HasOne(bb => bb.ApplicationUser)
                .WithMany(c => c.BorrowedBooks)
                .HasForeignKey(bb => bb.UserId);
        }
    }
}
