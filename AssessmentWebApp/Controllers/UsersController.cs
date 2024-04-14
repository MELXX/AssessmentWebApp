using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AssessmentWebApp.Data;
using AssessmentWebApp.Data.Models;
using System.Diagnostics;
using System.IO;
using AssessmentWebApp.Helpers;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AssessmentWebApp.Controllers
{
    public class UsersController : Controller
    {
        private readonly AppDbContext _context;

        public UsersController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> FileUp(IFormFile postedFile)
        {
            if (postedFile == null || postedFile.Length == 0)
                return BadRequest("No file selected for upload...");

            string fileName = Path.GetFileName(postedFile.FileName);
            string contentType = postedFile.ContentType;
            string csv = string.Empty;

            using (var reader = new StreamReader(postedFile.OpenReadStream()))
            {
                string row;
                while ((row = reader.ReadLine()) != null)
                {
                   csv += row +'\n';
                }
            }

            var data = CsvParser.LoadDataFromCSV();
            var dbName = _context.Database.GetDbConnection().Database;
            if (data != default)
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    await _context.User.AddRangeAsync(data);
                    await _context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT dbo.[User] ON");
                    await _context.SaveChangesAsync();
                    await _context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT dbo.[User] OFF");
                    transaction.Commit();
                }

            }
            else
            {
                return BadRequest();
            }


            return RedirectToAction(nameof(Index));
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
              return _context.User != null ? 
                          View(await _context.User.AsNoTracking().ToListAsync()) :
                          Problem("Entity set 'AppDbContext.User'  is null.");
        }



        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.User == null)
            {
                return NotFound();
            }

            var user = await _context.User.AsNoTracking()
                .FirstOrDefaultAsync(m => m.Identifier == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Identifier,Username,FirstName,LastName,LoginEmail,Date,Values")] User user)
        {
            if (ModelState.IsValid)
            {
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.User == null)
            {
                return NotFound();
            }

            var user = await _context.User.AsNoTracking().FirstAsync(x=>x.Identifier == id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Identifier,Username,FirstName,LastName,LoginEmail,Date,Values")] User user)
        {
            if (id != user.Identifier)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Identifier))
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
            if (id == null || _context.User == null)
            {
                return NotFound();
            }

            var user = await _context.User
                .FirstOrDefaultAsync(m => m.Identifier == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.User == null)
            {
                return Problem("Entity set 'AppDbContext.User'  is null.");
            }
            var user = await _context.User.FindAsync(id);
            if (user != null)
            {
                _context.User.Remove(user);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
          return (_context.User?.AsNoTracking().Any(e => e.Identifier == id)).GetValueOrDefault();
        }
    }
}
