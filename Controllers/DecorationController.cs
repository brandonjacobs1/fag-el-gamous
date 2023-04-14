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
    public class DecorationController : Controller
    {
        private readonly postgresContext _context;

        public DecorationController(postgresContext context)
        {
            _context = context;
        }

        // GET: Decoration
        public async Task<IActionResult> Index()
        {
              return _context.Decorations != null ? 
                          View(await _context.Decorations.ToListAsync()) :
                          Problem("Entity set 'postgresContext.Decorations'  is null.");
        }

        // GET: Decoration/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.Decorations == null)
            {
                return NotFound();
            }

            var decoration = await _context.Decorations
                .FirstOrDefaultAsync(m => m.Id == id);
            if (decoration == null)
            {
                return NotFound();
            }

            return View(decoration);
        }

        // GET: Decoration/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Decoration/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Decorationid,Value")] Decoration decoration)
        {
            if (ModelState.IsValid)
            {
                _context.Add(decoration);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(decoration);
        }

        // GET: Decoration/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.Decorations == null)
            {
                return NotFound();
            }

            var decoration = await _context.Decorations.FindAsync(id);
            if (decoration == null)
            {
                return NotFound();
            }
            return View(decoration);
        }

        // POST: Decoration/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Decorationid,Value")] Decoration decoration)
        {
            if (id != decoration.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(decoration);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DecorationExists(decoration.Id))
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
            return View(decoration);
        }

        // GET: Decoration/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.Decorations == null)
            {
                return NotFound();
            }

            var decoration = await _context.Decorations
                .FirstOrDefaultAsync(m => m.Id == id);
            if (decoration == null)
            {
                return NotFound();
            }

            return View(decoration);
        }

        // POST: Decoration/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.Decorations == null)
            {
                return Problem("Entity set 'postgresContext.Decorations'  is null.");
            }
            var decoration = await _context.Decorations.FindAsync(id);
            if (decoration != null)
            {
                _context.Decorations.Remove(decoration);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DecorationExists(long id)
        {
          return (_context.Decorations?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
