using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
            var user = await _userManager.GetUserAsync(HttpContext.User);

            var booksQuery = _context.BorrowedBooks
                .Include(b => b.ApplicationUser)
                .Include(b => b.Book)
                .Where(b => b.UserId == user.Id)
                .ToListAsync();

            return View(await booksQuery);
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

        // GET: BorrowedBooks/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["BookId"] = new SelectList(_context.Books, "Id", "Title");
            return View();
        }

        // POST: BorrowedBooks/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,BookId")] BorrowedBook borrowedBooks)
        {
            if (ModelState.IsValid)
            {
                _context.Add(borrowedBooks);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", borrowedBooks.UserId);
            ViewData["BookId"] = new SelectList(_context.Books, "Id", "Title", borrowedBooks.BookId);
            return View(borrowedBooks);
        }

        // GET: BorrowedBooks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var borrowedBooks = await _context.BorrowedBooks.FindAsync(id);
            if (borrowedBooks == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", borrowedBooks.UserId);
            ViewData["BookId"] = new SelectList(_context.Books, "Id", "Title", borrowedBooks.BookId);
            return View(borrowedBooks);
        }

        // POST: BorrowedBooks/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,BookId")] BorrowedBook borrowedBooks)
        {
            if (id != borrowedBooks.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(borrowedBooks);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BorrowedBooksExists(borrowedBooks.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", borrowedBooks.UserId);
            ViewData["BookId"] = new SelectList(_context.Books, "Id", "Title", borrowedBooks.BookId);
            return View(borrowedBooks);
        }

        // GET: BorrowedBooks/Delete/5
        public async Task<IActionResult> Delete(int? id)
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

        // POST: BorrowedBooks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var borrowedBooks = await _context.BorrowedBooks.FindAsync(id);
            _context.BorrowedBooks.Remove(borrowedBooks);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BorrowedBooksExists(int id)
        {
            return _context.BorrowedBooks.Any(e => e.Id == id);
        }

        public async Task<IActionResult> CheckBookOut(ApplicationUser user, Book book)
        {
           // var user = await _userManager.GetUserAsync(HttpContext.User);

           Boo


            return View(await booksQuery);
        }
    }
}
