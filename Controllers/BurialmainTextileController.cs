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
    public class BurialmainTextileController : Controller
    {
        private readonly postgresContext _context;

        public BurialmainTextileController(postgresContext context)
        {
            _context = context;
        }

        // GET: BurialmainTextile
        [Authorize(Roles = "Admin, Researcher")]
        public async Task<IActionResult> Index()
        {
            var postgresContext = _context.BurialmainTextiles.Include(b => b.Burialmain).Include(b => b.Textile);
            return View(await postgresContext.ToListAsync());
        }

        // GET: BurialmainTextile/Details/5
        [Authorize(Roles = "Admin, Researcher")]
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.BurialmainTextiles == null)
            {
                return NotFound();
            }

            var burialmainTextile = await _context.BurialmainTextiles
                .Include(b => b.Burialmain)
                .Include(b => b.Textile)
                .FirstOrDefaultAsync(m => m.MainBurialmainid == id);
            if (burialmainTextile == null)
            {
                return NotFound();
            }

            return View(burialmainTextile);
        }

        // GET: BurialmainTextile/Create
        [Authorize(Roles = "Admin, Researcher")]
        public IActionResult Create()
        {
            ViewData["MainBurialmainid"] = new SelectList(_context.Burialmains, "Id", "Id");
            ViewData["MainTextileid"] = new SelectList(_context.Textiles, "Id", "Id");
            return View();
        }

        // POST: BurialmainTextile/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin, Researcher")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MainBurialmainid,MainTextileid")] BurialmainTextile burialmainTextile)
        {
            if (ModelState.IsValid)
            {
                _context.Add(burialmainTextile);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MainBurialmainid"] = new SelectList(_context.Burialmains, "Id", "Id", burialmainTextile.MainBurialmainid);
            ViewData["MainTextileid"] = new SelectList(_context.Textiles, "Id", "Id", burialmainTextile.MainTextileid);
            return View(burialmainTextile);
        }

        // GET: BurialmainTextile/Edit/5
        [Authorize(Roles = "Admin, Researcher")]
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.BurialmainTextiles == null)
            {
                return NotFound();
            }

            var burialmainTextile = await _context.BurialmainTextiles.FindAsync(id);
            if (burialmainTextile == null)
            {
                return NotFound();
            }
            ViewData["MainBurialmainid"] = new SelectList(_context.Burialmains, "Id", "Id", burialmainTextile.MainBurialmainid);
            ViewData["MainTextileid"] = new SelectList(_context.Textiles, "Id", "Id", burialmainTextile.MainTextileid);
            return View(burialmainTextile);
        }

        // POST: BurialmainTextile/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin, Researcher")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("MainBurialmainid,MainTextileid")] BurialmainTextile burialmainTextile)
        {
            if (id != burialmainTextile.MainBurialmainid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(burialmainTextile);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BurialmainTextileExists(burialmainTextile.MainBurialmainid))
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
            ViewData["MainBurialmainid"] = new SelectList(_context.Burialmains, "Id", "Id", burialmainTextile.MainBurialmainid);
            ViewData["MainTextileid"] = new SelectList(_context.Textiles, "Id", "Id", burialmainTextile.MainTextileid);
            return View(burialmainTextile);
        }

        // GET: BurialmainTextile/Delete/5
        [Authorize(Roles = "Admin, Researcher")]
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.BurialmainTextiles == null)
            {
                return NotFound();
            }

            var burialmainTextile = await _context.BurialmainTextiles
                .Include(b => b.Burialmain)
                .Include(b => b.Textile)
                .FirstOrDefaultAsync(m => m.MainBurialmainid == id);
            if (burialmainTextile == null)
            {
                return NotFound();
            }

            return View(burialmainTextile);
        }

        // POST: BurialmainTextile/Delete/5
        [Authorize(Roles = "Admin, Researcher")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.BurialmainTextiles == null)
            {
                return Problem("Entity set 'postgresContext.BurialmainTextiles'  is null.");
            }
            var burialmainTextile = await _context.BurialmainTextiles.FindAsync(id);
            if (burialmainTextile != null)
            {
                _context.BurialmainTextiles.Remove(burialmainTextile);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin, Researcher")]
        private bool BurialmainTextileExists(long id)
        {
          return (_context.BurialmainTextiles?.Any(e => e.MainBurialmainid == id)).GetValueOrDefault();
        }
    }
}
