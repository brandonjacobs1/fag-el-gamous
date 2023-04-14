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
    public class PhotodatumController : Controller
    {
        private readonly postgresContext _context;

        public PhotodatumController(postgresContext context)
        {
            _context = context;
        }

        // GET: Photodatum
        [Authorize(Roles = "Admin, Researcher")]
        public async Task<IActionResult> Index()
        {
              return _context.Photodata != null ? 
                          View(await _context.Photodata.ToListAsync()) :
                          Problem("Entity set 'postgresContext.Photodata'  is null.");
        }

        // GET: Photodatum/Details/5
        [Authorize(Roles = "Admin, Researcher")]
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.Photodata == null)
            {
                return NotFound();
            }

            var photodatum = await _context.Photodata
                .FirstOrDefaultAsync(m => m.Id == id);
            if (photodatum == null)
            {
                return NotFound();
            }

            return View(photodatum);
        }

        // GET: Photodatum/Create
        [Authorize(Roles = "Admin, Researcher")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Photodatum/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin, Researcher")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Description,Filename,Photodataid,Date,Url")] Photodatum photodatum)
        {
            if (ModelState.IsValid)
            {
                _context.Add(photodatum);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(photodatum);
        }

        // GET: Photodatum/Edit/5
        [Authorize(Roles = "Admin, Researcher")]
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.Photodata == null)
            {
                return NotFound();
            }

            var photodatum = await _context.Photodata.FindAsync(id);
            if (photodatum == null)
            {
                return NotFound();
            }
            return View(photodatum);
        }

        // POST: Photodatum/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin, Researcher")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Description,Filename,Photodataid,Date,Url")] Photodatum photodatum)
        {
            if (id != photodatum.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(photodatum);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PhotodatumExists(photodatum.Id))
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
            return View(photodatum);
        }

        // GET: Photodatum/Delete/5
        [Authorize(Roles = "Admin, Researcher")]
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.Photodata == null)
            {
                return NotFound();
            }

            var photodatum = await _context.Photodata
                .FirstOrDefaultAsync(m => m.Id == id);
            if (photodatum == null)
            {
                return NotFound();
            }

            return View(photodatum);
        }

        // POST: Photodatum/Delete/5
        [Authorize(Roles = "Admin, Researcher")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.Photodata == null)
            {
                return Problem("Entity set 'postgresContext.Photodata'  is null.");
            }
            var photodatum = await _context.Photodata.FindAsync(id);
            if (photodatum != null)
            {
                _context.Photodata.Remove(photodatum);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin, Researcher")]
        private bool PhotodatumExists(long id)
        {
          return (_context.Photodata?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
