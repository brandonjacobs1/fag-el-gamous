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
    public class TextilefunctionTextileController : Controller
    {
        private readonly postgresContext _context;

        public TextilefunctionTextileController(postgresContext context)
        {
            _context = context;
        }

        // GET: TextilefunctionTextile
        public async Task<IActionResult> Index()
        {
            var postgresContext = _context.TextilefunctionTextiles.Include(t => t.Textile).Include(t => t.Textilefunction);
            return View(await postgresContext.ToListAsync());
        }

        // GET: TextilefunctionTextile/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.TextilefunctionTextiles == null)
            {
                return NotFound();
            }

            var textilefunctionTextile = await _context.TextilefunctionTextiles
                .Include(t => t.Textile)
                .Include(t => t.Textilefunction)
                .FirstOrDefaultAsync(m => m.MainTextilefunctionid == id);
            if (textilefunctionTextile == null)
            {
                return NotFound();
            }

            return View(textilefunctionTextile);
        }

        // GET: TextilefunctionTextile/Create
        public IActionResult Create()
        {
            ViewData["MainTextileid"] = new SelectList(_context.Textiles, "Id", "Id");
            ViewData["MainTextilefunctionid"] = new SelectList(_context.Textilefunctions, "Id", "Id");
            return View();
        }

        // POST: TextilefunctionTextile/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MainTextilefunctionid,MainTextileid")] TextilefunctionTextile textilefunctionTextile)
        {
            if (ModelState.IsValid)
            {
                _context.Add(textilefunctionTextile);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MainTextileid"] = new SelectList(_context.Textiles, "Id", "Id", textilefunctionTextile.MainTextileid);
            ViewData["MainTextilefunctionid"] = new SelectList(_context.Textilefunctions, "Id", "Id", textilefunctionTextile.MainTextilefunctionid);
            return View(textilefunctionTextile);
        }

        // GET: TextilefunctionTextile/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.TextilefunctionTextiles == null)
            {
                return NotFound();
            }

            var textilefunctionTextile = await _context.TextilefunctionTextiles.FindAsync(id);
            if (textilefunctionTextile == null)
            {
                return NotFound();
            }
            ViewData["MainTextileid"] = new SelectList(_context.Textiles, "Id", "Id", textilefunctionTextile.MainTextileid);
            ViewData["MainTextilefunctionid"] = new SelectList(_context.Textilefunctions, "Id", "Id", textilefunctionTextile.MainTextilefunctionid);
            return View(textilefunctionTextile);
        }

        // POST: TextilefunctionTextile/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("MainTextilefunctionid,MainTextileid")] TextilefunctionTextile textilefunctionTextile)
        {
            if (id != textilefunctionTextile.MainTextilefunctionid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(textilefunctionTextile);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TextilefunctionTextileExists(textilefunctionTextile.MainTextilefunctionid))
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
            ViewData["MainTextileid"] = new SelectList(_context.Textiles, "Id", "Id", textilefunctionTextile.MainTextileid);
            ViewData["MainTextilefunctionid"] = new SelectList(_context.Textilefunctions, "Id", "Id", textilefunctionTextile.MainTextilefunctionid);
            return View(textilefunctionTextile);
        }

        // GET: TextilefunctionTextile/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.TextilefunctionTextiles == null)
            {
                return NotFound();
            }

            var textilefunctionTextile = await _context.TextilefunctionTextiles
                .Include(t => t.Textile)
                .Include(t => t.Textilefunction)
                .FirstOrDefaultAsync(m => m.MainTextilefunctionid == id);
            if (textilefunctionTextile == null)
            {
                return NotFound();
            }

            return View(textilefunctionTextile);
        }

        // POST: TextilefunctionTextile/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.TextilefunctionTextiles == null)
            {
                return Problem("Entity set 'postgresContext.TextilefunctionTextiles'  is null.");
            }
            var textilefunctionTextile = await _context.TextilefunctionTextiles.FindAsync(id);
            if (textilefunctionTextile != null)
            {
                _context.TextilefunctionTextiles.Remove(textilefunctionTextile);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TextilefunctionTextileExists(long id)
        {
          return (_context.TextilefunctionTextiles?.Any(e => e.MainTextilefunctionid == id)).GetValueOrDefault();
        }
    }
}
