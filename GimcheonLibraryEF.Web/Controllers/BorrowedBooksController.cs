using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GimcheonLibraryEF.DataAccess;
using GimcheonLibraryEF.DataAccess.Models;
using Microsoft.AspNetCore.Identity;

namespace GimcheonLibraryEF.Web.Controllers
{
    public class BorrowedBooksController : Controller
    {
        private readonly GimcheonLibraryDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public BorrowedBooksController(GimcheonLibraryDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: BorrowedBooks
        [Route("/MyBooks")]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);

            var books = _context.BorrowedBooks
                .Include(b => b.ApplicationUser)
                .Include(b => b.Book)
                .Where(b => b.UserId == user.Id)
                .ToListAsync();

            return View(await books);
        }

        // GET: BorrowedBooks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var borrowedBooks = await _context.BorrowedBooks
                .Include(b => b.ApplicationUser)
                .Include(b => b.Book)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (borrowedBooks == null)
            {
                return NotFound();
            }

            return View(borrowedBooks);
        }

        public async Task<IActionResult> ReturnBook(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return NotFound();
            if (id == 0) return NotFound();

            var borrowedBook = await _context.BorrowedBooks
                .Include(b => b.Book)
                .SingleOrDefaultAsync(b => b.Id == id);

            if (borrowedBook == null) return NotFound();

            borrowedBook.Book.AvailableCopies++;

             _context.BorrowedBooks.Remove(borrowedBook);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

    }
}
