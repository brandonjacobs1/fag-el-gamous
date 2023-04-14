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
    public class DecorationTextileController : Controller
    {
        private readonly postgresContext _context;

        public DecorationTextileController(postgresContext context)
        {
            _context = context;
        }

        // GET: DecorationTextile
        [Authorize(Roles = "Admin, Researcher")]
        public async Task<IActionResult> Index()
        {
            var postgresContext = _context.DecorationTextiles.Include(d => d.Decoration).Include(d => d.Textile);
            return View(await postgresContext.ToListAsync());
        }

        // GET: DecorationTextile/Details/5
        [Authorize(Roles = "Admin, Researcher")]
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.DecorationTextiles == null)
            {
                return NotFound();
            }

            var decorationTextile = await _context.DecorationTextiles
                .Include(d => d.Decoration)
                .Include(d => d.Textile)
                .FirstOrDefaultAsync(m => m.MainDecorationid == id);
            if (decorationTextile == null)
            {
                return NotFound();
            }

            return View(decorationTextile);
        }

        // GET: DecorationTextile/Create
        [Authorize(Roles = "Admin, Researcher")]
        public IActionResult Create()
        {
            ViewData["MainDecorationid"] = new SelectList(_context.Decorations, "Id", "Id");
            ViewData["MainTextileid"] = new SelectList(_context.Textiles, "Id", "Id");
            return View();
        }

        // POST: DecorationTextile/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin, Researcher")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MainDecorationid,MainTextileid")] DecorationTextile decorationTextile)
        {
            if (ModelState.IsValid)
            {
                _context.Add(decorationTextile);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MainDecorationid"] = new SelectList(_context.Decorations, "Id", "Id", decorationTextile.MainDecorationid);
            ViewData["MainTextileid"] = new SelectList(_context.Textiles, "Id", "Id", decorationTextile.MainTextileid);
            return View(decorationTextile);
        }

        // GET: DecorationTextile/Edit/5
        [Authorize(Roles = "Admin, Researcher")]
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.DecorationTextiles == null)
            {
                return NotFound();
            }

            var decorationTextile = await _context.DecorationTextiles.FindAsync(id);
            if (decorationTextile == null)
            {
                return NotFound();
            }
            ViewData["MainDecorationid"] = new SelectList(_context.Decorations, "Id", "Id", decorationTextile.MainDecorationid);
            ViewData["MainTextileid"] = new SelectList(_context.Textiles, "Id", "Id", decorationTextile.MainTextileid);
            return View(decorationTextile);
        }

        // POST: DecorationTextile/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin, Researcher")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("MainDecorationid,MainTextileid")] DecorationTextile decorationTextile)
        {
            if (id != decorationTextile.MainDecorationid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(decorationTextile);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DecorationTextileExists(decorationTextile.MainDecorationid))
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
            ViewData["MainDecorationid"] = new SelectList(_context.Decorations, "Id", "Id", decorationTextile.MainDecorationid);
            ViewData["MainTextileid"] = new SelectList(_context.Textiles, "Id", "Id", decorationTextile.MainTextileid);
            return View(decorationTextile);
        }

        // GET: DecorationTextile/Delete/5
        [Authorize(Roles = "Admin, Researcher")]
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.DecorationTextiles == null)
            {
                return NotFound();
            }

            var decorationTextile = await _context.DecorationTextiles
                .Include(d => d.Decoration)
                .Include(d => d.Textile)
                .FirstOrDefaultAsync(m => m.MainDecorationid == id);
            if (decorationTextile == null)
            {
                return NotFound();
            }

            return View(decorationTextile);
        }

        // POST: DecorationTextile/Delete/5
        [Authorize(Roles = "Admin, Researcher")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.DecorationTextiles == null)
            {
                return Problem("Entity set 'postgresContext.DecorationTextiles'  is null.");
            }
            var decorationTextile = await _context.DecorationTextiles.FindAsync(id);
            if (decorationTextile != null)
            {
                _context.DecorationTextiles.Remove(decorationTextile);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin, Researcher")]
        private bool DecorationTextileExists(long id)
        {
          return (_context.DecorationTextiles?.Any(e => e.MainDecorationid == id)).GetValueOrDefault();
        }
    }
}
