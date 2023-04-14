using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using fag_el_gamous.Data;
using fag_el_gamous.Models;

namespace fag_el_gamous.Views
{
    public class PhotodataTextileController : Controller
    {
        private readonly postgresContext _context;

        public PhotodataTextileController(postgresContext context)
        {
            _context = context;
        }

        // GET: PhotodataTextile
        public async Task<IActionResult> Index()
        {
            var postgresContext = _context.PhotodataTextiles.Include(p => p.Photodatum).Include(p => p.Textile);
            return View(await postgresContext.ToListAsync());
        }

        // GET: PhotodataTextile/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.PhotodataTextiles == null)
            {
                return NotFound();
            }

            var photodataTextile = await _context.PhotodataTextiles
                .Include(p => p.Photodatum)
                .Include(p => p.Textile)
                .FirstOrDefaultAsync(m => m.MainPhotodataid == id);
            if (photodataTextile == null)
            {
                return NotFound();
            }

            return View(photodataTextile);
        }

        // GET: PhotodataTextile/Create
        public IActionResult Create()
        {
            ViewData["MainPhotodataid"] = new SelectList(_context.Photodata, "Id", "Id");
            ViewData["MainTextileid"] = new SelectList(_context.Textiles, "Id", "Id");
            return View();
        }

        // POST: PhotodataTextile/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MainPhotodataid,MainTextileid")] PhotodataTextile photodataTextile)
        {
            if (ModelState.IsValid)
            {
                _context.Add(photodataTextile);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MainPhotodataid"] = new SelectList(_context.Photodata, "Id", "Id", photodataTextile.MainPhotodataid);
            ViewData["MainTextileid"] = new SelectList(_context.Textiles, "Id", "Id", photodataTextile.MainTextileid);
            return View(photodataTextile);
        }

        // GET: PhotodataTextile/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.PhotodataTextiles == null)
            {
                return NotFound();
            }

            var photodataTextile = await _context.PhotodataTextiles.FindAsync(id);
            if (photodataTextile == null)
            {
                return NotFound();
            }
            ViewData["MainPhotodataid"] = new SelectList(_context.Photodata, "Id", "Id", photodataTextile.MainPhotodataid);
            ViewData["MainTextileid"] = new SelectList(_context.Textiles, "Id", "Id", photodataTextile.MainTextileid);
            return View(photodataTextile);
        }

        // POST: PhotodataTextile/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("MainPhotodataid,MainTextileid")] PhotodataTextile photodataTextile)
        {
            if (id != photodataTextile.MainPhotodataid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(photodataTextile);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PhotodataTextileExists(photodataTextile.MainPhotodataid))
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
            ViewData["MainPhotodataid"] = new SelectList(_context.Photodata, "Id", "Id", photodataTextile.MainPhotodataid);
            ViewData["MainTextileid"] = new SelectList(_context.Textiles, "Id", "Id", photodataTextile.MainTextileid);
            return View(photodataTextile);
        }

        // GET: PhotodataTextile/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.PhotodataTextiles == null)
            {
                return NotFound();
            }

            var photodataTextile = await _context.PhotodataTextiles
                .Include(p => p.Photodatum)
                .Include(p => p.Textile)
                .FirstOrDefaultAsync(m => m.MainPhotodataid == id);
            if (photodataTextile == null)
            {
                return NotFound();
            }

            return View(photodataTextile);
        }

        // POST: PhotodataTextile/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.PhotodataTextiles == null)
            {
                return Problem("Entity set 'postgresContext.PhotodataTextiles'  is null.");
            }
            var photodataTextile = await _context.PhotodataTextiles.FindAsync(id);
            if (photodataTextile != null)
            {
                _context.PhotodataTextiles.Remove(photodataTextile);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PhotodataTextileExists(long id)
        {
          return (_context.PhotodataTextiles?.Any(e => e.MainPhotodataid == id)).GetValueOrDefault();
        }
    }
}
