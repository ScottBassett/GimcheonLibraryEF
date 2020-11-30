using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GimcheonLibraryEF.DataAccess;
using GimcheonLibraryEF.DataAccess.Models;

namespace GimcheonLibraryEF.Web.Controllers
{
    public class BorrowedBooksController : Controller
    {
        private readonly GimcheonLibraryDbContext _context;

        public BorrowedBooksController(GimcheonLibraryDbContext context)
        {
            _context = context;
        }

        // GET: BorrowedBooks
        public async Task<IActionResult> Index()
        {
            var gimcheonLibraryDbContext = _context.BorrowedBooks.Include(b => b.ApplicationUser).Include(b => b.Book);
            return View(await gimcheonLibraryDbContext.ToListAsync());
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
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,BookId")] BorrowedBooks borrowedBooks)
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
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,BookId")] BorrowedBooks borrowedBooks)
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
    }
}
