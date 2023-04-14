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
    public class ColorTextileController : Controller
    {
        private readonly postgresContext _context;

        public ColorTextileController(postgresContext context)
        {
            _context = context;
        }

        // GET: ColorTextile
        [Authorize(Roles = "Admin, Researcher")]
        public async Task<IActionResult> Index()
        {
            var postgresContext = _context.ColorTextiles.Include(c => c.Color).Include(c => c.Textile);
            return View(await postgresContext.ToListAsync());
        }

        // GET: ColorTextile/Details/5
        [Authorize(Roles = "Admin, Researcher")]
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.ColorTextiles == null)
            {
                return NotFound();
            }

            var colorTextile = await _context.ColorTextiles
                .Include(c => c.Color)
                .Include(c => c.Textile)
                .FirstOrDefaultAsync(m => m.MainColorid == id);
            if (colorTextile == null)
            {
                return NotFound();
            }

            return View(colorTextile);
        }

        // GET: ColorTextile/Create
        [Authorize(Roles = "Admin, Researcher")]
        public IActionResult Create()
        {
            ViewData["MainColorid"] = new SelectList(_context.Colors, "Id", "Id");
            ViewData["MainTextileid"] = new SelectList(_context.Textiles, "Id", "Id");
            return View();
        }

        // POST: ColorTextile/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin, Researcher")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MainColorid,MainTextileid")] ColorTextile colorTextile)
        {
            if (ModelState.IsValid)
            {
                _context.Add(colorTextile);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MainColorid"] = new SelectList(_context.Colors, "Id", "Id", colorTextile.MainColorid);
            ViewData["MainTextileid"] = new SelectList(_context.Textiles, "Id", "Id", colorTextile.MainTextileid);
            return View(colorTextile);
        }

        // GET: ColorTextile/Edit/5
        [Authorize(Roles = "Admin, Researcher")]
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.ColorTextiles == null)
            {
                return NotFound();
            }

            var colorTextile = await _context.ColorTextiles.FindAsync(id);
            if (colorTextile == null)
            {
                return NotFound();
            }
            ViewData["MainColorid"] = new SelectList(_context.Colors, "Id", "Id", colorTextile.MainColorid);
            ViewData["MainTextileid"] = new SelectList(_context.Textiles, "Id", "Id", colorTextile.MainTextileid);
            return View(colorTextile);
        }

        // POST: ColorTextile/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin, Researcher")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("MainColorid,MainTextileid")] ColorTextile colorTextile)
        {
            if (id != colorTextile.MainColorid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(colorTextile);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ColorTextileExists(colorTextile.MainColorid))
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
            ViewData["MainColorid"] = new SelectList(_context.Colors, "Id", "Id", colorTextile.MainColorid);
            ViewData["MainTextileid"] = new SelectList(_context.Textiles, "Id", "Id", colorTextile.MainTextileid);
            return View(colorTextile);
        }

        // GET: ColorTextile/Delete/5
        [Authorize(Roles = "Admin, Researcher")]
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.ColorTextiles == null)
            {
                return NotFound();
            }

            var colorTextile = await _context.ColorTextiles
                .Include(c => c.Color)
                .Include(c => c.Textile)
                .FirstOrDefaultAsync(m => m.MainColorid == id);
            if (colorTextile == null)
            {
                return NotFound();
            }

            return View(colorTextile);
        }

        // POST: ColorTextile/Delete/5
        [Authorize(Roles = "Admin, Researcher")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.ColorTextiles == null)
            {
                return Problem("Entity set 'postgresContext.ColorTextiles'  is null.");
            }
            var colorTextile = await _context.ColorTextiles.FindAsync(id);
            if (colorTextile != null)
            {
                _context.ColorTextiles.Remove(colorTextile);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin, Researcher")]
        private bool ColorTextileExists(long id)
        {
          return (_context.ColorTextiles?.Any(e => e.MainColorid == id)).GetValueOrDefault();
        }
    }
}
