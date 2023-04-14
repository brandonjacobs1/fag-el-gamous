using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using fag_el_gamous.Data;
using fag_el_gamous.Models;
using Microsoft.AspNetCore.Authorization;

namespace fag_el_gamous.Views
{
    public class C14Controller : Controller
    {
        private readonly postgresContext _context;

        public C14Controller(postgresContext context)
        {
            _context = context;
        }

        // GET: C14
        [Authorize(Roles = "Admin, Researcher")]
        public async Task<IActionResult> Index()
        {
              return _context.C14s != null ? 
                          View(await _context.C14s.ToListAsync()) :
                          Problem("Entity set 'postgresContext.C14s'  is null.");
        }

        // GET: C14/Details/5
        [Authorize(Roles = "Admin, Researcher")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.C14s == null)
            {
                return NotFound();
            }

            var c14 = await _context.C14s
                .FirstOrDefaultAsync(m => m.Sample == id);
            if (c14 == null)
            {
                return NotFound();
            }

            return View(c14);
        }

        // GET: C14/Create
        [Authorize(Roles = "Admin, Researcher")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: C14/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin, Researcher")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Sample,Squarenorthsouth,Northsouth,Squareeastwest,Eastwest,Area,Burialnumber,Description,Agebp,Calendardate")] C14 c14)
        {
            if (ModelState.IsValid)
            {
                _context.Add(c14);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(c14);
        }

        // GET: C14/Edit/5
        [Authorize(Roles = "Admin, Researcher")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.C14s == null)
            {
                return NotFound();
            }

            var c14 = await _context.C14s.FindAsync(id);
            if (c14 == null)
            {
                return NotFound();
            }
            return View(c14);
        }

        // POST: C14/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin, Researcher")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Sample,Squarenorthsouth,Northsouth,Squareeastwest,Eastwest,Area,Burialnumber,Description,Agebp,Calendardate")] C14 c14)
        {
            if (id != c14.Sample)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(c14);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!C14Exists(c14.Sample))
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
            return View(c14);
        }

        // GET: C14/Delete/5
        [Authorize(Roles = "Admin, Researcher")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.C14s == null)
            {
                return NotFound();
            }

            var c14 = await _context.C14s
                .FirstOrDefaultAsync(m => m.Sample == id);
            if (c14 == null)
            {
                return NotFound();
            }

            return View(c14);
        }

        // POST: C14/Delete/5
        [Authorize(Roles = "Admin, Researcher")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.C14s == null)
            {
                return Problem("Entity set 'postgresContext.C14s'  is null.");
            }
            var c14 = await _context.C14s.FindAsync(id);
            if (c14 != null)
            {
                _context.C14s.Remove(c14);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin, Researcher")]
        private bool C14Exists(int id)
        {
          return (_context.C14s?.Any(e => e.Sample == id)).GetValueOrDefault();
        }
    }
}
