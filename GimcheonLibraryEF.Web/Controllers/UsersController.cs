﻿using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GimcheonLibraryEF.DataAccess;
using GimcheonLibraryEF.DataAccess.Models;
using GimcheonLibraryEF.Web.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.VisualStudio.Web.CodeGeneration;

namespace GimcheonLibraryEF.Web.Controllers
{
    public class UsersController : Controller
    {
        private readonly GimcheonLibraryDbContext _context;
        private readonly IHostingEnvironment _hostingEnvironment;

        public UsersController(GimcheonLibraryDbContext context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            return View(await _context.LibraryUsers.ToListAsync());
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var user = await _context.LibraryUsers
                .FirstOrDefaultAsync(m => m.Id == id);

            if (user == null) return NotFound();

            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,Email")] User user)
        {
            //if (ModelState.IsValid)
            //{
            //    string uniqueFileName = null;
            //    if (model.Photo != null)
            //    {
            //       var uploadsFolder =  Path.Combine(_hostingEnvironment.WebRootPath + "images");
            //       uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
            //       string filePath = Path.Combine(uploadsFolder, uniqueFileName);
            //       model.Photo.CopyTo(new FileStream(filePath, FileMode.Create));
            //    }

            //    User newUser = new User
            //    {
            //        FirstName = model.FirstName,
            //        LastName = model.LastName,
            //        Email = model.Email,
            //        PhotoPath = uniqueFileName
            //    };
            //    _context.Add(newUser);
            //    await _context.SaveChangesAsync();
            //    // return RedirectToAction(nameof(Details));
            //    return RedirectToAction("Details", new {id = newUser.Id});
            //}

            if (ModelState.IsValid)
            {
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", new { id = user.Id });
            }

            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var user = await _context.LibraryUsers.FindAsync(id);

            if (user == null) return NotFound();

            return View(user);
        }

        // POST: Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,Email")] User user)
        {
            if (id != user.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Id))
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
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var user = await _context.LibraryUsers.FirstOrDefaultAsync(m => m.Id == id);
            
            if (user == null) return NotFound();

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _context.LibraryUsers.FindAsync(id);
            _context.LibraryUsers.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
            return _context.LibraryUsers.Any(e => e.Id == id);
        }
    }
}